using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Worlds;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using NotionWorld.Actions;

public class Bullet : MonoBehaviour
{
    [Header("基础属性")]
    [Tooltip("子弹速度曲线")]
    public AnimationCurve SpeedRatio;
    public float Speed;
    [Tooltip("子弹自动回收时间")]
    public float AutoRecycleTime;//最长生存时间（超过该时间会被自动回收）
    [HideInInspector] public Vector3 Scale = new Vector3(1, 1, 1);
    
    protected float NowSpeed;//当前速度
    protected float Nowtime = 0;//当前飞行的时间

    protected int DamageValue;
    protected string AttackTag;
    protected string ActorTag;

    HealthModifier modifier;

    [Header("音效与特效")]
    [Tooltip("撞击的音效")] public AudioSource HitAudioSource;
    [Tooltip("撞击的特效名")] public string HitEffect;
    [Tooltip("自动摧毁的的特效名")] public string DestoryEffect;
    
    protected bool IsNotRecycled;//标记是否被回收的开关
    protected Vector3 m_euler;


    public virtual void ActiveIt(Vector3 RespawnPos, Vector3 Euler, int Damage, string TargetTag)
    {
        DamageValue = Damage;
        transform.localScale = Scale;
        transform.position = RespawnPos;
        m_euler = Euler;
        transform.Rotate(Euler);
        AttackTag = TargetTag;
        gameObject.layer = LayerMask.NameToLayer(TargetTag == "Player" ? "Enemy" : "Player");
        modifier = new HealthModifier(-DamageValue);
    }
   

    protected virtual void FixedUpdate()
    {
        if (IsNotRecycled)
        {
            Nowtime += Time.fixedDeltaTime;
            NowSpeed = SpeedRatio.Evaluate(Nowtime) * Speed;
            transform.Translate(Vector3.up * NowSpeed * Time.fixedDeltaTime);
        }
    }

    public virtual void OnEnable()
    {
        StartCoroutine(AutoRecycle());
        IsNotRecycled = true;
    }

    public virtual IEnumerator AutoRecycle()
    {
        yield return new WaitForSeconds(AutoRecycleTime);
        RecycleNow();
    }

    public virtual void RecycleNow()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        if (IsNotRecycled)
        {
            if (DestoryEffect != null && DestoryEffect != "" && DestoryEffect != " ")
            {
                GameObject shootHitEffect = ObjectPool.GetObject(DestoryEffect, "Effects");
                shootHitEffect.transform.Rotate(m_euler);
                shootHitEffect.transform.position = transform.position;
            }
            Nowtime = 0;
            ObjectPool.RecycleObject(gameObject);
            IsNotRecycled = false;
        }
    }
    public virtual void HitRecycleNow()
    {
        if (IsNotRecycled)
        {
            if (HitEffect != null && HitEffect != "" && HitEffect != " ")
            {
                GameObject shootHitEffect = ObjectPool.GetObject(HitEffect, "Effects");
                // shootHitEffect.transform.Rotate(m_euler);
                shootHitEffect.transform.position = transform.position + transform.up.normalized * 0.5f + Vector3.forward * -5f;
            }
            if (HitAudioSource != null)
            {
                HitAudioSource.Play();
            }
            Nowtime = 0;
            
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            ObjectPool.RecycleObject(gameObject);
            IsNotRecycled = false;
        }
    }

    // 获取当前速度
    public float GetSpeed
    {
        get => NowSpeed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullets"))
        {
            return;
        }
        if (AttackTag != null && AttackTag != "")
        {
            if (collision.gameObject.CompareTag(AttackTag))
            {
                modifier.TakeEffect(collision.gameObject.GetComponent<Entity>());

                MoveTowardFragment moveTowardFragment = new MoveTowardFragment();
                moveTowardFragment.InternalTime = 0.5f;
                moveTowardFragment.Speed = 0.3f;
                moveTowardFragment.Direction = collision.transform.position - transform.position;
                moveTowardFragment.TakeEffect(collision.gameObject.GetComponent<Entity>());

                AnimatorParameterFragment animator = new AnimatorParameterFragment();
                animator.Animator = collision.transform.GetChild(0).GetComponent<Animator>();
                animator.Name = "Hit";
                animator.TakeEffect();
                if (collision.gameObject.GetComponent<Entity>().GetCapability<Health>().Value < 0)
                {
                    animator.Name = "Die";
                    animator.TakeEffect();
                }
            }
        }
        HitRecycleNow();
    }
}
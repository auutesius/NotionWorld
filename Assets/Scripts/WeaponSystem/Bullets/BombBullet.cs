using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Worlds;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using NotionWorld.Actions;

public class BombBullet : Bullet
{
    public float AttackRange;   // 爆炸波及范围

    public int GravitationInternal; // 吸引力效用时间
    public int GravitationPower;    // 吸引力
    GravitationModifier gravitationModifier;
    float flyingDistance = 0;
    float targetDistance = 0;


    public void ActiveIt(Vector3 RespawnPos, Vector3 Euler, int Damage, string TargetTag,float distance)
    {
        targetDistance = distance;
        DamageValue = Damage;
        transform.localScale = Scale;
        transform.position = RespawnPos;
        m_euler = Euler;
        transform.Rotate(Euler);
        AttackTag = TargetTag;
        gameObject.layer = LayerMask.NameToLayer(TargetTag == "Player" ? "Enemy" : "Player");
    }
   

    protected override void FixedUpdate()
    {
        if (IsNotRecycled)
        {
            Nowtime += Time.deltaTime;
            NowSpeed = SpeedRatio.Evaluate(Nowtime) * Speed;
            if(flyingDistance <= targetDistance){
                Vector2 trans = Vector3.up * NowSpeed;
                transform.Translate(trans);
                transform.GetChild(0).transform.Rotate(NowSpeed * Vector3.forward * 30);
                flyingDistance += trans.magnitude;
            }
            
        }
    }


    public override void OnEnable()
    {
        StartCoroutine(AutoRecycle());
        IsNotRecycled = true;
    }

    public override IEnumerator AutoRecycle()
    {
        yield return new WaitForSeconds(AutoRecycleTime);
        HitRecycleNow();
    }

    public override void HitRecycleNow()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        if (IsNotRecycled)
        {
            if (HitEffect != null || HitEffect != "")
            {
                GameObject shootHitEffect = ObjectPool.GetObject(HitEffect, "Effects");
                shootHitEffect.transform.position = transform.position + Vector3.forward * 10f;
            }
            Nowtime = 0;

            List<GameObject> targets = GetAttackTargets();
            if (targets != null && targets.Count != 0)
            {
                foreach (var t in targets)
                {
                    gravitationModifier = new GravitationModifier(transform.position, GravitationInternal, GravitationPower);
                    gravitationModifier.TakeEffect(t.GetComponent<Entity>());
                    AnimatorParameterFragment animator = new AnimatorParameterFragment();
                    for (int i = 0; i < 3; i++)
                    {
                        HealthModifier hm = new HealthModifier(-30);
                        hm.TakeEffect(t.GetComponent<Entity>()); 
                        animator.Animator = t.transform.GetChild(0).GetComponent<Animator>();
                        animator.Name = "Hit";
                        animator.TakeEffect();
                    }
                    if (t.gameObject.GetComponent<Entity>().GetCapability<Health>().Value < 0)
                    {
                        animator.Name = "Die";
                        animator.TakeEffect();
                        if (t.gameObject.CompareTag("Player"))
                        {
                            StopPlayerMovementFragment stopPlayerMovementFragment = new StopPlayerMovementFragment(0f);
                            stopPlayerMovementFragment.TakeEffect(t.GetComponent<Entity>());

                            StopPlayerStateFragment stopPlayerStateFragment = new StopPlayerStateFragment(0f);
                            stopPlayerStateFragment.TakeEffect(t.GetComponent<Entity>());
                        };
                        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    }
                }
            }
            flyingDistance = 0;
            targetDistance = 0;
            ObjectPool.RecycleObject(gameObject);
            IsNotRecycled = false;
        }
    }

    private List<GameObject> GetAttackTargets()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, AttackRange);
        if (cols.Length > 0)
        {
            List<GameObject> Targets = new List<GameObject>();
            foreach (var c in cols)
            {
                if (c.transform.CompareTag(AttackTag))
                {
                    Targets.Add(c.gameObject);
                }
            }
            if (Targets.Count != 0)
                return Targets;
        }
        return null;
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // if (collision.gameObject.CompareTag("Bullets"))
        // {
        //     return;
        // }
        if (AttackTag != null && AttackTag != "")
        {
            if (collision.gameObject.CompareTag(AttackTag))
            {
            }
        }
        HitRecycleNow();
    }

}
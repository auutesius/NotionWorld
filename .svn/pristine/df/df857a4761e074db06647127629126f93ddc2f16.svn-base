using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Worlds;
using NotionWorld.Entities;
using NotionWorld.Capabilities;

public class BombBullet : Bullet
{
    public float AttackRange;   // 爆炸波及范围

    public int GravitationInternal; // 吸引力效用时间
    public int GravitationPower;    // 吸引力


    private Vector3 TargetPos;
    HealthModifier healthModifier;
    GravitationModifier gravitationModifier;


    public override void ActiveIt(Vector3 RespawnPos, Vector3 Euler, int Damage, string TargetTag)
    {
        DamageValue = Damage;
        transform.localScale = Scale;
        transform.position = RespawnPos;
        m_euler = Euler;
        transform.Rotate(Euler);
        AttackTag = TargetTag;
        gameObject.layer = LayerMask.NameToLayer(TargetTag == "Player" ? "Enemy" : "Player");
        healthModifier = new HealthModifier(-DamageValue);
    }
   

    protected override void FixedUpdate()
    {
        if (IsNotRecycled)
        {
            Nowtime += Time.deltaTime;
            NowSpeed = SpeedRatio.Evaluate(Nowtime) * Speed;
            transform.Translate(Vector3.up * NowSpeed);
            transform.GetChild(0).transform.Rotate(NowSpeed * Vector3.forward * 30);
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
            if (HitAudioSource != null)
            {
                HitAudioSource.Play();
            }
            Nowtime = 0;

            List<GameObject> targets = GetAttackTargets();
            if (targets != null && targets.Count != 0)
            {
                foreach (var t in targets)
                {
                    gravitationModifier = new GravitationModifier(transform.position, GravitationInternal);
                    gravitationModifier.TakeEffect(t.GetComponent<Entity>());
                    for (int i = 0; i < 3; i++)
                    {
                        HealthModifier hm = new HealthModifier(-30);
                        hm.TakeEffect(t.GetComponent<Entity>());
                    }
                }
            }

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
        if (collision.gameObject.CompareTag("Bullets"))
        {
            return;
        }
        if (AttackTag != null && AttackTag != "")
        {
            if (collision.gameObject.CompareTag(AttackTag))
            {
            }
        }
        HitRecycleNow();
    }

}
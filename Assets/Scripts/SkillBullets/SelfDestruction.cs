using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Modifiers;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using NotionWorld.Worlds;
using HealthModifier = NotionWorld.Modifiers.HealthModifier;

public sealed class SelfDestruction : SkillBullet
{
    public int damage;

    public int selfDamage;

    public float warningTime;

    public float radius;

    private HealthModifier healthModifier;

    public string explodeEffect;

    private void Awake()
    {
        healthModifier = new HealthModifier();
    }

    public override void Launch(Vector2 position, Vector2 direction)
    {
        StartCoroutine(WarnCorotinue());
    }

    private IEnumerator WarnCorotinue()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        float timer = warningTime;

        while (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
            yield return wait;
        }
        SelfDestruct();
        ObjectPool.RecycleObject(gameObject);
    }

    private void SelfDestruct()
    {
        GameObject shootHitEffect = ObjectPool.GetObject(explodeEffect, "Effects");
        shootHitEffect.transform.position = Source.transform.position;

        Entity entity;
        if ((Target.transform.position - Source.transform.position).magnitude < radius)
        {
            entity = Target.GetComponent<Entity>();
            if (entity != null)
            {
                healthModifier.Health = entity.GetCapability<Health>();
                healthModifier.DeltaValue = -damage;
            }
            healthModifier.TakeEffect();
        }

        entity = Source.GetComponent<Entity>();
        if (entity != null)
        {
            healthModifier.Health = entity.GetCapability<Health>();
            healthModifier.DeltaValue = -selfDamage;
        }
        healthModifier.TakeEffect();
    }
}
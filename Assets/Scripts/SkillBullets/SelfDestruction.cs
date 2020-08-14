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

    public float warningTime;

    public float radius;

    private Vector2 originPosition;

    private HealthModifier healthModifier;

    private void Awake()
    {
        healthModifier = new HealthModifier()
        {
            DeltaValue = -damage
        };
    }

    public override void Launch(Vector2 position, Vector2 direction)
    {
        originPosition = position;
        StartCoroutine(WarnCorotinue());
    }

    private IEnumerator WarnCorotinue()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        float timer = warningTime;

        while (timer > 0)
        {
            //TODO: 闪烁效果
            timer -= Time.fixedDeltaTime;
            transform.position = originPosition;
            yield return wait;
        }
        SelfDestruct();
        ObjectPool.RecycleObject(this.gameObject);
    }

    private void SelfDestruct()
    {
        //TODO: 爆炸效果
        var entity = Target.GetComponent<Entity>();
        if (entity != null)
        {
            healthModifier.Health = entity.GetCapability<Health>();
        }
        healthModifier.TakeEffect();

        if ((Target.transform.position - Source.transform.position).magnitude < radius)
        {
            entity = Source.GetComponent<Entity>();
            if (entity != null)
            {
                healthModifier.Health = entity.GetCapability<Health>();
            }
            healthModifier.TakeEffect();
        }
    }
}
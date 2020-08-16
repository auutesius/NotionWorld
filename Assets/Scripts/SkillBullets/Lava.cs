using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Worlds;
using NotionWorld.Modifiers;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using HealthModifier = NotionWorld.Modifiers.HealthModifier;
using NotionWorld.Buffs;

public sealed class Lava : SkillBullet
{
    public int damage;

    public float existTime;

    public override void Launch(Vector2 position, Vector2 direction)
    {
        transform.position = position;
        StartCoroutine(ExistingCorotinue());
    }

    private IEnumerator ExistingCorotinue()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();

        float timer = existTime;

        while (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
            yield return wait;
        }
        ObjectPool.RecycleObject(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var entity = other.gameObject.GetComponent<Entity>();
        if (entity != null)
        {
            var list = entity.GetCapability<BuffList>();
            if(list != null)
            {
                list.SetBuff(new Bleeding(damage));   
            }
        }
    }
}
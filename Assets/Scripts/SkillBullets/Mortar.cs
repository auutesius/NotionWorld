using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Worlds;
using NotionWorld.Modifiers;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using HealthModifier = NotionWorld.Modifiers.HealthModifier;

public sealed class Mortar : SkillBullet
{
    public string bullet;

    public float warningTime;

    public int damage;

    public float damageTime;

    private HealthModifier healthModifier;

    private AnimatorTriggerModifier animatorTrigger;

    private bool readyToDamage;

    private void Awake()
    {
        healthModifier = new HealthModifier()
        {
            DeltaValue = -damage
        };
        animatorTrigger = new AnimatorTriggerModifier()
        {
            Name = "Hit"
        };
    }

    public override void Launch(Vector2 position, Vector2 direction)
    {
        transform.position = position;
        StartCoroutine(HitCorotinue());
    }

    private IEnumerator HitCorotinue()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();

        float timer = warningTime;

        while (timer > 0)
        {
            timer -= Time.fixedDeltaTime;

            yield return wait;
        }

        timer = damageTime;
        readyToDamage = true;
        while (timer > 0)
        {
            timer -= Time.fixedDeltaTime;

            yield return wait;
        }
        readyToDamage = false;

        var obj = ObjectPool.GetObject(bullet, "SkillBullets");
        SkillBullet skill = obj.GetComponent<SkillBullet>();
        skill.Source = Source;
        skill.Target = Target;

        skill.Launch(transform.position, transform.right);

        ObjectPool.RecycleObject(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(!readyToDamage)
        {
            return;
        }

        var gameObject = other.gameObject;

        if (gameObject == Target)
        {
            var entity = gameObject.GetComponent<Entity>();
            if (entity != null)
            {
                healthModifier.Health = entity.GetCapability<Health>();
            }
            animatorTrigger.Animator = gameObject.GetComponent<Animator>();

            TakeEffect();

            readyToDamage = false;
        }
    }

    private void TakeEffect()
    {
        healthModifier.TakeEffect();
        animatorTrigger.TakeEffect();
    }
}

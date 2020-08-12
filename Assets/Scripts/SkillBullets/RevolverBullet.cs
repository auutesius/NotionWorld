using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Modifiers;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using NotionWorld.Worlds;
using HealthModifier = NotionWorld.Modifiers.HealthModifier;

public sealed class RevolverBullet : SkillBullet
{
    public int damage;

    public float speed;

    public float time;

    private HealthModifier healthModifier;

    private AnimatorTriggerModifier animatorTrigger;

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
        transform.forward = direction;
        StartCoroutine(MoveCorotinue());
    }

    private IEnumerator MoveCorotinue()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();

        float timer = time;

        Vector2 deltaForward = transform.forward * speed * Time.fixedDeltaTime;
        Vector2 position = transform.position;

        while (timer > 0)
        {
            transform.position = position;
            position += deltaForward;
            timer -= Time.fixedDeltaTime;
            yield return wait;
        }

        ObjectPool.RecycleObject(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
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

            ObjectPool.RecycleObject(this.gameObject);
        }
    }

    private void TakeEffect()
    {
        healthModifier.TakeEffect();
        animatorTrigger.TakeEffect();
    }
}

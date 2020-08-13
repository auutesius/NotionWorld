using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Modifiers;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using NotionWorld.Worlds;
using HealthModifier = NotionWorld.Modifiers.HealthModifier;

public sealed class RectDamage : SkillBullet
{
    public int damage;

    public float length;

    public float width;

    public float time;

    public float warningTime;

    private HealthModifier healthModifier;

    private AnimatorTriggerModifier animatorTrigger;

    private BoxCollider2D boxCollider;

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
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(length, width);
        boxCollider.offset = new Vector2(length / 2, 0);
    }

    public override void Launch(Vector2 position, Vector2 direction)
    {
        transform.position = position;
        transform.right = direction;
        StartCoroutine(WarnCorotinue());
    }

    private IEnumerator WarnCorotinue()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        float timer = warningTime;

        boxCollider.enabled = false;

        while (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
            yield return wait;
        }

        boxCollider.enabled = true;

        timer = time;
        while (timer > 0)
        {
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

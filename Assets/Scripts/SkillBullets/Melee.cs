using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Modifiers;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using NotionWorld.Worlds;
using HealthModifier = NotionWorld.Modifiers.HealthModifier;

public sealed class Melee : SkillBullet
{
    public int damage;

    public float radius;

    public float time;

    public float angle;

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

    public override void Launch(Vector3 position, Vector3 direction)
    {
        transform.position = position;
        transform.LookAt(position + direction);
        StartCoroutine(SweepCorotinue());
    }

    private IEnumerator SweepCorotinue()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();

        float timer = time;
        float anglePerFrame = angle / time * Time.fixedDeltaTime;
        float halfAngle = angle / 2;

        transform.Rotate(0, 0, -halfAngle);

        while (timer > 0)
        {
            var hits = Physics2D.RaycastAll(transform.position, transform.forward);
            foreach (var hit in hits)
            {
                var gameObject = hit.transform.gameObject;
                if (gameObject == Target)
                {
                    var entity = gameObject.GetComponent<Entity>();
                    if (entity != null)
                    {
                        healthModifier.Health = entity.GetCapability<Health>();
                    }
                    animatorTrigger.Animator = gameObject.GetComponent<Animator>();

                    TakeEffect();
                    timer = 0;
                }
            }
            timer -= Time.fixedDeltaTime;
            yield return wait;
        }

        ObjectPool.RecycleObject(this.gameObject);
    }

    private void TakeEffect()
    {
        healthModifier.TakeEffect();
        animatorTrigger.TakeEffect();
    }
}

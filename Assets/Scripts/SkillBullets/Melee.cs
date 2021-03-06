﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Modifiers;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using NotionWorld.Worlds;
using HealthModifier = NotionWorld.Modifiers.HealthModifier;
using NotionWorld.Actions;

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

    public override void Launch(Vector2 position, Vector2 direction)
    {
        transform.position = position;
        transform.right = direction;
        StartCoroutine(SweepCorotinue());
    }

    private IEnumerator SweepCorotinue()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();

        float timer = time;
        float anglePerFrame = angle / time * Time.fixedDeltaTime;
        float halfAngle = angle / 2;

        transform.Rotate( 0, 0,-halfAngle);

        while (timer > 0)
        {
            var hits = Physics2D.RaycastAll(transform.position, transform.right);
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
                    animatorTrigger.Animator = gameObject.transform.GetChild(0).GetComponent<Animator>();

                    animatorTrigger.Name = "Hit";
                    if (gameObject.GetComponent<Entity>().GetCapability<Health>().Value <= 0)
                    {
                        animatorTrigger.Name = "Die";

                        if (gameObject.gameObject.CompareTag("Player"))
                        {
                            StopPlayerMovementFragment stopPlayerMovementFragment = new StopPlayerMovementFragment(0f);
                            stopPlayerMovementFragment.TakeEffect(gameObject.GetComponent<Entity>());

                            StopPlayerStateFragment stopPlayerStateFragment = new StopPlayerStateFragment(0f);
                            stopPlayerStateFragment.TakeEffect(gameObject.GetComponent<Entity>());
                        };
                        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
                    }

                    TakeEffect();
                    timer = 0;
                }
            }
            timer -= Time.fixedDeltaTime;
            transform.Rotate(0, 0,anglePerFrame);
            yield return wait;
        }

        ObjectPool.RecycleObject(gameObject);
    }

    private void TakeEffect()
    {
        healthModifier.TakeEffect();
        animatorTrigger.TakeEffect();
    }
}

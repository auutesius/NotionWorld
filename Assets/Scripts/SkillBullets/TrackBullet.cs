﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Modifiers;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using NotionWorld.Worlds;
using HealthModifier = NotionWorld.Modifiers.HealthModifier;

public sealed class TrackBullet : SkillBullet
{
    public float speed;

    public float time;

    public int damage;

    public float maxTrackAngle;

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
        StartCoroutine(TrackCorotinue());
    }

    private IEnumerator TrackCorotinue()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();

        float timer = time;
        while (timer > 0)
        {
            Vector2 movement = transform.right * speed * Time.fixedDeltaTime;

            Vector2 moveDirection = transform.right;
            Vector2 targetDirection = Target.transform.position - transform.position;
            targetDirection = targetDirection.normalized;

            float targetAngle = Vector2.Angle(transform.right, targetDirection);
            float angle = maxTrackAngle < targetAngle ? maxTrackAngle : targetAngle;

            moveDirection = Vector2.Lerp(moveDirection, targetDirection, angle / targetAngle);

            Vector2 position =  transform.position;
            position += moveDirection * speed * Time.fixedDeltaTime;;
            transform.position = position;
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
            animatorTrigger.Animator = gameObject.transform.GetChild(0).GetComponent<Animator>();

            animatorTrigger.Name = "Hit";
            if (other.gameObject.GetComponent<Entity>().GetCapability<Health>().Value < 0)
            {
                animatorTrigger.Name = "Die";
            }
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

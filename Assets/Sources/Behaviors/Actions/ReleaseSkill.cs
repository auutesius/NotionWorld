﻿using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using NotionWorld.Entities;
using UnityEngine;
using NotionWorld.Actions;
using NotionWorld.Capabilities;
using NotionWorld.Modifiers;

namespace NotionWorld.Behaviors
{
    [TaskCategory("NotionWorld")]
    public sealed class ReleaseSkill : Action
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("Skill Want to use.")]
        public string skillID;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("Attack Target.")]
        public SharedGameObject targetGameObject;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("Skill Animator.")]
        public SharedGameObject animatorObject;

        private float coldDownTimer;

        private AnimatorTriggerModifier animatorModifier;

        public override void OnAwake()
        {
            var animator = animatorObject.Value.transform.GetChild(0).GetComponent<Animator>();

            animatorModifier = new AnimatorTriggerModifier()
            {
                Animator = animator,
                Name = skillID
            };
        }

        public override TaskStatus OnUpdate()
        {
            if (coldDownTimer <= 0)
            {
                Skill();
                return TaskStatus.Success;
            }
            else
            {
                coldDownTimer -= Time.deltaTime;
                return TaskStatus.Failure;
            }
        }

        private void Skill()
        {
            GameObject prefab = NotionWorld.Worlds.ObjectPool.GetObject(skillID, "SkillBullets");
            SkillBullet bullet = prefab.GetComponent<SkillBullet>();
            bullet.Source = Owner.gameObject;
            bullet.Target = targetGameObject.Value;

            Vector3 position = animatorModifier.Animator.transform.position;
            Vector3 direction = targetGameObject.Value.transform.position - position;
            direction = direction.normalized;

            bullet.Launch(position, direction);
            coldDownTimer = bullet.coldDown;

            animatorModifier.TakeEffect();
        }
    }

}
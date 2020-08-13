using System.Collections;
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
        [BehaviorDesigner.Runtime.Tasks.Tooltip("Skill want to use.")]
        public string skillBullet;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("Attack Target.")]
        public SharedGameObject targetGameObject;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("Skill Animator.")]
        public SharedGameObject animatorObject;

        private float coldDownTimer;

        private AnimatorTriggerModifier animatorModifier;

        public override void OnAwake()
        {
            var animator = animatorObject.Value.GetComponent<Animator>();

            animatorModifier = new AnimatorTriggerModifier()
            {
                Animator = animator,
                Name = "Attack"
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
            GameObject prefab = NotionWorld.Worlds.ObjectPool.GetObject(skillBullet, "SkillBullets");
            SkillBullet bullet = prefab.GetComponent<SkillBullet>();
            bullet.Source = Owner.gameObject;
            bullet.Target = targetGameObject.Value;

            Vector2 position = animatorModifier.Animator.transform.position;
            Vector2 direction = ((Vector2)targetGameObject.Value.transform.position - position);
            direction = direction.normalized;

            bullet.Launch(position, direction);
            coldDownTimer = bullet.coldDown;

            animatorModifier.TakeEffect();
        }
    }

}
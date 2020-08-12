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
    public sealed class EntityAttack : Action
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("Attack Target.")]
        public SharedGameObject targetGameObject;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("Attack Animator.")]
        public SharedGameObject animatorObject;

        private Entity entity;

        private Attack attack;

        private EntityAttackFragment attackFragment = new EntityAttackFragment();

        private AnimatorTriggerModifier animatorModifier;

        private float coldDownTimer;

        public override void OnAwake()
        {
            entity = Owner.GetComponent<Entity>();

             var animator = animatorObject.Value.transform.GetChild(0).GetComponent<Animator>();

            animatorModifier = new AnimatorTriggerModifier()
            {
                Animator = animator,
                Name = "Attack"
            };
        }

        public override void OnStart()
        {
            attack = entity.GetCapability<Attack>();         
        }

        public override TaskStatus OnUpdate()
        {
            if (coldDownTimer <= 0)
            {
                Attack();              
                return TaskStatus.Success;
            }
            else
            {
                coldDownTimer -= Time.deltaTime;
                return TaskStatus.Failure;
            }
        }

        private void Attack()
        {
            attackFragment.Target = targetGameObject.Value;
            attackFragment.TakeEffect(entity);

            animatorModifier.TakeEffect();

            coldDownTimer = attack.Interval;
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using NotionWorld.Entities;
using UnityEngine;
using NotionWorld.Actions;
using NotionWorld.Capabilities;

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

        private Animator animator;

        private EntityAttackFragment attackFragment = new EntityAttackFragment();

        private AnimatorParameterFragment animatorFragment = new AnimatorParameterFragment()
        {
            Name = "IsAttacking",
            Value = true
        };

        private float coldDownTimer;

        public override void OnAwake()
        {
            entity = Owner.GetComponent<Entity>();
        }

        public override void OnStart()
        {
            attack = entity.GetCapability<Attack>();

            animator = animatorObject.Value.GetComponent<Animator>();
        }

        public override TaskStatus OnUpdate()
        {
            if (coldDownTimer <= 0)
            {
                Attack();
                coldDownTimer = attack.Interval;
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
            animatorFragment.Animator = animator;
            animatorFragment.TakeEffect();
        }
    }

}
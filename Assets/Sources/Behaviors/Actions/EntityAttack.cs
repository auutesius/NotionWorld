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
        public SharedGameObject animator;

        private Entity entity;

        private Attack attack;

        private EntityAttackAction attackAction;

        private float coldDownTimer;

        public override void OnAwake()
        {
            entity = Owner.GetComponent<Entity>();

            attackAction = new EntityAttackAction();
            attackAction.AttackAnimator = animator.Value.GetComponent<Animator>();
        }

        public override void OnStart()
        {
            attack = entity.GetCapability<Attack>();
        }

        public override TaskStatus OnUpdate()
        {
            if (coldDownTimer <= 0)
            {
                attackAction.Target = targetGameObject.Value;
                attackAction.TakeAction(entity);
                coldDownTimer = attack.Interval;
                return TaskStatus.Success;
            }
            else
            {
                coldDownTimer -= Time.deltaTime;
                return TaskStatus.Failure;
            }
        }
    }

}
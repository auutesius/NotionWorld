using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using NotionWorld.Entities;
using UnityEngine;

namespace NotionWorld.Behaviors
{
    [TaskCategory("NotionWorld")]
    public sealed class EntityAttack : Action
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("Attack Target.")]
        public SharedGameObject targetGameObject;

        private Vector3 direction;

        private EntityMovement movement;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("Move Trend of GameObject.")]
        public MoveTrends trend;

        public enum MoveTrends
        {
            Towards, Away
        }

        public override void OnAwake()
        {
            movement = Owner.GetComponent<EntityMovement>();
        }

        public override TaskStatus OnUpdate()
        {
            movement.Direction = MoveDirection(trend);
            return TaskStatus.Success;
        }

        private Vector2 MoveDirection(MoveTrends trend)
        {
            Vector2 direction;
            switch (trend)
            {
                case MoveTrends.Away:
                    direction = transform.position - targetGameObject.Value.transform.position;
                    break;
                default:
                    direction = targetGameObject.Value.transform.position - transform.position;
                    break;
            }
            direction = direction.normalized;
            return direction;
        }
    }

}
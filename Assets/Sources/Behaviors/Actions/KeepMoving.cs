using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using NotionWorld.Entities;
using UnityEngine;

namespace NotionWorld.Behaviors
{
    [TaskCategory("NotionWorld")]
    public sealed class KeepMoving : Action
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("The GameObject that tracks.")]
        public SharedGameObject targetGameObject;

        private Vector3 direction;

        private EntityMovement movement;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("Move Trend of GameObject.")]
        public MoveTrends trend;

        public float targetDisturbance = 2.8F;

        public float minMovingTime = 1F;

        public float maxMovingTime = 1.5F;

        private float currentTime = float.MaxValue;

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
            if (currentTime > maxMovingTime)
            {
                currentTime = Random.Range(minMovingTime, maxMovingTime);
                direction = MoveDirection(trend);
            }
            if (currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                movement.Direction = direction;
                return TaskStatus.Running;
            }
            else
            {
                currentTime = float.MaxValue;
                return TaskStatus.Success;
            }
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
            Vector2 horizontal = Vector3.Cross(Vector3.forward, direction).normalized;
            horizontal *= Random.Range(-targetDisturbance, targetDisturbance);
            direction += horizontal;
            direction = direction.normalized;
            return direction;
        }
    }

}
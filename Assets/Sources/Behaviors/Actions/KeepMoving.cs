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
    public sealed class KeepMoving : Action
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("The GameObject that tracks.")]
        public SharedGameObject targetGameObject;

        private Vector3 direction;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("Move Trend of GameObject.")]
        public MoveTrends trend;

        private Entity entity;

        private Speed speed;

        public float targetDisturbance = 2.8F;

        public float minMovingTime = 1F;

        public float maxMovingTime = 1.5F;

        private float currentTime = float.MaxValue;

        private MoveFragment moveFragment;

        public enum MoveTrends
        {
            Towards, Away
        }

        public override void OnAwake()
        {
            moveFragment = new MoveFragment();
            entity = Owner.GetComponent<Entity>();
        }

        public override void OnStart()
        {
            speed = entity.GetCapability<Speed>();
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
                moveFragment.Movement = Time.deltaTime * direction * speed.Value;
                moveFragment.TakeEffect(entity);
                return TaskStatus.Running;
            }
            else
            {
                ResetTime();
                return TaskStatus.Success;
            }
        }

        private void ResetTime()
        {
            currentTime = float.MaxValue;
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
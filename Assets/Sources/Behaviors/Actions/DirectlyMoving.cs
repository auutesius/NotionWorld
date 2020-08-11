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
    public sealed class DirectlyMoving : Action
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("The GameObject that tracks.")]
        public SharedGameObject targetGameObject;

        public enum MoveTrends
        {
            Towards, Away
        }

        [BehaviorDesigner.Runtime.Tasks.Tooltip("Move Trend of GameObject.")]
        public MoveTrends trend;

        private Entity entity;

        private Speed speed;

        private MoveAction moveAction;

        public override void OnAwake()
        {
            moveAction = new MoveAction();
            entity = Owner.GetComponent<Entity>();
        }

        public override void OnStart()
        {
            speed = entity.GetCapability<Speed>();
        }

        public override TaskStatus OnUpdate()
        {
            moveAction.Movement = Time.deltaTime * MoveDirection(trend) * speed.Value;
            moveAction.TakeAction(entity);
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
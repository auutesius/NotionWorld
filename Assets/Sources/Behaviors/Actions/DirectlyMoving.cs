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

        private Speed speed;

        private AnimatorTriggerModifier animatorModifier;

        private PositionModifier positionModifier;

        public override void OnAwake()
        {
            var animator = Owner.GetComponent<Animator>();

            animatorModifier = new AnimatorTriggerModifier()
            {
                Animator = animator,
                Name = "Walk"
            };

            positionModifier = new PositionModifier()
            {
                Transform = transform,
            };
        }

        public override void OnStart()
        {
            speed = Owner.GetComponent<Entity>().GetCapability<Speed>();
        }

        public override TaskStatus OnUpdate()
        {
            TakeAction();
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

        private void TakeAction()
        {
            positionModifier.DeltaPosition = Time.deltaTime * MoveDirection(trend) * speed.Value;
            positionModifier.TakeEffect();

            animatorModifier.TakeEffect();
        }
    }

}
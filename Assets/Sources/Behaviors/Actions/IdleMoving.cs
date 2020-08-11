using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using NotionWorld.Entities;
using UnityEngine;

namespace NotionWorld.Behaviors
{
    [TaskCategory("NotionWorld")]
    public sealed class IdleMoving : Action
    {
        private EntityMovement movement;

        public float minIdleTime = 0.5F;

        public float maxIdleTime = 1F;

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
            if (currentTime > maxIdleTime)
            {
                currentTime = Random.Range(minIdleTime, maxIdleTime);
            }

            if(currentTime > 0)
            {
                currentTime -= Time.deltaTime;
                movement.Direction = Vector2.zero;
                return TaskStatus.Running;
            }
            else
            {
                currentTime = float.MaxValue;
                return TaskStatus.Success;
            }         
        }
    }

}
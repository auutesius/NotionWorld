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
        public float minIdleTime = 0.5F;

        public float maxIdleTime = 1F;

        private float currentTime = float.MaxValue;


        public override TaskStatus OnUpdate()
        {
            if (currentTime > maxIdleTime)
            {
                currentTime = Random.Range(minIdleTime, maxIdleTime);
            }

            if(currentTime > 0)
            {
                currentTime -= Time.deltaTime; 
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
    }

}
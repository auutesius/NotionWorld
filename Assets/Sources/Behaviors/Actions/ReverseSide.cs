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
    public sealed class ReverseSide : Action
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("Target face to.")]
        public SharedGameObject targetGameObject;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("Animator want to reverse.")]
        public SharedGameObject animatorObject;

        private float coldDownTimer;

        private float lastSide;

        public override void OnAwake()
        {
            lastSide = targetGameObject.Value.transform.position.x - animatorObject.Value.transform.position.x;
            lastSide = Row(-1, 0, lastSide);
        }

        public override TaskStatus OnUpdate()
        {
            float side = targetGameObject.Value.transform.position.x - animatorObject.Value.transform.position.x;
            side = Row(-1, 0, side);
            if (side != lastSide)
            {
                lastSide = side;
                Vector3 euler = animatorObject.Value.transform.rotation.eulerAngles;
                euler.y = side * 180;
                animatorObject.Value.transform.rotation = Quaternion.Euler(euler);
            }
            return TaskStatus.Success;
        }

        private float Row(float min, float max, float value)
        {
            if (value < (min + max) / 2)
            {
                return min;
            }
            else
            {
                return max;
            }
        }
    }

}
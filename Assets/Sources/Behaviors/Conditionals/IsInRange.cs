using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace NotionWorld.Behaviors
{
    [TaskCategory("NotionWorld")]
    [TaskDescription("Returns Success if target is is range.")]
    public sealed class IsInRange : Conditional
    {
        [Tooltip("GameObject what to check.")]
        public SharedGameObject targetGameObject;

        [Tooltip("Min Distance of checking area.")]
        public float minDistance;


        [Tooltip("Max Distance of checking area.")]
        public float maxDistance;

        public override TaskStatus OnUpdate()
        {
            float distance  = (gameObject.transform.position - targetGameObject.Value.transform.position).magnitude;

            if(distance < minDistance)
            {
                return TaskStatus.Failure;
            }
            else if (distance > maxDistance)
            {
                return TaskStatus.Failure;
            }
            return TaskStatus.Success;
        }
    }

}
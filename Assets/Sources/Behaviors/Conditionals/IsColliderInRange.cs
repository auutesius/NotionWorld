using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace NotionWorld.Behaviors
{
    [TaskCategory("NotionWorld")]
    [TaskDescription("Returns Success if target is is range.")]
    public sealed class IsColliderInRange : Conditional
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("GameObject what to check.")]
        public SharedGameObject targetGameObject;

        [BehaviorDesigner.Runtime.Tasks.Tooltip("Max Distance of checking area.")]
        public float maxDistance;

        public override TaskStatus OnUpdate()
        {
            var direction = (targetGameObject.Value.transform.position - transform.position).normalized;
            var hits = Physics2D.RaycastAll(transform.position, direction, maxDistance);

            foreach (var hit in hits)
            {
                if (hit.collider.gameObject == targetGameObject.Value)
                {
                    return TaskStatus.Success;
                }
            }
            return TaskStatus.Failure;
        }
    }

}
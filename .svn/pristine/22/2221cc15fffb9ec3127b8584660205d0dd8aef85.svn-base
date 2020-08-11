using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace NotionWorld.Behaviors
{
    [TaskCategory("NotionWorld")]
    [TaskDescription("Returns Success if tracking target is not null.")]
    public sealed class IsTracking : Conditional
    {
        [Tooltip("The GameObject that tracks.")]
        public SharedGameObject targetGameObject;

        public override TaskStatus OnUpdate()
        {
            return targetGameObject != null ? TaskStatus.Success : TaskStatus.Failure;
        }
    }

}
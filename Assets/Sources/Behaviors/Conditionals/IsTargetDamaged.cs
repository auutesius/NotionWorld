using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using NotionWorld.Entities;
using NotionWorld.Capabilities;

namespace NotionWorld.Behaviors
{
    [TaskCategory("NotionWorld")]
    [TaskDescription("Returns Success if target damaged.")]
    public sealed class IsTargetDamaged : Conditional
    {
         [Tooltip("The Attack Target GameObject.")]
        public SharedGameObject targetGameObject;

        private Health health;

        private int lastHealth;

        public override void OnStart()
        {
            health = targetGameObject.Value.GetComponent<Entity>().GetCapability<Health>();
            lastHealth = health.Value;
        }

        public override TaskStatus OnUpdate()
        {
            if (lastHealth == health.Value)
            {
                return TaskStatus.Failure;
            }
            else
            {
                lastHealth = health.Value;
                return TaskStatus.Success;
            }

        }
    }

}
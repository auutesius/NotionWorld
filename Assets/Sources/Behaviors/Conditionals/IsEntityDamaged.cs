using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using NotionWorld.Entities;
using NotionWorld.Capabilities;

namespace NotionWorld.Behaviors
{
    [TaskCategory("NotionWorld")]
    [TaskDescription("Returns Success if entity damaged.")]
    public sealed class IsEntityDamaged : Conditional
    {
        private Health health;

        private int lastHealth;

        public override void OnStart()
        {
            health = Owner.GetComponent<Entity>().GetCapability<Health>();
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
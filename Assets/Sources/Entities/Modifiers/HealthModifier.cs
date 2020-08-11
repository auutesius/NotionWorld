using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;

namespace NotionWorld.Entities
{
    public sealed class HealthModifier : Modifier
    {
        public int Delta
        {
            get; set;
        }

        public HealthModifier(int delta)
        {
            Delta = delta;
        }

        public override void TakeEffect(Entity entity)
        {
            Health health = entity.GetCapability<Health>();
            if(health != null)
            {
                health.Value += Delta;
            }
        }
    }

}
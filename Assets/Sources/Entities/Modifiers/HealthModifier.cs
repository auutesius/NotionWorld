using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Events;
using System.Threading.Tasks;

namespace NotionWorld.Entities
{
    public sealed class HealthModifier : Modifier
    {
        private ComboEvenArg evenArg;

        public int Delta
        {
            get; set;
        }

        public HealthModifier(int delta)
        {
            evenArg = new ComboEvenArg(1);
            Delta = delta;
        }

        public override void TakeEffect(Entity entity)
        {
            Health health = entity.GetCapability<Health>();
            if(health != null)
            {
                health.Value += Delta;
                if (Delta < 0 && entity.CompareTag("Enemies"))
                {
                    EventCenter.DispatchEvent(evenArg);
                }
            }
        }
}

}
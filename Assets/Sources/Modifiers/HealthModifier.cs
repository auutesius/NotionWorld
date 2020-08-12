using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;

namespace NotionWorld.Modifiers
{
    public sealed class HealthModifier : Modifier
    {
        public Health Health
        {
            get; set;
        }

        public int DeltaValue
        {
            get; set;
        }

        public override void TakeEffect()
        {
            if(Health != null)
            {
                Health.Value += DeltaValue;
            }
        }
    }

}
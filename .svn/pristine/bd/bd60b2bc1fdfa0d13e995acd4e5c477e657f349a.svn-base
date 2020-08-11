using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;

namespace NotionWorld.Entities
{
    public sealed class SpeedModifier : RollBackModifier
    {
        public float Delta
        {
            get; set;
        }

        public SpeedModifier(float delta)
        {
            Delta = delta;
        }

        public override void TakeEffect(Entity entity)
        {
            Speed speed = entity.GetCapability<Speed>();
            if (speed != null)
            {
                speed.Value += Delta;
            }
        }

        public override void RollBackEffect(Entity entity)
        {
            Speed speed = entity.GetCapability<Speed>();
            if (speed != null)
            {
                speed.Value -= Delta;
            }
        }
    }

}
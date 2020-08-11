using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Entities;

namespace NotionWorld.Buffs
{
    public sealed class SlowDown : TimeBoundBuff
    {
        public float Value
        {
            get;
        }

        private SpeedModifier modifier;

        public SlowDown(float value)
        {
            Value = value;
            modifier = new SpeedModifier(-value);
        }

        public override void OnAdded(Entity entity)
        {
            modifier.TakeEffect(entity);
        }

        public override void OnRemoved(Entity entity)
        {
            modifier.RollBackEffect(entity);
        }
    }

}
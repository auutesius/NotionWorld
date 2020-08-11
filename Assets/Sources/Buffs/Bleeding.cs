using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Entities;

namespace NotionWorld.Buffs
{
    public sealed class Bleeding : TimeBoundBuff
    {
        public int Value
        {
            get;
        }

        private HealthModifier modifier;

        private float valueDecimal;

        public Bleeding(int value)
        {
            Value = value;
            modifier = new HealthModifier(-value);
        }

        public override void OnTick(Entity entity, float deltaTime)
        {
            valueDecimal = deltaTime * Value;
            if(valueDecimal > 1)
            {
                modifier.Delta = Mathf.RoundToInt(valueDecimal);
                modifier.TakeEffect(entity);
                valueDecimal -= modifier.Delta;
            }       
        }
    }

}
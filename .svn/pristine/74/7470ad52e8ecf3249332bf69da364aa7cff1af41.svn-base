using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Events;
using NotionWorld.Entities;

namespace NotionWorld.Buffs
{
    public abstract class Buff
    {
        public virtual void OnAdded(Entity entity)
        {

        }

        public virtual void OnRemoved(Entity entity)
        {

        }
    }

    public abstract class TimeBoundBuff : Buff
    {
        public float Time
        {
            get; set;
        }

        public virtual void OnTick(Entity entity, float deltaTime)
        {

        }
    }
}
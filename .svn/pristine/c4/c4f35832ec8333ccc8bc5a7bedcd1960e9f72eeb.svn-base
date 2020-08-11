using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Buffs;
using NotionWorld.Events;
using NotionWorld.Entities;

namespace NotionWorld.Capabilities
{
    public sealed class BuffList : Capability, ISubscriber<SecondUpdateEventArgs>
    {
        private Entity entity;

        private List<Buff> buffs = new List<Buff>();

        public T GetBuff<T>() where T : Buff
        {
            for (int i = 0; i < buffs.Count; i++)
            {
                if (buffs[i].GetType() == typeof(T))
                {
                    return buffs[i] as T;
                }
            }
            return default;
        }

        public void SetBuff<T>(T buff) where T : Buff
        {
            for (int i = 0; i < buffs.Count; i++)
            {
                if (buffs[i].GetType() == typeof(T))
                {
                    buffs[i].OnRemoved(entity);
                    buffs[i] = buff;
                    buff.OnAdded(entity);
                    return;
                }
            }
            buffs.Add(buff);
            buff.OnAdded(entity);
        }

        public bool RemoveBuff<T>() where T : Buff
        {
            for (int i = 0; i < buffs.Count; i++)
            {
                if (buffs[i].GetType() == typeof(T))
                {
                    buffs[i].OnRemoved(entity);
                    buffs.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public override void Initialize(Entity entity)
        {
            this.entity = entity;
            EventCenter.Subscribe(this);
        }

        public void OnEventOccurred(SecondUpdateEventArgs e)
        {
            for (int i = 0; i < buffs.Count; i++)
            {
                if (buffs[i].GetType().IsSubclassOf(typeof(TimeBoundBuff)))
                {
                    var buff = buffs[i] as TimeBoundBuff;
                    buff.Time -= e.DeltaTime;
                    if (buff.Time <= 0)
                    {
                        buffs[i].OnRemoved(entity);
                        buffs.RemoveAt(i);
                    }
                }
            }
        }
    }

}
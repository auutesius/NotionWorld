using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;
using NotionWorld.Worlds;
using System.Threading.Tasks;
using Unity.Mathematics;

namespace NotionWorld.Actions
{
    public sealed class MoveTowardFragment : Fragment
    {
        public float InternalTime;
        public Vector2 Direction;
        public float Speed;

        public override void TakeEffect(Entity entity)
        {
            Delay(50,entity);
        }

        private async void Delay(int ms, Entity actor)
        {
            int step = 0;
            while (step < (int)(InternalTime * 1000 / ms))
            {
                await Task.Delay(ms);
                if(actor != null)
                actor.transform.Translate(Speed * (Vector3)Direction.normalized);
                step++;
            }
        }
    }
}

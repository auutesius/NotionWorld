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
    public sealed class RushAttackFragment : Fragment
    {
        public int InternalTime;
        public Vector2 RushDir;
        public override void TakeEffect(Entity actor)
        {
            actor.GetComponent<WeaponController>().SetCollider(true);
            actor.GetComponent<WeaponController>().WeaponLookAt(RushDir);
            Delay(InternalTime * 1000, actor); 
        }
        private async void Delay(int ms, Entity actor)
        {
            await Task.Delay(ms);
            actor.GetComponent<WeaponController>().SetCollider(false);
        }
    }

}

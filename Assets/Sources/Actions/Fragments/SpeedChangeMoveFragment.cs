using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;
using NotionWorld.Worlds;
using System.Threading.Tasks;

namespace NotionWorld.Actions
{
    public sealed class SpeedChangeMoveFragment : Fragment
    {
        SpeedModifier speedModifier;
        public float InternalTime;
        public override void TakeEffect(Entity actor)
        {

            speedModifier = new SpeedModifier(actor.GetCapability<Speed>().Value * 0.5f);
            speedModifier.TakeEffect(actor);
            Delay((int)(InternalTime * 1000), actor); 

        }
        private async void Delay(int ms, Entity actor)
        {
            await Task.Delay(ms);
            speedModifier.RollBackEffect(actor);
        }
    }

}

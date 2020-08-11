using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;
using NotionWorld.Worlds;
using System.Threading.Tasks;

namespace NotionWorld.Actions
{
    public sealed class RushFragment : Fragment
    {
        SpeedModifier speedModifier;
        public int InternalTime;
        public override void TakeEffect(Entity actor)
        {
            // TODO：需要禁用玩家输入
            speedModifier = new SpeedModifier(actor.GetCapability<Speed>().Value);

            speedModifier.TakeEffect(actor);
            Delay(InternalTime * 1000, actor); 

        }
        private async void Delay(int ms, Entity actor)
        {
            await Task.Delay(ms);
            speedModifier.RollBackEffect(actor);
        }
    }

}

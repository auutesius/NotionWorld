using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;
using NotionWorld.Worlds;
using System.Threading.Tasks;
using BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion;

namespace NotionWorld.Actions
{
    public sealed class StopPlayerStateFragment : Fragment
    {
        public int SkillInternal;

        public override void TakeEffect(Entity actor)
        {
            actor.gameObject.GetComponent<WeaponController>().IsSkilling = true;
            Delay(50, actor);
        }
        private async void Delay(int ms, Entity actor)
        {
            int step = 0;
            while (step < SkillInternal * 1000 / ms)
            {
                step++;
                await Task.Delay(ms);
            }
            actor.gameObject.GetComponent<WeaponController>().IsSkilling = false;
        }
    }
}
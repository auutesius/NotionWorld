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
    public sealed class StopAutoAttackFragment : Fragment
    {
        public int SkillInternal;

        public StopAutoAttackFragment(int skillInternal)
        {
            SkillInternal = skillInternal;
        }
        public override void TakeEffect(Entity actor)
        {
            Delay(SkillInternal * 1000 /5, actor);
        }
        private async void Delay(int ms, Entity actor)
        {
            actor.gameObject.GetComponent<WeaponController>().IsSkilling = true;
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
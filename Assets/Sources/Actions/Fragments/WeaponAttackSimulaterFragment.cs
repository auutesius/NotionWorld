using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;
using System.Threading.Tasks;

namespace NotionWorld.Actions
{
    public sealed class WeaponAttackSimulaterFragment : Fragment
    {
        public Vector3 AttackDir;
        public float AttackPerformanceTime;
        private WeaponController WC;
        private float SkillInternal;
        public override void TakeEffect(Entity actor)
        {
            WC = actor.GetComponent<WeaponController>();
            var attack = actor.GetComponent<Entity>().GetCapability<Attack>();
            if (WC == null)
            {
                throw new InvalidOperationException("Entity without WeaponController can not do AttackAction.");
            }
            if (attack == null)
            {
                throw new InvalidOperationException("Entity without Attack can not do AttackAction.");
            }
            SkillInternal = attack.Interval;
            Delay(67,actor);
        }
        private async void Delay(int ms, Entity actor)
        {
            Transform WeaponImage = WC.weapon.transform.GetChild(0).transform;

            int step = 0;
            float Count = SkillInternal * 1000 / ms;
            float attackAngle = 100f * (AttackDir.x > 0 ? 1f : -1f);      // 挥砍幅度（度）
            float attackInternal = Count * 0.1f;   // 挥砍动作所占时间
            float prepareInternal = (Count - attackInternal) / 2;
            while (step < Count)
            {
                if (actor == null) { break; }
                if (step < prepareInternal)
                {
                    WeaponImage.rotation = WeaponImage.rotation * Quaternion.Euler(new Vector3(0f, 0f, (attackAngle / 2) / prepareInternal));
                }
                else if(step < attackInternal + prepareInternal)
                {
                    WeaponImage.rotation = WeaponImage.rotation * Quaternion.Euler(new Vector3(0f, 0f, -(attackAngle) / attackInternal));
                }
                else if (step < attackInternal + prepareInternal*2)
                {
                    WeaponImage.rotation = WeaponImage.rotation * Quaternion.Euler(new Vector3(0f, 0f, (attackAngle / 2) / prepareInternal));
                }
                step++;
                await Task.Delay(ms);
            }
        }
    }

    

}
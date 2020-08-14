using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;
using System.Threading.Tasks;

namespace NotionWorld.Actions
{
    public sealed class WeaponAttackAnimator : Fragment
    {
        private float SkillInternal;
        public bool IsLeftAttack;
        private Animator animator;
        public override void TakeEffect(Entity actor)
        {
            animator = actor.GetComponent<WeaponController>().weapon.transform.GetChild(0).GetComponent<Animator>();
            var attack = actor.GetComponent<Entity>().GetCapability<Attack>();
            if (animator == null)
            {
                throw new InvalidOperationException("Entity without Weapon can not do AttackAction.");
            }
            if (attack == null)
            {
                throw new InvalidOperationException("Entity without Attack can not do AttackAction.");
            }
            if (IsLeftAttack)
            {
                animator.SetBool("LeftAttack", true);
            }
            else
            {
                animator.SetBool("RightAttack", true);
            }
            SkillInternal = attack.Interval;
            Delay((int)(SkillInternal * 1000), actor);
        }
        private async void Delay(int ms, Entity actor)
        {
            await Task.Delay(ms);
            if (IsLeftAttack)
            {
                animator.SetBool("LeftAttack", false);
            }
            else
            {
                animator.SetBool("RightAttack", false);
            }
        }
    }



}
using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;
using System.Threading.Tasks;

namespace NotionWorld.Actions
{
    public sealed class WeaponAnimatorFrament : Fragment
    {
        public override void TakeEffect(Entity actor)
        {
            Vector3 pos = actor.transform.position;
            Attack attack = actor.GetCapability<Attack>();
            if (attack == null)
            {
                throw new InvalidOperationException("Entity without Attack capability can not do AttackAction.");
            }
            Animator animator = actor.gameObject.GetComponent<WeaponController>().weapon.GetComponent<Animator>();
            animator.SetBool("isAttacking",true);
            Delay((int)((attack.Interval - animator.gameObject.GetComponent<AnimatorTimeInfo>().AttackInternal) * 1000), animator);
        }
        private async void Delay(int ms,Animator a)
        {
            await Task.Delay(ms);
            a.SetBool("isAttacking", false);
        }
    }

    

}
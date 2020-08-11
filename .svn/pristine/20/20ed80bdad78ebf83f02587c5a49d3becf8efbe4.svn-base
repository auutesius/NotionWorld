using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;
using System.Threading.Tasks;

namespace NotionWorld.Actions
{
    public sealed class RotateAttackAnimatorFragment : Fragment
    {
        public float InteralTime;
        public override void TakeEffect(Entity actor)
        {
            Animator animator = actor.gameObject.GetComponent<WeaponController>().weapon.GetComponent<Animator>();
            animator.SetBool("isRotateAttacking", true);
            Delay((int)(InteralTime * 1000), animator);
        }
        private async void Delay(int ms, Animator a)
        {
            await Task.Delay(ms);
            a.SetBool("isRotateAttacking", false);
        }
    }



}
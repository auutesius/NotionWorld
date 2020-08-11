using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using System.Collections;
using System.Threading.Tasks;

namespace NotionWorld.Actions
{
    public sealed class AttackAction : EntityAction
    {
        public int AttackDelayOffset;
        public Vector3 AttackDir;
        public string AttackTag; 

        private AttackFragment attack;
        private WeaponAnimatorFrament weaponAnimator;

        public override void TakeAction(Entity entity)
        {            
            if (weaponAnimator == null)
            {
                weaponAnimator = new WeaponAnimatorFrament();
            }
            weaponAnimator.TakeEffect(entity);
            if (attack == null)
            {
                attack = new AttackFragment();
            }
            attack.AttackTag = AttackTag;
            attack.AttackDir = AttackDir;
            attack.TakeEffect(entity);
        }

    }

}
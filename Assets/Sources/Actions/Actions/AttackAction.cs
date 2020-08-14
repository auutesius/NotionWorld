using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using System.Collections;
using System.Threading.Tasks;
using NotionWorld.Modifiers;

namespace NotionWorld.Actions
{
    public sealed class AttackAction : EntityAction
    {
        public int AttackDelayOffset;
        public Vector3 AttackDir;
        public string AttackTag;

        private AttackFragment attack;
        private WeaponAttackSimulaterFragment weaponAnimator;
        private AudioCreateFragment audioCreateFragment;

        public override void TakeAction(Entity entity)
        {            
            if (weaponAnimator == null)
            {
                weaponAnimator = new WeaponAttackSimulaterFragment();
            }
            weaponAnimator.AttackDir = AttackDir;
            weaponAnimator.TakeEffect(entity);


            if (attack == null)
            {
                attack = new AttackFragment();
                attack.AttackTag = AttackTag;
            }
            attack.AttackDir = AttackDir;
            attack.TakeEffect(entity);


            if (audioCreateFragment == null)
            {
                audioCreateFragment = new AudioCreateFragment();
                audioCreateFragment.PlayInternal = entity.GetCapability<Attack>().Interval / 2;
                audioCreateFragment.AudioName = string.Format("{0}Attack", entity.name);
            }
            audioCreateFragment.TakeEffect(entity);

        }

    }

}
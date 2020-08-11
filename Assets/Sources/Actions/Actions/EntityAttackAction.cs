using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using System.Collections;
using System.Threading.Tasks;

namespace NotionWorld.Actions
{
    public sealed class EntityAttackAction : EntityAction
    {
        public GameObject Target
        {
            get; set;
        }

        public Animator AttackAnimator
        {
            get; set;
        }

        private EntityAttackFragment attack = new EntityAttackFragment();

        private AnimatorParameterFragment animatorFragment = new AnimatorParameterFragment()
        {
            Name = "IsAttacking",
            Value = true
        };

        public override void TakeAction(Entity entity)
        {
            attack.Target = Target;
            attack.TakeEffect(entity);
            animatorFragment.Animator = AttackAnimator;
            animatorFragment.TakeEffect();
        }

    }

}
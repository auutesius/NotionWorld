using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Entities;

namespace NotionWorld.Actions
{
    public sealed class MoveAction : EntityAction
    {
        public Vector3 Movement
        {
            get; set;
        }

        private MoveFragment move;
        private AnimatorParameterFragment animatorFragment;
        private AnimatorDirFragment dir;

        public override void TakeAction(Entity entity)
        {
            if (move == null)
            {
                move = new MoveFragment();
            }
            move.Movement = Movement;
            move.TakeEffect(entity);

            if (animatorFragment == null)
            {
                animatorFragment = new AnimatorParameterFragment();
                animatorFragment.Name = "Move";
                animatorFragment.Animator = entity.transform.GetChild(0).GetComponent<Animator>();
            }
            if (dir == null)
            {
                dir = new AnimatorDirFragment();
            }
            if (animatorFragment.Animator != null)
            {
                animatorFragment.Value = Movement.magnitude > 0.01f;
                animatorFragment.TakeEffect();
                dir.Movement = Movement;
                dir.TakeEffect(entity);
            }

        }
    }

}
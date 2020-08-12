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

        public override void TakeAction(Entity entity)
        {
            if (move == null)
            {
                move = new MoveFragment();
            }
            move.Movement = Movement;
            move.TakeEffect(entity);
        }
    }

}
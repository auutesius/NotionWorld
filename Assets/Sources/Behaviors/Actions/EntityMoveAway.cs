using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using NotionWorld.Entities;
using UnityEngine;

namespace NotionWorld.Behaviors
{
    [TaskCategory("NotionWorld")]
    public sealed class EntityMoveAway : Action
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("The GameObject that tracks.")]
        public SharedGameObject targetGameObject;

        private EntityMovement movement; 

        public override void OnAwake()
        {
            movement = Owner.GetComponent<EntityMovement>();
        }

        public override void OnFixedUpdate()
        {
            Vector3 direction = transform.position - targetGameObject.Value.transform.position;
            direction = direction.normalized;

            movement.Direction = direction;
        }
    }

}
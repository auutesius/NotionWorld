using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace NotionWorld.Behaviors
{
    [TaskCategory("NotionWorld")]
    public sealed class EntityAutoAttack : Action
    {
        private WeaponController weaponController;

        private GameObject attackTarget;

        public override void OnAwake()
        {
            weaponController = Owner.GetComponent<WeaponController>();
        }

        public override void OnFixedUpdate()
        {
            if(attackTarget == null)
            {
                attackTarget = weaponController.CheckAttackTarget();               
            }
            else
            {
                weaponController.forward = attackTarget.transform.position - transform.position;
            }
        }
    }

}
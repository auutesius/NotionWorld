using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;
using NotionWorld.Worlds;
using System.Threading.Tasks;
using BehaviorDesigner.Runtime.Tasks.Basic.UnityQuaternion;

namespace NotionWorld.Actions
{
    public sealed class RotateAttackFragment : Fragment
    {
        AttackFragment attackFragment = new AttackFragment();
        public Vector3 AttackDir;
        public string AttackTag;
        public int SkillInternal;
        public int RotateSpeed;
        public override void TakeEffect(Entity actor)
        {
            AttackDir = actor.transform.up;
            AttackTag = actor.gameObject.GetComponent<WeaponController>().AttackTag;
            actor.GetComponent<WeaponController>().SetCollider(true);
            Delay((int)50/RotateSpeed, actor);
        }
        private async void Delay(int ms, Entity actor)
        {
            int step = 0;
            while (step < SkillInternal * 1000 / ms)
            {
                step++;
                await Task.Delay(ms);
                float euler = AttackDir.y > 0 ? -(Mathf.Atan(AttackDir.x / AttackDir.y)) * 180 / Mathf.PI : -((Mathf.Atan(AttackDir.x / AttackDir.y)) * 180 / Mathf.PI - 180);

                AttackDir = Quaternion.AngleAxis(10f, Vector3.forward) * AttackDir;
                actor.gameObject.GetComponent<WeaponController>().WeaponLookAt(AttackDir);
            }
        }
    }

}
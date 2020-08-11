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
    public sealed class AreaAttackFragment : Fragment
    {
        public int SkillInternal;
        public int Damage;
        public override void TakeEffect(Entity actor)
        {
            Delay(50, actor);
        }
        private async void Delay(float ms, Entity actor)
        {
            int step = 0;
            List<Collider2D> HurtTargets = new List<Collider2D>();
            do
            {
                step++;
                await Task.Delay((int)ms);
                if (step == SkillInternal * 500 / ms)
                {
                    HurtTargets.Clear();
                }
                Collider2D[] cols = Physics2D.OverlapCircleAll(actor.transform.position, actor.GetComponent<Entity>().GetCapability<Attack>().Range);
                if (cols.Length > 0)
                {
                    List<GameObject> Targets = new List<GameObject>();
                    foreach (var c in cols)
                    {
                        if (c.transform.CompareTag(actor.transform.GetComponent<WeaponController>().AttackTag) && !HurtTargets.Contains(c))
                        {
                            Targets.Add(c.gameObject);
                            HurtTargets.Add(c);
                        }
                    }
                    if (Targets.Count != 0)
                    {
                        HealthModifier healthModifier = new HealthModifier(-Damage);
                        MoveTowardFragment moveTowardFragment = new MoveTowardFragment();
                        foreach (var t in Targets)
                        {
                            moveTowardFragment.Direction = ( t.transform.position - actor.transform.position);
                            moveTowardFragment.InternalTime = 0.2f;
                            moveTowardFragment.Speed = 0.1f;
                            moveTowardFragment.TakeEffect(t.GetComponent<Entity>());
                            healthModifier.TakeEffect(t.GetComponent<Entity>());

                        }
                    }
                }
            } while (step < SkillInternal * 1000 / ms) ;
        }
    }

}
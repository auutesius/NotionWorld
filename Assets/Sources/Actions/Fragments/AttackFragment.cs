using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;
using NotionWorld.Worlds;
using System.Threading.Tasks;

namespace NotionWorld.Actions
{
    public sealed class AttackFragment : Fragment
    {
        public Vector3 AttackDir;
        public string AttackTag;
        public override void TakeEffect(Entity actor)
        {
            Vector3 pos = actor.transform.position;
            Attack attack = actor.GetCapability<Attack>();
            if (attack == null)
            {
                throw new InvalidOperationException("Entity without Attack capability can not do AttackAction.");
            }
            Delay((int)(actor.GetCapability<Attack>().Interval * 0.5F) * 1000, actor);

        }
        private async void Delay(int ms, Entity actor)
        {
            Debug.Log("start" + Time.time);
            await Task.Delay(ms);
            Debug.Log("end" + Time.time);

            float euler = AttackDir.y > 0 ? -(Mathf.Atan(AttackDir.x / AttackDir.y)) * 180 / Mathf.PI : -((Mathf.Atan(AttackDir.x / AttackDir.y)) * 180 / Mathf.PI - 180);
            GameObject bullet = ObjectPool.GetObject(actor.GetCapability<Attack>().AttackType, "Bullets");
            bullet.GetComponent<Bullet>().Speed = actor.GetCapability<Attack>().BulletSpeed;
            bullet.GetComponent<Bullet>().ActiveIt(actor.transform.position, new Vector3(0f, 0f, euler), actor.GetCapability<Attack>().Value, AttackTag);
        }
    }

}
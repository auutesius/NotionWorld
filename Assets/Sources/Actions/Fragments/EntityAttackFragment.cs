using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;
using NotionWorld.Worlds;
using System.Threading.Tasks;

namespace NotionWorld.Actions
{
    public sealed class EntityAttackFragment : Fragment
    {
        public GameObject Target
        {
            get; set;
        }//我寻思这里本不用传入Entity的Attack

        public override void TakeEffect(Entity entity)
        {
            Attack attack = entity.GetCapability<Attack>();
            if (attack == null)
            {
                throw new InvalidOperationException("Entity without Attack capability can not do AttackAction.");
            }
            Vector2 direction = Target.transform.position - entity.transform.position;
            direction = direction.normalized;
            LaunchBullet(attack, entity.transform.position, direction);
        }

        private void LaunchBullet(Attack attack, Vector2 position, Vector2 direction)
        {
            float euler = direction.y > 0 ? -(Mathf.Atan(direction.x / direction.y)) * 180 / Mathf.PI : -((Mathf.Atan(direction.x / direction.y)) * 180 / Mathf.PI - 180);

            GameObject bullet = ObjectPool.GetObject(attack.AttackType, "Bullets");
            bullet.GetComponent<Bullet>().Speed = attack.BulletSpeed;
            //bullet.GetComponent<Bullet>().ActiveIt(position, new Vector3(0f, 0f, euler), attack.Value, Target.tag);
        }
    }

}
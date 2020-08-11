using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;
using System.Threading.Tasks;
using NotionWorld.Worlds;

namespace NotionWorld.Actions
{
    public sealed class BombFragment : Fragment
    {
        public string AttackTag;
        public float BulletSpeed;
        public Vector3 TargetPos;
        public int Damage;
        public override void TakeEffect(Entity actor)
        {
            Vector3 AttackDir = TargetPos - actor.transform.position;
            float euler = AttackDir.y > 0 ? -(Mathf.Atan(AttackDir.x / AttackDir.y)) * 180 / Mathf.PI : -((Mathf.Atan(AttackDir.x / AttackDir.y)) * 180 / Mathf.PI - 180);
            GameObject bullet = ObjectPool.GetObject("BombBullet", "Bullets");
            bullet.GetComponent<Bullet>().Speed = BulletSpeed;
            bullet.GetComponent<Bullet>().ActiveIt(actor.transform.position, new Vector3(0f, 0f, euler), Damage, AttackTag);

        }
    }



}
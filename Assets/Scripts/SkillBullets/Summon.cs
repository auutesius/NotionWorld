using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Modifiers;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using BehaviorDesigner.Runtime;
using ObjectPool = NotionWorld.Worlds.ObjectPool;

public sealed class Summon : SkillBullet
{
    public string gameObjectName;

    public Vector2[] points;


    public override void Launch(Vector2 position, Vector2 direction)
    {
        Spawns();
    }

    private void Spawns()
    {
        foreach (var point in points)
        {
            var bulletObj = ObjectPool.GetObject("Spawn", "SkillBullets");
            var bullet = bulletObj.GetComponent<Spawn>();
            bullet.Target = Target;
            bullet.gameObjectName = gameObjectName;
            bullet.point = point;
            bullet.Launch(point, Vector2.zero);
        }
        ObjectPool.RecycleObject(this.gameObject);
    }
}
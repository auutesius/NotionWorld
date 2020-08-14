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
        foreach (var point in points)
        {
            SpawnEnemy(gameObjectName, point);
        }
        ObjectPool.RecycleObject(this.gameObject);
    }

    private void SpawnEnemy(string id, Vector2 position)
    {
        var enemy = ObjectPool.GetObject(id, "Entities");

        enemy.transform.position = position;

        Behavior behavior = enemy.GetComponent<Behavior>();
        if (behavior != null)
        {
            behavior.SetVariableValue("TrackTarget", Target);
            behavior.EnableBehavior();
            behavior.Start();
        }
    }
}
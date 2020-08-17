using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Modifiers;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using BehaviorDesigner.Runtime;
using ObjectPool = NotionWorld.Worlds.ObjectPool;

public sealed class Spawn : SkillBullet
{
    public string gameObjectName;

    public Vector2 point;

    public float spawningTime;

    private WaitForSeconds spawning;

    private void Awake()
    {
        spawning = new WaitForSeconds(spawningTime);
    }

    public override void Launch(Vector2 position, Vector2 direction)
    {
        transform.position = point;
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        yield return spawning;
        var enemy = ObjectPool.GetObject(gameObjectName, "Entities");
        enemy.transform.position = point;

        Behavior behavior = enemy.GetComponent<Behavior>();
        if (behavior != null)
        {
            behavior.SetVariableValue("TrackTarget", Target);
            behavior.EnableBehavior();
            behavior.Start();
        }
        ObjectPool.RecycleObject(this.gameObject);
    }
}
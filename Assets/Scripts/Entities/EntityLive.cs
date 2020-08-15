using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;
using NotionWorld.Worlds;

public class EntityLive : MonoBehaviour
{

    private Health health;


    void Start()
    {
        health = GetComponent<Entity>().GetCapability<Health>();
    }

    void Update()
    {
        if (health.Value < 0)
        {
            health.Value = health.MaxValue;
            ObjectPool.RecycleObject(gameObject);
        }
    }
}

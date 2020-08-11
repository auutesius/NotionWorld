using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using NotionWorld.Worlds;

public class HealthController : MonoBehaviour
{
    Entity entity;
    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponent<Entity>();
    }

    public void EditHealth(int value)
    {
        var health = entity.GetCapability<Health>();
        health.Value += value;
        if (health.Value <= 0)
        {
            Debug.Log(name + "死掉了");
            ObjectPool.RecycleObject(gameObject);
        }
    }


}

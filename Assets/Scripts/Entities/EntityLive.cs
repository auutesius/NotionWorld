using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;
using NotionWorld.Worlds;

public class EntityLive : MonoBehaviour
{
    private Health health;

    private int lastValue;

    public AudioSource source;

    public AudioClip clip;

    void Start()
    {
        health = GetComponent<Entity>().GetCapability<Health>();
        lastValue = health.Value;
    }

    void Update()
    {
        if(lastValue != health.Value)
        {
            lastValue = health.Value;
            source.PlayOneShot(clip);
        }

        if (health.Value < 0)
        {
            health.Value = health.MaxValue;
            ObjectPool.RecycleObject(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;
using NotionWorld.Worlds;

public class PlayerLive : MonoBehaviour
{
    private Health health;

    private float endingTime = 2F;

    private float endingScale = 0.5F;

    void Start()
    {
        health = GetComponent<Entity>().GetCapability<Health>();
    }

    void Update()
    {
        if (health.Value < 0)
        {
            health.Value = health.MaxValue;
            
            StartCoroutine(EndCorotinue());
        }
    }

    private IEnumerator EndCorotinue()
    {
        Time.timeScale = endingScale;
        yield return new WaitForSeconds(endingTime);
        Time.timeScale = 1;
    }
}

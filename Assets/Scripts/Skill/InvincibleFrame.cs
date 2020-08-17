using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;
using NotionWorld.Worlds;
using NotionWorld.Actions;
public class InvincibleFrame : MonoBehaviour
{

    private Health health;
    private float currHealth;


    void Start()
    {
        
        health = GetComponent<Entity>().GetCapability<Health>();
        currHealth = health.Value;
    }

    void Update()
    {
        if(health.Value < currHealth){
        InvincibleFragment invincibleFragment = new InvincibleFragment();
        invincibleFragment.InternalTime = 0.2f;   // 无敌时间
        invincibleFragment.TakeEffect(GetComponent<Entity>());
        }
        currHealth = health.Value;
    }
    
}

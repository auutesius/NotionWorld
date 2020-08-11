
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Entities;
using NotionWorld.Capabilities;

public class PlayerMovement : MonoBehaviour
{
    Speed speed;
    Entity entity;
    [HideInInspector] public Vector2 dest = Vector2.zero;
    

    void Start()
    {
        
        entity = GetComponent<Entity>();
        speed = entity.GetCapability<Speed>();
    }

    void FixedUpdate()
    {
        
        if (dest != Vector2.zero && DetectCollider())
        {
            transform.Translate(0.0005f * dest * speed.Value);
        }
    }


    bool DetectCollider()
    {
        float halfSize = GetComponent<BoxCollider2D>().size.x / 2 * transform.localScale.x;
        return Physics2D.Raycast((Vector2)transform.position, dest, halfSize + speed.Value, 1);
    }

}
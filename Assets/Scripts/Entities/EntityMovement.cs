
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Actions;
using NotionWorld.Entities;
using NotionWorld.Capabilities;

public class EntityMovement : MonoBehaviour
{
    public bool IsSkilling;

    private Speed speed;

    private Entity entity;

    public Vector2 Direction
    {
        get; set;
    }

    private MoveAction action;

    private void Awake()
    {
        entity = GetComponent<Entity>();
        action = new MoveAction();
    }

    void Start()
    {
        speed = entity.GetCapability<Speed>();
    }

    void FixedUpdate()
    {
        if (Direction != Vector2.zero && !IsSkilling)
        {
            action.Movement = Time.fixedDeltaTime * Direction * speed.Value;
            action.TakeAction(entity);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Modifiers;

public abstract class SkillBullet : MonoBehaviour
{
    public GameObject Source
    {
        get; set;
    }

    public GameObject Target
    {
        get; set;
    }

    public float coldDown;
    
    protected virtual void OnSkillReleased()
    {

    }

    protected virtual void OnSkillHit()
    {

    }

    public abstract void Launch(Vector2 position, Vector2 direction);
}

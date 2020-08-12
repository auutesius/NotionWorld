using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;

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

    protected Modifier[] sourceModifiers;

    protected Modifier[] targetModifiers;
    
    protected virtual void OnSkillReleased()
    {

    }

    protected virtual void OnSkillHit()
    {

    }

    public abstract void Launch(Vector3 position, Vector3 direction);
}

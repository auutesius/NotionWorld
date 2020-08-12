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
    
    public virtual void OnSkillReleased()
    {

    }

    public virtual void OnSkillHit()
    {

    }
}

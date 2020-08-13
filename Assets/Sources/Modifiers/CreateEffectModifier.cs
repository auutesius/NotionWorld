using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Worlds;

namespace NotionWorld.Modifiers
{
    public sealed class CreatEffectModifier : Modifier
    {
        public string EffectName
        {
            get; set;
        }

        public Transform HitPoint
        {
            get; set;
        }

        public override void TakeEffect()
        {
            if (EffectName != null)
            {
                GameObject shootHitEffect = ObjectPool.GetObject(EffectName, "Effects");
                shootHitEffect.transform.position = HitPoint.position + HitPoint.up.normalized * 0.5f + Vector3.forward * -5f;
            }
        }
    }
}
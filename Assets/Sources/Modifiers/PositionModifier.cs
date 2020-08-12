using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;

namespace NotionWorld.Modifiers
{
    public sealed class PositionModifier : Modifier
    {
        public Vector3 DeltaPosition
        {
            get; set;
        }

        public Transform Transform
        {
            get; set;
        }

        public override void TakeEffect()
        {
            Transform.position += DeltaPosition;
        }
    }

}
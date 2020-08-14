using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;

namespace NotionWorld.Modifiers
{
    public sealed class RotationModifier : Modifier
    {
        public Vector3 DeltaRotation
        {
            get; set;
        }

        public Transform Transform
        {
            get; set;
        }

        public override void TakeEffect()
        {
            if(Transform != null)
            {
                Transform.rotation *= Quaternion.Euler(DeltaRotation);
            }
        }
    }

}
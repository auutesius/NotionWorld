using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NotionWorld.Capabilities
{
    public sealed class Energy : Capability
    {
        public float Value
        {
            get; set;
        }
        public float MaxValue
        {
            get; set;
        }

        public float Recharge
        {
            get; set;
        }
    }

}
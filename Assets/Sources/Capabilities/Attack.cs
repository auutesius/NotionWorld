using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NotionWorld.Capabilities
{
    public sealed class Attack : Capability
    {
        public int Value
        {
            get; set;
        }

        public float Interval
        {
            get; set;
        }

        public float Range
        {
            get; set;
        }

        public string AttackType
        {
            get; set;
        }
        
        public float BulletSpeed
        {
            get; set;
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NotionWorld.Capabilities
{
    public sealed class Health : Capability
    {
        public int Value
        {
            get; set;
        }

        public int MaxValue
        {
            get;set;
        }
    }

}
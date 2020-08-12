using System;
using System.Collections.Generic;
using UnityEngine;

namespace NotionWorld.Events
{
    public class ComboEvenArg : EventArgs
    {
        public int Value
        {
            get; set;
        }

        public ComboEvenArg(int value)
        {
            Value = value;
        }
    }

}
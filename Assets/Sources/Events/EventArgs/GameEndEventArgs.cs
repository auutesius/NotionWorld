using System;
using System.Collections.Generic;
using UnityEngine;

namespace NotionWorld.Events
{
    public class GameEndEventArgs : EventArgs
    {
        public int Value
        {
            get; set;
        }

        public GameEndEventArgs(int value)
        {
            Value = value;
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NotionWorld.Events
{
    public class JoyStickMovedEventArgs : EventArgs
    {
        public Vector2 Vector
        {
            get; set;
        }

        public JoyStickMovedEventArgs(Vector2 vector)
        {
            Vector = vector;
        }
    }

}
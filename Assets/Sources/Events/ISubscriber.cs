using System;
using System.Collections.Generic;
using UnityEngine;

namespace NotionWorld.Events
{
    public interface ISubscriber<T> where T : EventArgs
    {
        void OnEventOccurred(T e);
    }
}

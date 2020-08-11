using System;
using System.Collections.Generic;
using UnityEngine;

namespace NotionWorld.Events
{
    public static class EventCenter
    {
        private static readonly Dictionary<Type, List<WeakReference>> eventHandlers = new Dictionary<Type, List<WeakReference>>();

        public static void DispatchEvent<T>(T e) where T : EventArgs
        {
            Type eventType = typeof(T);
            if (!eventHandlers.ContainsKey(eventType))
            {
                return;
            }
            var handlerList = eventHandlers[eventType];
            for (int i = 0; i < handlerList.Count; i++)
            {
                var target = handlerList[i].Target;
                if (target == null)
                {
                    handlerList.RemoveAt(i);
                }
                else
                {
                    (target as ISubscriber<T>).OnEventOccurred(e as T);
                }
            }
        }

        public static void Subscribe<T>(ISubscriber<T> subscriber) where T : EventArgs
        {
            Type eventType = typeof(T);
            List<WeakReference> handlerList;
            if(eventHandlers.ContainsKey(eventType))
            {
                handlerList  = eventHandlers[eventType];
            }
            else
            {
                handlerList = new List<WeakReference>();
                eventHandlers.Add(eventType, handlerList);
            }
            bool subscribed = false;
            for (int i = 0; i < handlerList.Count; i++)
            {
                var target = handlerList[i].Target;
                if (target == null)
                {
                    handlerList.RemoveAt(i);
                }
                else
                {
                    if (subscriber == target)
                    {
                        subscribed = true;
                        break;
                    }
                }
            }
            if(!subscribed)
            {
                handlerList.Add(new WeakReference(subscriber));
            }
        }

        public static void Unsubscribe<T>(ISubscriber<T> subscriber) where T : EventArgs
        {
            Type eventType = typeof(T);

            List<WeakReference> handlerList;
            if(eventHandlers.ContainsKey(eventType))
            {
                handlerList  = eventHandlers[eventType];
            }
            else
            {
                return;
            }
            for (int i = 0; i < handlerList.Count; i++)
            {
                var target = handlerList[i].Target;
                if (target != null)
                {
                    if (subscriber == target)
                    {
                        handlerList.RemoveAt(i);
                        break;
                    }
                }
                else
                {
                    handlerList.RemoveAt(i);
                }
            }
        }

        public static void ClearInvalidSubscribers()
        {
            foreach (Type type in eventHandlers.Keys)
            {
                var list = eventHandlers[type];
                for (int i = 0; i < list.Count; i++)
                {
                    if (!list[i].IsAlive)
                    {
                        list.RemoveAt(i);
                    }
                }
            }
        }

        public static void Clear()
        {
            eventHandlers.Clear();
        }
    }
}
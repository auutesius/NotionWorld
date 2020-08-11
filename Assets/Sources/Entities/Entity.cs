using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;

namespace NotionWorld.Entities
{
    public sealed class Entity : MonoBehaviour
    {
        private Capability[] capabilities;

        [SerializeField]
        public EntityArchetype archeType;

        private void Awake()
        {
            if (archeType == null)
            {
                throw new ArgumentNullException("ArcheType is null");
            }
            Initialize();
        }

        private void Initialize()
        {
            capabilities = archeType.CreateCapabilities();
            for (int i = 0; i < capabilities.Length; i++)
            {
                capabilities[i].Initialize(this);
            }
        }

        public T GetCapability<T>() where T : Capability
        {
            for (int i = 0; i < capabilities.Length; i++)
            {
                if (capabilities[i].GetType() == typeof(T))
                {
                    return capabilities[i] as T;
                }
            }
            return default;
        }
    }

}
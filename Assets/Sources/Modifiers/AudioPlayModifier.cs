using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;

namespace NotionWorld.Modifiers
{
    public sealed class AudioPlayModifier : Modifier
    {
        public AudioSource Audio
        {
            get; set;
        }

        public override void TakeEffect()
        {
            if (Audio != null)
            {
                Audio.Play();
            }
        }
    }
}
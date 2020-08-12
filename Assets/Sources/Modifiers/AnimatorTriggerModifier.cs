using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;

namespace NotionWorld.Modifiers
{
    public sealed class AnimatorTriggerModifier : Modifier
    {
        public string Name
        {
            get; set;
        }

        public Animator Animator
        {
            get; set;
        }

        public override void TakeEffect()
        {
            if (Name == null)
            {
                throw new ArgumentNullException("Parameter name is null.");
            }
            if(Animator != null)
            {
                Animator.SetTrigger(Name);
            }
        }
    }
}
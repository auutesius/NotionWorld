using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;

namespace NotionWorld.Modifiers
{
    public sealed class AnimatorParameterModifier : Modifier
    {
        public string Name
        {
            get; set;
        }

        public object Value
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
            if (Value == null)
            {
                Animator.SetTrigger(Name);
            }
            Type type = Value.GetType();

            if (type == typeof(bool))
            {
                Animator.SetBool(Name, (bool)Value);
            }
            else if (type == typeof(int))
            {
                Animator.SetInteger(Name, (int)Value);
            }
            else if (type == typeof(float))
            {
                Animator.SetFloat(Name, (float)Value);
            }
            else
            {
                throw new ArgumentException("Value is not a animator parameter.");
            }
        }
    }
}
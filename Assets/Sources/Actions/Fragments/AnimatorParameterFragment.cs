using System;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;

namespace NotionWorld.Actions
{
    public sealed class AnimatorParameterFragment
    {
        public string Name
        {
            get; set;
        }

        public object Value
        {
            get; set;
        }

        public void TakeEffect(Entity actor)
        {
            Animator animator = actor.GetComponent<Animator>();
            if(Name == null)
            {
                throw new ArgumentNullException("Parameter name is null.");
            }
            if(Value == null)
            {
                animator.SetTrigger(Name);
            }
            Type type = Value.GetType();

            if(type == typeof(bool))
            {
                animator.SetBool(Name, (bool)Value);
            }
            else if(type == typeof(int))
            {
                animator.SetInteger(Name, (int)Value);
            }
            else if(type == typeof(float))
            {
                animator.SetFloat(Name, (float)Value);
            }
            else
            {
                throw new ArgumentException("Value is not a animator parameter.");
            }
           
        }
    }

}
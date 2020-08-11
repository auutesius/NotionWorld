using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using NotionWorld.Entities;
using UnityEngine;

namespace NotionWorld.Behaviors
{
    [TaskCategory("NotionWorld")]
    public sealed class UseSkill : Action
    {
        public string SkillID;

        public override void OnAwake()
        {
            
        }

        public override TaskStatus OnUpdate()
        {
              return TaskStatus.Success;
        }
    }

}
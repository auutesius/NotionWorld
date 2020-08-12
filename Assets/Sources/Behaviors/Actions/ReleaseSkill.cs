using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using NotionWorld.Entities;
using UnityEngine;
using NotionWorld.Actions;
using NotionWorld.Capabilities;

namespace NotionWorld.Behaviors
{
    [TaskCategory("NotionWorld")]
    public sealed class ReleaseSkill : Action
    {
        [BehaviorDesigner.Runtime.Tasks.Tooltip("Skill Want to use.")]
        public string SkillID;

        private float coldDownTimer;

        public override void OnAwake()
        {

        }

        public override TaskStatus OnUpdate()
        {
            if (coldDownTimer <= 0)
            {
                //TODO: 发射SkillBullet
                //coldDownTimer = attack.Interval;
                return TaskStatus.Success;
            }
            else
            {
                coldDownTimer -= Time.deltaTime;
                return TaskStatus.Failure;
            }
        }
    }

}
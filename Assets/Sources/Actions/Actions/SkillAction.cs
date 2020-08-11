using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Entities;
using System;
using NotionWorld.Capabilities;

namespace NotionWorld.Actions
{
    public sealed class SkillAction : EntityAction
    {
        public string SkillType;
        // TODO 输入端未完成
        public Vector2 TouchPoint = new Vector2(5, 0);

        public override void TakeAction(Entity entity)
        {
            if (SkillType == "RotateAttackSkill")
            {
                //TODO 技能持续时间暂定3s，等待完善
                int SkillInternal = 3;

                // 暂停自动攻击
                StopAutoAttackFragment stopAutoAttackFragment = new StopAutoAttackFragment(SkillInternal);
                stopAutoAttackFragment.TakeEffect(entity);

                // 旋转攻击
                RotateAttackFragment rotateAttackFragment = new RotateAttackFragment();
                rotateAttackFragment.SkillInternal = SkillInternal;
                rotateAttackFragment.RotateSpeed = 5;   // 旋转速度，暂时写死
                rotateAttackFragment.TakeEffect(entity);

                // 移速增加
                SpeedChangeMoveFragment speedChangeMoveFragment = new SpeedChangeMoveFragment();
                speedChangeMoveFragment.InternalTime = SkillInternal;       
                speedChangeMoveFragment.TakeEffect(entity);

            }
            else if (SkillType == "BombSkill")
            {
                BombFragment bombFragement = new BombFragment();
                bombFragement.AttackTag = "Enemies";
                bombFragement.BulletSpeed = 0.5f;
                bombFragement.TargetPos = TouchPoint;
                bombFragement.Damage = 30;
                bombFragement.TakeEffect(entity);
            }
            else if (SkillType == "RushSkill")
            {
                int SkillInternal = 2;

                // 冲刺
                GravitationModifier gravitationModifier = new GravitationModifier(TouchPoint, SkillInternal);
                gravitationModifier.TakeEffect(entity);

                // 暂停移动输入
                StopPlayerMovementFragment stopPlayerMovementFragment = new StopPlayerMovementFragment(SkillInternal);
                stopPlayerMovementFragment.TakeEffect(entity);

                // 暂停自动攻击
                StopAutoAttackFragment stopAutoAttackFragment = new StopAutoAttackFragment(SkillInternal);
                stopAutoAttackFragment.TakeEffect(entity);

                // 将武器指向冲刺方向(武器附上击退效果)
                RushAttackFragment rushFragment = new RushAttackFragment();
                rushFragment.RushDir = TouchPoint - (Vector2) entity.transform.position;
                rushFragment.InternalTime = SkillInternal;
                rushFragment.TakeEffect(entity);
            }
            else
            {
                throw new ArgumentException("SkillType is out of consider.");
            }
        }
    }

}
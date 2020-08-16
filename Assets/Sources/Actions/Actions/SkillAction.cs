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
                //TODO 技能持续时间暂定1s，等待完善,调整后记得调weapon上的动画时间
                float SkillInternal = 1f;

                // 暂停自动攻击
                StopAutoAttackFragment stopAutoAttackFragment = new StopAutoAttackFragment(SkillInternal);
                stopAutoAttackFragment.TakeEffect(entity);

                // 范围攻击
                AreaAttackFragment areaAttackFragment = new AreaAttackFragment();
                areaAttackFragment.Damage = 35;
                areaAttackFragment.SkillInternal = SkillInternal;
                areaAttackFragment.HitPower = 0.4f;
                areaAttackFragment.TakeEffect(entity);

                // 移速增加
                SpeedChangeMoveFragment speedChangeMoveFragment = new SpeedChangeMoveFragment();
                speedChangeMoveFragment.InternalTime = SkillInternal;
                speedChangeMoveFragment.TakeEffect(entity);

                // 动画表示
                RotateAttackAnimatorFragment rotateAttackAnimatorFragment = new RotateAttackAnimatorFragment();
                rotateAttackAnimatorFragment.InteralTime = SkillInternal;
                rotateAttackAnimatorFragment.TakeEffect(entity);

                // 音效表示
                AudioCreateFragment audioPlayFragment = new AudioCreateFragment();
                audioPlayFragment.AudioName = SkillType;
                audioPlayFragment.PlayInternal = 0;
                audioPlayFragment.TakeEffect(entity);
                audioPlayFragment.PlayInternal = SkillInternal / 2;
                audioPlayFragment.TakeEffect(entity);

                //格挡子弹

                entity.GetComponent<CircleCollider2D>().enabled = true;
                entity.transform.GetChild(2).gameObject.SetActive(true);
                InvincibleFragment invincibleFragment = new InvincibleFragment();
                invincibleFragment.InternalTime = 1f;   // 无敌时间
                entity.StartCoroutine(CloseCircleCollider(SkillInternal,entity));
                


            }
            else if (SkillType == "BombSkill")
            {
                // 动画展示
                AnimatorParameterFragment animatorFragment = new AnimatorParameterFragment();
                animatorFragment.Animator = entity.transform.GetChild(0).GetComponent<Animator>();
                animatorFragment.Name = "Throw";
                animatorFragment.TakeEffect();

                // 扔一个水弹
                BombFragment bombFragement = new BombFragment();
                bombFragement.AttackTag = "Enemies";
                bombFragement.BulletSpeed = 1f;
                bombFragement.TargetPos = TouchPoint;
                bombFragement.Damage = 30;
                bombFragement.TakeEffect(entity);

                // 音效表示
                AudioCreateFragment audioPlayFragment = new AudioCreateFragment();
                audioPlayFragment.AudioName = SkillType;
                audioPlayFragment.PlayInternal = 0f;
                audioPlayFragment.TakeEffect(entity);
            }
            else if (SkillType == "RushSkill")
            {
                int SkillInternal = 1;

                // 冲刺
                MoveTowardFragment moveTowardFragment = new MoveTowardFragment();
                moveTowardFragment.InternalTime = SkillInternal;
                moveTowardFragment.Direction = TouchPoint;
                moveTowardFragment.Speed = 0.4f;
                moveTowardFragment.TakeEffect(entity);

                // 暂停移动输入
                StopPlayerMovementFragment stopPlayerMovementFragment = new StopPlayerMovementFragment(SkillInternal);
                stopPlayerMovementFragment.TakeEffect(entity);

                // 暂停自动攻击
                StopAutoAttackFragment stopAutoAttackFragment = new StopAutoAttackFragment(SkillInternal);
                stopAutoAttackFragment.TakeEffect(entity);

                // 将武器指向冲刺方向(武器附上击退效果)
                RushAttackFragment rushFragment = new RushAttackFragment();
                rushFragment.RushDir = TouchPoint - (Vector2)entity.transform.position;
                rushFragment.InternalTime = SkillInternal;
                rushFragment.TakeEffect(entity);

                // 武器动画
                RushAttackAnimatorFragment weaponAnimatorFragment = new RushAttackAnimatorFragment();
                weaponAnimatorFragment.InteralTime = SkillInternal;
                weaponAnimatorFragment.TakeEffect(entity);

                // 人物动画
                AnimatorParameterFragment animatorFragment = new AnimatorParameterFragment();
                animatorFragment.Animator = entity.transform.GetChild(0).GetComponent<Animator>();
                animatorFragment.Name = "Rush";
                animatorFragment.TakeEffect();

                // 音效表示
                AudioCreateFragment audioPlayFragment = new AudioCreateFragment();
                audioPlayFragment.AudioName = SkillType;
                audioPlayFragment.PlayInternal = 0f;
                audioPlayFragment.TakeEffect(entity);
            }
            else
            {
                throw new ArgumentException("SkillType is out of consider.");
            }
        }
        IEnumerator CloseCircleCollider(float time, Entity entity)
        {
            yield return new WaitForSeconds(time);
            entity.transform.GetChild(2).gameObject.SetActive(false);
            //entity.GetComponent<CircleCollider2D>().enabled = false;;
        }
    }

}
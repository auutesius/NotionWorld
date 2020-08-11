﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using NotionWorld.Actions;
using System.Linq;

public class SkillControllerForPlayer : MonoBehaviour
{
    public float ValidTime;
    public GameObject mask;
    private bool isAvailabe;
    private bool isWorking;
    public Entity entity;
    private SkillAction skill;

    public void SetSkillAvailable(bool s)
    {
        if (!isWorking)
        {
            isAvailabe = s;
            transform.GetChild(0).gameObject.SetActive(isAvailabe);
        }
    }

    public void StartSkillTime()
    {
        StartCoroutine(BulletTimeCD());
    }

    public bool GetIsWorking()
    {
        return isWorking;
    }

    IEnumerator BulletTimeCD()
    {
        SetSkillAvailable(false);
        isWorking = true;
        Time.timeScale = 0.5f;
        transform.GetChild(1).gameObject.SetActive(isWorking);
        mask.SetActive(true);

        yield return new WaitForSecondsRealtime(ValidTime);

        mask.SetActive(false);
        isWorking = false;
        transform.GetChild(1).gameObject.SetActive(isWorking);
        Time.timeScale = 1f;
    }

    public void SkillTrigger(int num)
    {
        if (num < 3 && num >= 0)
        {
            skill = new SkillAction();
            skill.SkillType = entity.GetCapability<Skill>().SkillTypes[num];
            skill.TakeAction(entity);

            mask.SetActive(false);
            isWorking = false;
            transform.GetChild(1).gameObject.SetActive(isWorking);
            Time.timeScale = 1f;
        }
        else
        {
            Debug.LogError("技能编号越界");
        }
    }

    public void InvincibleButton()
    {
        InvincibleFragment invincibleFragment = new InvincibleFragment();
        invincibleFragment.InternalTime = 1f;   // 无敌时间
        invincibleFragment.TakeEffect(entity);
    }

}
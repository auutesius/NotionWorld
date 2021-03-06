﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Entities;
using NotionWorld.Capabilities;
using NotionWorld.Actions;
using System.Linq;
using HedgehogTeam.EasyTouch;

public class SkillControllerForPlayer : MonoBehaviour
{
    public float ValidTime;
    public GameObject mask;
    private bool isAvailabe;
    private bool isWorking;
    public Entity entity;
    private SkillAction skill;
    public RippleEffect ripple;
    public GameObject BombSkill;
    public GameObject RushSkill;
    [SerializeField]public float invincibleTime;
    Vector2 pos2;
    Vector2 pos3;

    private void Awake() {
        invincibleTime = 1f;
        pos2 = new Vector2(0,140);
        pos3 = new Vector2(70,110);
    }
    public void ResetUIPosition(){
        BombSkill.GetComponent<RectTransform>().anchoredPosition = pos2;
        RushSkill.GetComponent<RectTransform>().anchoredPosition = pos3;
    }
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
        ripple.Emit(new Vector2(0f,0f));
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
        Time.timeScale = 0.1f;
        transform.GetChild(1).gameObject.SetActive(isWorking);
        mask.SetActive(true);


        yield return new WaitForSecondsRealtime(ValidTime);

        entity.GetCapability<Energy>().Value = 0;
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
            if(num != 0)skill.TouchPoint =  Camera.main.ScreenToWorldPoint(EasyTouch.current.position);
            skill.TakeAction(entity);
            
            StopAllCoroutines();
            mask.SetActive(false);
            isWorking = false;
            transform.GetChild(1).gameObject.SetActive(isWorking);
            entity.GetCapability<Energy>().Value = 0;
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
        invincibleFragment.InternalTime = invincibleTime;   // 无敌时间
        invincibleFragment.TakeEffect(entity);
    }
}

using System.Collections;
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
    private Vector2 pos2;
    private Vector2 pos3;
    private void Awake() {
        pos2 = GameObject.Find("Button_Skill2").transform.position;
        pos3 = GameObject.Find("Button_Skill3").transform.position;
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
        Time.timeScale = 0.5f;
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
            skill.TouchPoint =  Camera.main.ScreenToWorldPoint(EasyTouch.current.position);
            Debug.Log("Skill Position" + skill.TouchPoint);
            Debug.Log("Touch Point" + EasyTouch.current.position);
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
        invincibleFragment.InternalTime = 100f;   // 无敌时间
        invincibleFragment.TakeEffect(entity);
    }
    public void RecoverPos(){
        transform.GetChild(1).GetChild(1).position = pos2;
        transform.GetChild(1).GetChild(2).position = pos3;
    }

    // public void SetSkillTouchPoint(){
    //     if(skill != null){
    //         skill.TouchPoint = EasyTouch.current.position;
    //     }
    // }
}

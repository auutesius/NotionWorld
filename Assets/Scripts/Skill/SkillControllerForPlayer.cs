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
    private RectTransform pos2;
    private RectTransform pos3;
    public void OnDragStart() {
        pos2 = GameObject.Find("Button_Skill2").GetComponent<RectTransform>();
        pos3 = GameObject.Find("Button_Skill3").GetComponent<RectTransform>();
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
            GameObject.Find("Button_Skill2").GetComponent<RectTransform>().pivot = pos2.pivot;
            GameObject.Find("Button_Skill2").GetComponent<RectTransform>().position = pos2.position;
            GameObject.Find("Button_Skill3").GetComponent<RectTransform>().pivot = pos3.pivot;
            GameObject.Find("Button_Skill3").GetComponent<RectTransform>().position = pos3.position;
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

    // public void SetSkillTouchPoint(){
    //     if(skill != null){
    //         skill.TouchPoint = EasyTouch.current.position;
    //     }
    // }
}

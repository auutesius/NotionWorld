using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTimeController : MonoBehaviour
{
    public float ValidTime;
    public GameObject mask;
    private bool isAvailabe;
    private bool isWorking;
    public void SetSkillAvailable(bool s)
    {
        if (!isWorking)
        {
            isAvailabe = s;
            transform.GetChild(0).gameObject.SetActive(isAvailabe);
        }
    }

    public void SkillTrigger()
    {
        StartCoroutine(SkillTriggerCD());
    }

    public bool GetIsWorking()
    {
        return isWorking;
    }

    IEnumerator SkillTriggerCD()
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
}

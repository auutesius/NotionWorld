using NotionWorld.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboController : MonoBehaviour, ISubscriber<ComboEvenArg>
{
    [Tooltip("连击清零的时间间隔")]
    public float ClearInternal;
    private float curTime;
    [HideInInspector] public int ComboValue;
    public Animator imageAnimator;
    private void Awake()
    {
        EventCenter.Subscribe(this);
    }
    public void OnEventOccurred(ComboEvenArg eventArgs)
    {
        ComboValue += eventArgs.Value;
        imageAnimator.Play("ComboSlot");
        curTime = 0;
    }

    private void Update()
    {
        CheckInternalTime();
    }

    private void CheckInternalTime()
    {
        curTime += Time.deltaTime;

        if (curTime > ClearInternal)
        {
            curTime = 0;
            ComboValue = 0;
        }

    }
}

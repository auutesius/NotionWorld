using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Events;

public class SecondTimer : MonoBehaviour
{
    private const float time = 1F;

    private SecondUpdateEventArgs eventArgs = new SecondUpdateEventArgs(time);

    void Start()
    {
        StartCoroutine(ClockCorotinue());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator ClockCorotinue()
    {
        WaitForSeconds second = new WaitForSeconds(time);
        while (true)
        {
            yield return second;
            EventCenter.DispatchEvent(eventArgs);
        }
    }
}

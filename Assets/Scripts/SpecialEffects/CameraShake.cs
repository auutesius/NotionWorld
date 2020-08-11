using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    // 抖动目标的transform(若未添加引用，怎默认为当前物体的transform)
    public Transform camTransform;

    //持续抖动的时长
    public float shakeTime = 0f;

    public float Amplitude = 0.5f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeTime > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * Amplitude;

            shakeTime -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeTime = 0f;
            camTransform.localPosition = originalPos;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    
    public float fadeTime;

    private float alpha = 0;

    private float targetAlpha = 1;

    public Image fadeImage;

    public void FadeIn()
    {
        alpha = 0.75f;
        targetAlpha = 0;
        StartCoroutine(FadeCorotinue());
    }

    public void FadeOut()
    {
        alpha = 0;
        targetAlpha = 0.75f;
        StartCoroutine(FadeCorotinue());
    }

    public IEnumerator FadeCorotinue()
    {       
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        float delta = (targetAlpha - alpha) * Time.fixedDeltaTime / fadeTime;
        float timer = fadeTime;
        Color color = fadeImage.color;
        color.a = alpha;
        fadeImage.color = color;
        while (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
            color.a += delta;
            fadeImage.color = color;
            Debug.Log(color.a);
            yield return wait;
        }
    }
}

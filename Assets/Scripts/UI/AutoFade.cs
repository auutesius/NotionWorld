using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Worlds;

public class AutoFade : MonoBehaviour
{

    public float fadeTime = 2F;

    private void Awake()
    {
        StartCoroutine(FadeCorotinue());
    }

    private IEnumerator FadeCorotinue()
    {
        var fadeCanvas = ObjectPool.GetObject("FadeCanvas", "UI");
        var fade = fadeCanvas.GetComponent<Fade>();
        fade.FadeIn();
        yield return new WaitForSeconds(fadeTime);

        ObjectPool.RecycleObject(fadeCanvas);
        Destroy(gameObject);
    }
}

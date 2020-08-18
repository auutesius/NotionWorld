using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NotionWorld.Worlds;

public sealed class Exit : MonoBehaviour
{
    public GameObject player;

    public string nextScene;

    public float fadeTime = 2F;

    private bool entered = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!entered && other.gameObject == player)
        {
            entered = true;
            StartCoroutine(FadeCorotinue());
        }
    }

    private IEnumerator FadeCorotinue()
    {
        var fadeCanvas = ObjectPool.GetObject("FadeCanvas", "UI");
        var fade = fadeCanvas.GetComponent<Fade>();
        fade.FadeOut();
        yield return new WaitForSeconds(fadeTime);

        SceneManager.LoadScene(nextScene);
    }
}

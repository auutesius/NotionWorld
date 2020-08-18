using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class Exit : MonoBehaviour
{
    public GameObject player;

    public string nextScene;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}

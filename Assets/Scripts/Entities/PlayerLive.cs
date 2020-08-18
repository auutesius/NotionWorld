﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NotionWorld.Capabilities;
using NotionWorld.Entities;
using NotionWorld.Worlds;
using UnityEngine.SceneManagement;

public class PlayerLive : MonoBehaviour
{
    private Health health;

    public float endingTime = 2F;

    public float endingScale = 0.5F;

    public float sceneTime = 3F;

    public Animator animator;

    public UIController uIController;

    private int lastValue;

    public AudioSource source;

    public AudioClip clip;

    void Start()
    {
        health = GetComponent<Entity>().GetCapability<Health>();
        lastValue = health.Value;
    }

    void Update()
    {
        if (lastValue != health.Value)
        {
            lastValue = health.Value;
            source.PlayOneShot(clip);
        }

        if (health.Value < 0)
        {
            health.Value = health.MaxValue;

            StartCoroutine(EndCorotinue());
        }
    }

    private IEnumerator EndCorotinue()
    {
        uIController.enabled = false;
        animator.SetTrigger("Die");

        Time.timeScale = endingScale;
        yield return new WaitForSeconds(endingTime);
        Time.timeScale = 1;

        var fadeCanvas = ObjectPool.GetObject("FadeCanvas", "UI");
        var fade = fadeCanvas.GetComponent<Fade>();
        fade.FadeOut();

        yield return new WaitForSeconds(sceneTime);
        SceneManager.LoadScene("died");
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreStar : MonoBehaviour
{
    public Image star;

    public ParticlePlayer starFX;

    public float delay = 0.5f;

    public AudioClip starSound;

    public bool activated = false;


    void Start()
    {
        SetActive(false);
    }

    void SetActive(bool state)
    {
        if (star != null)
        {
            star.gameObject.SetActive(state);
        }
    }

    public void Activate()
    {
        if (activated)
        {
            return;
        }

        StartCoroutine(ActivateRoutine());
    }

    IEnumerator ActivateRoutine()
    {
        activated = true;

        if (starFX != null)
        {
            starFX.Play();
        }

        if (SoundManager.Instance != null && starSound != null)
        {
            SoundManager.Instance.PlayClipAtPoint(starSound, Vector3.zero, SoundManager.Instance.fxVolume);
        }

        yield return new WaitForSeconds(delay);

        SetActive(true);
    }

}

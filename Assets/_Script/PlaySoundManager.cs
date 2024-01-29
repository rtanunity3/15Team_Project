using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySoundManager : MonoBehaviour
{
    public Slider soundSlider;

    public AudioSource effectSound;
    public AudioClip buttonSound;
    public AudioClip fruitSound;
    public AudioClip bombSound;
    public AudioClip clearSound;
    public AudioClip highClearSound;
    public AudioClip countSound;
    public AudioClip startSound;
    public AudioClip makeSound;

    private void Start()
    {
        soundSlider.onValueChanged.AddListener((float val) =>
        {
            GameManager.Instance.PlaySound = val;
            effectSound.volume = val;
        });
        soundSlider.value = GameManager.Instance.PlaySound;
    }

    public void buttonSoundPlay()
    {
        effectSound.clip = buttonSound;
        effectSound.Play();
    }

    public void fruitSoundPlay()
    {
        effectSound.clip = fruitSound;
        effectSound.Play();
    }

    public void bombSoundPlay()
    {
        effectSound.clip = bombSound;
        effectSound.Play();
    }

    public void clearSoundPlay()
    {
        try
        {
            effectSound.clip = countSound;
            effectSound.Play();
        }
        catch
        {
            Debug.Log("error");
        }
    }

    public void highClearSoundPlay()
    {
        effectSound.clip = highClearSound;
        effectSound.Play();
    }

    public void countSoundPlay()
    {
        effectSound.clip = clearSound;
        effectSound.Play();
    }

    public void startSoundPlay()
    {
        effectSound.clip = startSound;
        effectSound.Play();
    }

    public void makeSoundPlay()
    {
        effectSound.clip = makeSound;
        effectSound.Play();
    }
}

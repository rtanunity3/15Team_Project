using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySoundManager : MonoBehaviour
{
    public AudioClip eatClip;
    public AudioSource eatAudioSource;
    public AudioClip boomClip;
    public AudioSource boomAudioSource;
    public Slider soundSlider;
    // Start is called before the first frame update
    void Start()
    {
        eatAudioSource.volume = GameManager.Instance.PlaySound;
        soundSlider.value = GameManager.Instance.PlaySound;
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.PlaySound = soundSlider.value;
        eatAudioSource.volume = soundSlider.value;
        boomAudioSource.volume = soundSlider.value;
    }

    public void EatSoundPlay()
    {
        eatAudioSource.PlayOneShot(eatClip);
    }

    public void BoomSoundPlay()
    {
        boomAudioSource.PlayOneShot(boomClip);
    }
}

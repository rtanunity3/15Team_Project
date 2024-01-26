using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip bgmusic;
    public Slider soundSlider;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.volume = GameManager.Instance.BGMSound;
        soundSlider.value = GameManager.Instance.BGMSound;
        audioSource.clip = bgmusic;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.Instance.BGMSound = soundSlider.value;
        audioSource.volume = soundSlider.value;
    }
}

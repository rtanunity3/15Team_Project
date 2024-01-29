using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSoundManager : MonoBehaviour
{
    public Slider soundSlider;

    public AudioClip bgmusic;
    public AudioSource audioSource; // 배경음악

    // Start is called before the first frame update
    void Start()
    {
        soundSlider.onValueChanged.AddListener((float val) =>
        {
            GameManager.Instance.BGMSound = val;
            audioSource.volume = val;
        });
        soundSlider.value = GameManager.Instance.BGMSound;
        audioSource.clip = bgmusic;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}

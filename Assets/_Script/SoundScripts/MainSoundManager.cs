using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSoundManager : MonoBehaviour
{
    public AudioSource MainAudioSource;
    public AudioClip MainBgmusic;

    // Start is called before the first frame update
    void Start()
    {
        MainAudioSource.clip = MainBgmusic;
        MainAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

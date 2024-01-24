using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSoundManager : MonoBehaviour
{

    public AudioSource TitleAudioSource;
    public AudioClip TitleBgmusic;
    // Start is called before the first frame update
    void Start()
    {
        TitleAudioSource.clip = TitleBgmusic;
        TitleAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

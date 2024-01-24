using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundManager : MonoBehaviour
{
    public AudioClip EatClip;
    public AudioSource EatAudioSource;
    public AudioClip BoomClip;
    public AudioSource BoomAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EatSoundPlay()
    {
        EatAudioSource.PlayOneShot(EatClip);
    }

    public void BoomSoundPlay()
    {
        BoomAudioSource.PlayOneShot(BoomClip);
    }
}

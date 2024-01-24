using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalySoundManager : MonoBehaviour
{
    public AudioClip EatFlip;
    public AudioSource EatAudioSource;
    public AudioClip BoomEatFlip;
    public AudioSource BoomEatAudioSource;
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
        EatAudioSource.PlayOneShot(EatFlip);
    }

    public void BoomEatSoundPlay()
    {
        BoomEatAudioSource.PlayOneShot(BoomEatFlip);
    }
}

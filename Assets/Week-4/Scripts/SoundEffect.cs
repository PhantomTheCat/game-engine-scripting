using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    //Properties
    AudioSource soundAudio;
    private bool didPlay;


    //Methods
    private void Awake()
    {
        soundAudio = GetComponent<AudioSource>();
    }

    public void Initialize(AudioClip clip) //Teacher recommended Init as an alternative name
    {
        soundAudio.clip = clip;
        Play();
    }

    public void Play()
    {
        soundAudio.Play();
    }

    private void Update()
    {
        //Checking if didPlay is false
        //Then checking if the audio is playing to destroy the gameObject
        if(didPlay == false) return;
        if (soundAudio.isPlaying == false) Destroy(gameObject);
    }

}

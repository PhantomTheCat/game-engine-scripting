using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    //Properties
    AudioSource audio;
    private bool didPlay;


    //Methods
    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Initialize(AudioClip clip) //Teacher recommended Init as an alternative name
    {
        audio.clip = clip;
        Play();
    }

    public void Play()
    {
        audio.Play();
    }

    private void Update()
    {
        //Checking if didPlay is false
        //Then checking if the audio is playing to destroy the gameObject
        if(didPlay == false) return;
        if (audio.isPlaying == false) Destroy(gameObject);
    }

}

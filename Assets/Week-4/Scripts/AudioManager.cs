using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Best to have a AudioManager empty object and Play sounds from here
    //Don't have to repeat ourselves for bigger projects

    //Properties
    private static AudioManager instance; //This static member will be playing the sound
    [SerializeField] GameObject soundEffectPrefab;
    [SerializeField] private AudioClip attack;
    [SerializeField] private AudioClip damage;

    
    public enum SoundType //Enums are convinient ways of categorizing types
    {
        ATTACK = 0,
        DAMAGE = 1,
    }



    //Methods
    private void Awake()
    {
        //Prevents this from being destroyed
        DontDestroyOnLoad(this);

        //Makes the instance = this, connecting the instance to this object
        instance = this;
    }

    public static void PlaySound(SoundType s) //Global method
    {
        //Connecting enum to the audio clip, and playing it
        instance.PrivatePlaySound(s);

    }

    private void PrivatePlaySound(SoundType s)
    {
        //Original usage of this
        //Connects the enum to the audio clip
        /*switch(s)
        {
            case SoundType.ATTACK:
                audio.PlayOneShot(attack);
                break;
            case SoundType.DAMAGE: 
                audio.PlayOneShot(damage);
                break;
        }

        //Second version of this
        switch (s)
        {
            case SoundType.ATTACK:
                audio.clip = attack;
                break;
            case SoundType.DAMAGE:
                audio.clip = damage;
                break;
        }*/
        //audio.Play();



        //Thrid version
        AudioClip clip = null;


        switch (s)
        {
            case SoundType.ATTACK:
                clip = attack;
                break;
            case SoundType.DAMAGE:
                clip = damage;
                break;
        }

        GameObject soundEffectObject = Instantiate(soundEffectPrefab);
        SoundEffect soundEffect = soundEffectObject.GetComponent<SoundEffect>();
        soundEffect.Initialize(clip);
        soundEffect.Play();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            PrivatePlaySound(SoundType.ATTACK);
        }
    }
}

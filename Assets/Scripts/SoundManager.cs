using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [SerializeField] AudioSource aud;

    void Awake () 
    {
        instance = this;
    }

    public void PlaySound (AudioClip sound, float volume, float pitch)
    {
        aud.pitch = pitch;
        aud.volume = volume;
        aud.PlayOneShot(sound);
    }
}

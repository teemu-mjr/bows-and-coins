using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    // private fields
    [HideInInspector] public AudioSource audioSource;
    public void PlaySound(AudioClip clip, float volume = 1)
    {
        audioSource.volume = volume;
        audioSource.clip = clip;
        audioSource.Play();
    }
}

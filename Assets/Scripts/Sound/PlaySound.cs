using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private Transform audioTranform;
    private AudioSource m_audioSource;
    private GameObject audioObject;

    public PlaySound Init()
    {
        audioTranform = GameObject.Find("Audio").transform;
        audioObject = new GameObject(gameObject.name + "Sound");
        audioObject.AddComponent<PlaySoundObject>();
        audioObject.transform.SetParent(audioTranform);
        m_audioSource = audioObject.gameObject.AddComponent<AudioSource>();

        return this;
    }

    public void Play(AudioClip audioClip, bool randomizePitch = false, float volume = 1)
    {
        if (randomizePitch)
        {
            m_audioSource.pitch = Random.Range(1f, 1.05f);
        }
        m_audioSource.volume = volume * Options.optionData.effectVolume;
        m_audioSource.clip = audioClip;
        m_audioSource.Play();
    }

    public void DestroyAudio()
    {
        try
        {
            audioObject.GetComponent<PlaySoundObject>().DestroySound();
        }
        catch
        {
            // gameobject was destroyed
            return;
        }
    }
}

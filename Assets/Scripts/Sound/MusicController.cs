using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public List<AudioClip> playlist;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySong(int index)
    {
        audioSource.clip = playlist[index];
        StartCoroutine(FadeIn(0.01f));

        audioSource.Play();
    }

    private IEnumerator FadeIn(float speed)
    {
        audioSource.volume = 0;

        while(audioSource.volume < Options.optionData.musicVolume)
        {
            audioSource.volume += speed;
            yield return new WaitForSeconds(0.1f);
        }
    }
}

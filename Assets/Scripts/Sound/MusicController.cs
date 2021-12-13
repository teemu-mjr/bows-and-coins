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
        StartCoroutine(FadeIn(0.025f));
        audioSource.Play();
    }

    public void PlaySongWithFadeOut(int index)
    {
        StartCoroutine(FadeOut(0.025f, index));
    }

    private IEnumerator FadeOut(float speed, int index)
    {
        Debug.Log("FadingOut");
        while (audioSource.volume > 0)
        {
            audioSource.volume -= speed;
            yield return new WaitForSeconds(0.1f);
        }

        PlaySong(index);
    }

    private IEnumerator FadeIn(float speed)
    {
        Debug.Log("FadingIn");
        audioSource.volume = 0;

        while (audioSource.volume < Options.optionData.musicVolume)
        {
            audioSource.volume += speed;
            yield return new WaitForSeconds(0.1f);
        }
    }
}

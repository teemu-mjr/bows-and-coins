using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    // public fields
    public TextMeshProUGUI easyModeInfo;
    public Slider musicVolume;
    public Slider effectVolume;
    public AudioSource testAudio;
    public AudioClip testSound;

    // private fields
    private static bool doubleCoins;
    private SerializeData serializeData;
    private int cooldown;

    // properties
    public static bool DoubleCoins { get { return doubleCoins; } }

    void Start()
    {
        serializeData = new SerializeData();
        LoadOptions();
        RenderOptions();
    }

    public void ChangeDoubleCoins()
    {
        doubleCoins = !doubleCoins;
        RenderOptions();

        Options.optionData.easyMode = doubleCoins;
        SaveOptions();
    }

    public void ChangeMusicVolume(float volume)
    {
        Options.optionData.musicVolume = volume;
        SaveOptions();
    }
    public void ChangeEffectVolume(float volume)
    {
        Options.optionData.effectVolume = volume;
        SaveOptions();

        if (cooldown >= 5)
        {
            testAudio.PlayOneShot(testSound, volume);
            cooldown = 0;
        }
        else
        {
            cooldown++;
        }
    }

    private void RenderOptions()
    {
        musicVolume.value = Options.optionData.musicVolume;
        effectVolume.value = Options.optionData.effectVolume;

        if (doubleCoins)
        {
            easyModeInfo.text = "ON";
        }
        else
        {
            easyModeInfo.text = "OFF";
        }
    }

    private void SaveOptions()
    {
        serializeData.SaveData<Options.OptionData>(Options.optionData, "options");
    }

    private void LoadOptions()
    {
        Options.optionData = serializeData.LoadData<Options.OptionData>("options");
        doubleCoins = Options.optionData.easyMode;
    }
}

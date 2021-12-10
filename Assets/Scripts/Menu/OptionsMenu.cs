using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    // public fields
    public TextMeshProUGUI easyModeInfo;

    // private fields
    private static bool doubleCoins;
    private SerializeData serializeData;
    private OptionData options;

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

        options.easyMode = doubleCoins;
        SaveOptions();
    }

    private void RenderOptions()
    {

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
        serializeData.SaveData<OptionData>(options, "options");
    }

    private void LoadOptions()
    {
        options = serializeData.LoadData<OptionData>("options");

        doubleCoins = options.easyMode;
    }
}

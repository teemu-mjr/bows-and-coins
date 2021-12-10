using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Options
{
    public class OptionData
    {
        public bool easyMode = false;
        public float musicVolume = 0.5f;
        public float effectVolume = 1;

    }

    public static OptionData optionData;
    SerializeData serializeData = new SerializeData();

    public Options()
    {
        optionData = serializeData.LoadData<OptionData>("options");
    }
}

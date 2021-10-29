using System;
using UnityEngine;
using System.IO;
using System.Xml.Serialization;

public class SerializeData
{
    private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";


    public SerializeData()
    {
        if (!Directory.Exists(SAVE_FOLDER))
        {
            Directory.CreateDirectory(SAVE_FOLDER);
        }
    }

    public void SaveData <T> (T data)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

        using(TextWriter writer = new StreamWriter(@$"{SAVE_FOLDER}\playerStats.xml"))
        {
            xmlSerializer.Serialize(writer, data);
        }
    }


    public T LoadData<T>()
    {
        XmlSerializer deserializer = new XmlSerializer(typeof(T));
        if (File.Exists(@$"{SAVE_FOLDER}\playerStats.xml"))
        {
            TextReader reader = new StreamReader(@$"{SAVE_FOLDER}\playerStats.xml");
            object obj = deserializer.Deserialize(reader);
            reader.Close();
            return (T)obj;
        }
        else
        {
            object obj = new PlayerStats();
            return (T)obj;
        }
    }
}

using System;
using System.IO;
using System.Xml.Serialization;

public class SerializeData
{
    private static string userFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    private static string fileLocation = @$"{userFolderPath}\Bows and Coins\";


    public SerializeData()
    {
        if (!Directory.Exists(fileLocation))
        {
            Directory.CreateDirectory(fileLocation);
        }
    }

    public void SaveData <T> (T data)
    {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));

        using(TextWriter writer = new StreamWriter(@$"{fileLocation}\playerStats.dat"))
        {
            xmlSerializer.Serialize(writer, data);
        }
    }


    public T LoadData<T>()
    {
        XmlSerializer deserializer = new XmlSerializer(typeof(T));
        if (File.Exists(@$"{fileLocation}\playerStats.dat"))
        {
            TextReader reader = new StreamReader(@$"{fileLocation}\playerStats.dat");
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

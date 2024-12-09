using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class FileReadWrite
{
    const string ResourceFileName = "resources.data";

    public static void WriteResourceData(ResourceCounter counter)
    {
        BinaryFormatter formatter = new();

        string path = Application.persistentDataPath + "/" + ResourceFileName;
        FileStream stream = new(path, FileMode.Create);

        SerializedResourceData data = new(counter);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static SerializedResourceData ReadResourceData()
    {
        string path = Application.persistentDataPath + "/" + ResourceFileName;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);

            SerializedResourceData data = formatter.Deserialize(stream) as SerializedResourceData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file for resource counts could not be loaded.");
            return null;
        }
    }
}

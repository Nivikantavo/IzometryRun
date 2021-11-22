using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    private static string _path = Application.persistentDataPath + "/playerData.txt";

    public static void SaveProgress(int levelIndex, int levelScore)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream;
        PlayerData data;

        if (File.Exists(_path))
        {
            stream = new FileStream(_path, FileMode.Open);
            data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            data.AddLevelScore(levelIndex, levelScore);
        }
        else
        {
            data = new PlayerData(levelIndex, levelScore);
        }

        stream = new FileStream(_path, FileMode.Create);
        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static PlayerData LoadProgress()
    {
        if (File.Exists(_path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(_path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + _path);
            return CreateEmptyProgress();
        }
    }

    public static PlayerData CreateEmptyProgress()
    {
        PlayerData data = new PlayerData(1, 0);
        return data;
    }
}

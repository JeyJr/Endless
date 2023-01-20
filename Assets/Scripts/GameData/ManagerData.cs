using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;

public class ManagerData
{
    public static void Save(GameData data)
    {
        BinaryFormatter bf = new();
        FileStream fs = new(GetPath(), FileMode.Create);
        bf.Serialize(fs, data);
        fs.Close();
    }

    public static GameData Load()
    {
        if (!File.Exists(GetPath()))
        {
            GameData dataEmpty = new();
            Save(dataEmpty);
            return dataEmpty;
        }
            

        BinaryFormatter bf = new();
        FileStream fs = new(GetPath(), FileMode.Open);

        GameData data = (GameData)bf.Deserialize(fs);
        fs.Close();

        return data;
    }

    public static void DeleteData()
    {
        File.Delete(GetPath());
    }

    static string GetPath()
    {
        return Application.persistentDataPath + "/game.data";
    }

    public static bool CheckIfSavedFileExists()
    {
        return File.Exists(GetPath());
    }

    public static void CaminhoDoArquivo()
    {
        Debug.Log(Application.persistentDataPath + "/game.data");
    }
}

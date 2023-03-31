using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.VisualScripting;
using System.Collections.Generic;

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
        if (File.Exists(GetPath()))
        {
            // se o arquivo existe, redefina seus valores para 0 ou padrão
            ResetSavedFile();
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void ResetSavedFile()
    {
        GameData d = new();

        d.gold = 3000;
        d.atk = 1;
        d.def = 1;
        d.vit = 1;
        d.agi = 1;
        d.cri = 1;

        d.purchasedWeaponsIds = new List<int>();
        d.purchasedWeaponsIds.Add(1000); //Peda�o de madeira
        d.equipedWeaponId = d.purchasedWeaponsIds[0];

        d.purchasedArmorIds = new List<int>();
        d.purchasedArmorIds.Add(1000); //StandardSet
        d.equipedArmorId = d.purchasedArmorIds[0];

        d.purchasedHelmetIds = new List<int>();
        d.purchasedHelmetIds.Add(1000); //StandardSet
        d.equipedHelmetId = d.purchasedHelmetIds[0];

        d.purchasedArmIds = new List<int>();
        d.purchasedArmIds.Add(1000); //StandardSet
        d.equipedArmId = d.purchasedArmIds[0];

        d.skillLevelBonusDmg = 0;
        d.skillLevelBonusDef = 0;
        d.skillLevelBonusLife = 0;
        d.skillLevelBonusAtkSpeed = 0;
        d.skillLevelBonusRange = 0;
        d.skillLevelBonusGold = 0;

        d.levelUnlock = 0;
        d.maxLevel = 12;

        Save(d);
    }

    public static void CaminhoDoArquivo()
    {
        Debug.Log(Application.persistentDataPath + "/game.data");
    }
}

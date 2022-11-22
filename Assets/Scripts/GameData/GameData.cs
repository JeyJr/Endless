using Newtonsoft.Json.Linq;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameData : MonoBehaviour
{

    #region Gold
    public static void AddGold(float amount) => PlayerPrefs.SetFloat("gold", LoadGold() + amount);
    public static void SubGold(float amount) => PlayerPrefs.SetFloat("gold", LoadGold() - amount);

    public static float LoadGold()
    {
        if (!PlayerPrefs.HasKey("gold"))
            PlayerPrefs.SetFloat("gold", 100000);

        return PlayerPrefs.GetFloat("gold");
    }
    #endregion

    public static void FirstTime()
    {
        if (!PlayerPrefs.HasKey("firstTime"))
        {
            PlayerPrefs.SetInt("firstTime", 1);


            PlayerPrefs.SetFloat("atk", 1);
            PlayerPrefs.SetFloat("def", 1);
            PlayerPrefs.SetFloat("vit", 1);
            PlayerPrefs.SetFloat("agi", 1);
            PlayerPrefs.SetFloat("cri", 1);

            SetDMG();
            SetDefense();
        }
    }

    public static void SetAttribute(string key, bool add)
    {
        float value = PlayerPrefs.GetFloat(key);

        if (add)
        {
            if (LoadGold() > GetGoldCost(key) && value < 100)
            {
                SubGold(GetGoldCost(key));
                PlayerPrefs.SetFloat(key, value + 1);
            }
        }
        else
        {
            if (value > 1)
            {
                PlayerPrefs.SetFloat(key, value - 1);
                AddGold(GetGoldCost(key));
            }
        }

        if (key == "atk") SetDMG();
        if (key == "def") SetDefense();
    }

    public static float GetAttribute(string key)
    {
        return PlayerPrefs.GetFloat(key);
    }

    public static float GetGoldCost(string key)
    {
        return PlayerPrefs.GetFloat(key) * 200;
    }

    #region Damage
    //1 atk == 5 damage
    public static void SetDMG()
    {
        PlayerPrefs.SetFloat("damage", GetAttribute("atk") * 5);
    }
    public static float GetDMG()
    {
        return PlayerPrefs.GetFloat("damage");
    }
    #endregion

    #region Defense
    //1 def == .5f defense
    public static void SetDefense()
    {
        PlayerPrefs.SetFloat("defense", GetAttribute("def") / 2);
    }
    public static float GetDefense()
    {
        return PlayerPrefs.GetFloat("defense");
    }
    #endregion

}

using Newtonsoft.Json.Linq;
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

    public static void AddAttributes(string key, float mult, float amount)
    {
        SubGold(LoadAttributes(key) * mult);
        PlayerPrefs.SetFloat(key, LoadAttributes(key) + amount);
    }

    public static void SubAttributes(string key, float mult, float amount)
    {
        float attributeValue = LoadAttributes(key) - amount;

        PlayerPrefs.SetFloat(key, attributeValue);
        AddGold(attributeValue * mult);
    }

    public static float LoadAttributes(string key)
    {
        return PlayerPrefs.GetFloat(key);
    } 
}

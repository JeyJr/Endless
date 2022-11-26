using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    public static bool GetFirstTimeWeapon()
    {
        if (!PlayerPrefs.HasKey("firstTimeWeapon"))
            return true;

        return false;
    }

    public static void SetFirstTime()
    {
        PlayerPrefs.SetInt("firstTimeWeapon", 1);
    }

    #region ID
    public static void SetWeaponIndex(int index)
    {
        PlayerPrefs.SetInt("weaponIndex", index);
    }

    public static int GetWeaponIndex()
    {
        if (!PlayerPrefs.HasKey("weaponIndex"))
            SetWeaponIndex(0);

        return PlayerPrefs.GetInt("weaponIndex");
    }
    #endregion

    #region DMG
    public static void SetWeaponDMG(float value)
    {
        PlayerPrefs.SetFloat("weaponDMG", value);
    }

    public static float GetWeaponDMG()
    {
        if (!PlayerPrefs.HasKey("weaponDMG"))
            SetWeaponDMG(0);

        return PlayerPrefs.GetFloat("weaponDMG");
    }

    #endregion

    #region AtkSpeed
    public static void SetWeaponAtkSpeed(float value)
    {
        PlayerPrefs.SetFloat("weaponAtkSpeed", value);
    }

    public static float GetWeaponAtkSpeed()
    {
        if (!PlayerPrefs.HasKey("weaponAtkSpeed"))
            SetWeaponAtkSpeed(3.1f);

        return PlayerPrefs.GetFloat("weaponAtkSpeed");
    }

    #endregion

    #region AtkRange

    public static void SetWeaponAtkRange(float range)
    {
        PlayerPrefs.SetFloat("weaponAtkRange", range);
    }

    public static float GetWeaponAtkRange()
    {
        if (!PlayerPrefs.HasKey("weaponAtkRange"))
            SetWeaponAtkRange(2f);

        return PlayerPrefs.GetFloat("weaponAtkRange");
    }

    #endregion

    #region BuyWeapon

    public static bool GetWeaponPurchased(int id)
    {
        if (PlayerPrefs.GetInt(id.ToString()) == 1)
            return true;
        else
            return false;
    }

    public static void SetWeaponPurchased(int id, int value)
    {
        PlayerPrefs.SetInt(id.ToString(), value);
    }

    #endregion
}

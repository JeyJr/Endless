using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour
{
    #region ID
    public static void SetWeaponID(int index)
    {
        PlayerPrefs.SetInt("weaponIndex", index);
    }

    public static int GetWeaponID()
    {
        if (!PlayerPrefs.HasKey("weaponIndex"))
            SetWeaponID(0);

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

    #region ATKSPEED
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
}

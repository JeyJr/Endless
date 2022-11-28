using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool firstTime = true;

    public float gold;

    public float atk;
    public float def;
    public float vit;
    public float agi;
    public float cri;

    public float Damage 
    {
        get => (atk * 5) + weaponDmg + bonusDmg;
    }
    public float Defense
    {
        get => def / 2 + weaponDefense +bonusDefense;
    }
    public float MaxLife
    {
        get => vit * 50 + weaponLife +bonusLife;
    }
    public float AtkSpeed
    {
        get =>  (weaponSpeedAtk - agi * .03f) - bonusAtkSpeed;
    }
    public float CriticalDMG
    {
        get => cri / 2 + weaponCritical;
    }
    public float RangeAtk
    {
        get => weaponRangeAtk + bonusRangeAtk;
    }

    //Weapons
    public List<int> purchasedWeaponsIds;
    public int equipedWeaponId;

    public float weaponDmg;
    public float weaponDefense;
    public float weaponLife;
    public float weaponSpeedAtk;
    public float weaponCritical;
    public float weaponRangeAtk;


    //SkillsPassivas
    public float skillLevelBonusDmg;
    public float skillLevelBonusDef;
    public float skillLevelBonusLife;
    public float skillLevelBonusAtkSpeed;
    public float skillLevelBonusRange;
    public float skillLevelBonusGold;

    public float bonusDmg;
    public float bonusDefense;
    public float bonusLife;
    public float bonusRangeAtk;
    public float bonusAtkSpeed;
    public float bonusGold;


}

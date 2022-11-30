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
        get {
            float totalAtk = atk * 5;
            return totalAtk + (totalAtk * bonusDmg / 100) + weaponDmg;
        } 
    }
    public float Defense
    {
        get {
            float totalDef = def / 2;
            return totalDef + (totalDef * weaponDefense / 100) + (totalDef * bonusDefense / 100);
        }
    }
    public float MaxLife
    {
        get {
            float totalLife = vit * 50;
            return totalLife + (totalLife * weaponLife / 100) + (totalLife * bonusLife / 100);
        }
    }
    public float AtkSpeed
    {
        get =>  (weaponSpeedAtk - agi * .03f) - bonusAtkSpeed;
    }
    public float CriticalDMG
    {
        get {
            float totalCritic = cri / 2;
            return totalCritic + (totalCritic * weaponCritical / 100) + (totalCritic * bonusCritical / 100);
        }
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
    public float skillLevelCritical;
    public float skillLevelBonusGold;

    public float bonusDmg;
    public float bonusDefense;
    public float bonusLife;
    public float bonusRangeAtk;
    public float bonusAtkSpeed;
    public float bonusCritical;
    public float bonusGold;

    //Level
    public float levelUnlock;
}

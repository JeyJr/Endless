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
            return totalAtk + (totalAtk * bonusDmg / 100) + weaponDmg + buffSkillPowerUp + armorDmg;
        } 
    }
    public float Defense
    {
        get {
            float totalDef = def / 2;
            return totalDef + (totalDef * weaponDefense / 100) + (totalDef * bonusDefense / 100) + (totalDef * armorDefense / 100);
        }
    }
    public float MaxLife
    {
        get {
            float totalLife = vit * 50;
            return totalLife + (totalLife * weaponLife / 100) + (totalLife * bonusLife / 100) + (totalLife * armorLife / 100);
        }
    }
    public float AtkSpeed
    {
        get {
            float speed = (weaponSpeedAtk - agi * .02f) - (weaponSpeedAtk * bonusAtkSpeed / 100);
            speed -= (speed * buffSkillAtkSpeed / 100);
            return speed < 0.15f ? 0.15f : speed;
        }
        // r = (weaponSpeedAtk - 2) - (weaponSpeedAtk * .25 ) 
        // r -= r * 0.5 

    }
    public float CriticalDMG
    {
        get {
            float totalCritic = cri / 2;
            return totalCritic + (totalCritic * weaponCritical / 100) + (totalCritic * bonusCritical / 100) + (totalCritic * armorCritical / 100);
        }
    }
    public float RangeAtk
    {
        get => weaponRangeAtk + bonusRangeAtk + buffSkillRangeAtk + armorRangeAtk;
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

    //Armor
    public List<int> purchasedArmorIds;
    public int equipedArmorId;

    public float armorDmg;
    public float armorDefense;
    public float armorLife;
    public float armorCritical;
    public float armorRangeAtk;

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

    //SkillEnabledInLevel
    public float buffSkillPowerUp;
    public float buffSkillAtkSpeed;
    public float buffSkillRangeAtk;
}

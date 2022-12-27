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
            float equips = weaponDmg + armorDmg + helmetDmg;
            float totalAtk = atk * 5 + equips;
            return totalAtk + (totalAtk * bonusDmg / 100) + buffSkillPowerUp;
        } 
    }
    public float Defense
    {
        get {
            float equips = weaponDefense + armorDefense + helmetDefense;
            float totalDef = def / 2 + equips;
            return totalDef +(totalDef * bonusDefense / 100);
        }
    }
    public float MaxLife
    {
        get {
            float equips = weaponLife + armorLife + helmetLife;
            float totalLife = vit * 50 + equips;
            return totalLife + (totalLife * bonusLife / 100);
        }
    }
    public float AtkSpeed
    {
        get {
            float equips = weaponSpeedAtk + armorSpeedAtk + helmetSpeedAtk;
            float speed = (equips - agi * .02f) - (equips * bonusAtkSpeed / 100);
            speed -= (speed * buffSkillAtkSpeed / 100);
            return speed < 0.1f ? 0.1f : speed;
        }
    }
    public float CriticalDMG
    {
        get {
            float totalCritic = cri / 2;
            float equips = weaponCritical + armorCritical + helmetCritical;
            return totalCritic + equips + bonusCritical;
        }
    }
    public float RangeAtk
    {
        get {
            float strRangeBonus = atk / 40;
            float equips = weaponRangeAtk + armorRangeAtk + helmetRangeAtk;
            return equips + bonusRangeAtk + buffSkillRangeAtk + strRangeBonus;
        }
    }

    public float GoldBonus
    {
        get => bonusGold;
    }

    public float MoveSpeed
    {
        get {
            float equips = weaponMoveSpeed + armorMoveSpeed + helmetMoveSpeed;
            return bonusMoveSpeed + equips + (agi / 20);
        }
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
    public float weaponMoveSpeed;

    //Armor
    public List<int> purchasedArmorIds;
    public int equipedArmorId;

    public float armorDmg;
    public float armorDefense;
    public float armorLife;
    public float armorSpeedAtk;
    public float armorCritical;
    public float armorRangeAtk;
    public float armorMoveSpeed;

    //Helmet
    public List<int> purchasedHelmetIds;
    public int equipedHelmetId;

    public float helmetDmg;
    public float helmetDefense;
    public float helmetLife;
    public float helmetSpeedAtk;
    public float helmetCritical;
    public float helmetRangeAtk;
    public float helmetMoveSpeed;

    //SkillsPassivas
    public float skillLevelBonusDmg;
    public float skillLevelBonusDef;
    public float skillLevelBonusLife;
    public float skillLevelBonusAtkSpeed;
    public float skillLevelBonusRange;
    public float skillLevelBonusCritical;
    public float skillLevelBonusGold;
    public float skillLevelBonusMoveSpeed;

    public float bonusDmg;
    public float bonusDefense;
    public float bonusLife;
    public float bonusRangeAtk;
    public float bonusAtkSpeed;
    public float bonusCritical;
    public float bonusGold;
    public float bonusMoveSpeed;

    //Level
    public float levelUnlock;

    //SkillEnabledInLevel
    public float buffSkillPowerUp;
    public float buffSkillAtkSpeed;
    public float buffSkillRangeAtk;
}

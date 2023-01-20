using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float gold;

    //Attributes
    public float atk;
    public float def;
    public float vit;
    public float agi;
    public float cri;


    //Weapons
    public List<int> purchasedWeaponsIds;
    public int equipedWeaponId;

    public float weaponDmg;
    public float weaponDefense;
    public float weaponLife;
    public float weaponAtkSpeed;
    public float weaponCritical;
    public float weaponRangeAtk;
    public float weaponMoveSpeed;

    //Armor
    public List<int> purchasedArmorIds;
    public int equipedArmorId;

    public float armorDmg;
    public float armorDefense;
    public float armorLife;
    public float armorAtkSpeed;
    public float armorCritical;
    public float armorRangeAtk;
    public float armorMoveSpeed;

    //Helmet
    public List<int> purchasedHelmetIds;
    public int equipedHelmetId;

    public float helmetDmg;
    public float helmetDefense;
    public float helmetLife;
    public float helmetAtkSpeed;
    public float helmetCritical;
    public float helmetRangeAtk;
    public float helmetMoveSpeed;

    //Armor
    public List<int> purchasedArmIds;
    public int equipedArmId;

    public float armDmg;
    public float armDefense;
    public float armLife;
    public float armAtkSpeed;
    public float armCritical;
    public float armRangeAtk;
    public float armMoveSpeed;

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
    public float maxLevel;

    //SkillEnabledInLevel
    public float buffSkillPowerUp;
    public float buffSkillDefense;
    public float buffSkillMaxLife; //não usado
    public float buffSkillAtkSpeed;
    public float buffSkillCritical;
    public float buffSkillRangeAtk;
    public float buffSkillMoveSpeed;
    public float Damage 
    {
        get {
            float dmgBaseCalc = atk + weaponDmg;
            float bonusPercentage = 
                bonusDmg + 
                armorDmg + 
                helmetDmg + 
                armDmg +
                buffSkillPowerUp;

            return dmgBaseCalc + (dmgBaseCalc * bonusPercentage / 100);
        } 
    }
    public float Defense
    {
        get {
            float bonusPercentage = 
                (def / 2) + // max 50
                bonusDefense +  //max 10
                weaponDefense + 
                armorDefense + 
                helmetDefense +
                armDefense + 
                buffSkillDefense;

            return bonusPercentage;
        }
    }
    public float MaxLife
    {
        get
        {
            float lifeBaseCalc = 100;
            float multiplier = 5;
            float bonusPercentage =
                vit +
                bonusLife +
                weaponLife + 
                armorLife + 
                helmetLife + 
                armLife + 
                buffSkillMaxLife;

            return (lifeBaseCalc * bonusPercentage / 100) * multiplier;
        }
    }
    public float AtkSpeed
    {
        get {

            float baseCalc = 5;
            float bonusPercentage = 
                (agi / 2) +//max 50
                bonusAtkSpeed +
                weaponAtkSpeed + 
                armorAtkSpeed + 
                helmetAtkSpeed + 
                armAtkSpeed + 
                buffSkillAtkSpeed;

            float result = baseCalc - (baseCalc * bonusPercentage / 100);

            return result > 0.1f ? result : 0.1f;
        }
    }
    public float CriticalDMG
    {
        get {
            float bonusPercentage =
                (cri / 2) + //max 50
                bonusCritical +
                weaponCritical +
                armorCritical +
                helmetCritical +
                armCritical + 
                buffSkillCritical;

            return bonusPercentage;
        }
    }
    public float RangeAtk
    {
        get {
            float baseCalc = atk / 50 + weaponRangeAtk;
            float bonusPercentage =
                bonusRangeAtk +
                armorRangeAtk +
                helmetRangeAtk +
                armRangeAtk + 
                buffSkillRangeAtk; 

            return baseCalc + (baseCalc * bonusPercentage / 100);
        }
    }

    public float GoldBonus
    {
        get => bonusGold;
    }

    public float MoveSpeed
    {
        get {
            float baseCalc = 5;
            float bonusPercentage =
                (agi / 2) + 
                bonusMoveSpeed +
                weaponMoveSpeed +
                armorMoveSpeed +
                helmetMoveSpeed +
                armMoveSpeed + 
                buffSkillMoveSpeed;

            return baseCalc + (baseCalc * bonusPercentage / 100);
        }
    }


}

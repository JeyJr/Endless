using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PanelPassives : MonoBehaviour
{
    private float goldCost = 200;
    private float maxLevel = 11;

    public GameObject panelSkillInfo;
    public TextMeshProUGUI txtSkillName, txtSkillLevel, txtSkillDescription, txtGoldCost;
    public Button btnBuy;
    public Image imgWeapon;

    PassivesAttributes p;
    public float level, bonus, skillGoldCost, gold;

    public void OpenPanelSkillInfo(PassivesAttributes passive)
    {
        p = passive;

        level = GetSkillLevel(passive.SkillShortName) + 1;
        bonus = level * passive.SkillBonus;
        skillGoldCost = level * this.goldCost;

        panelSkillInfo.SetActive(true);

        if (level >= maxLevel)
        {
            txtSkillName.text = passive.SkillName;
            txtSkillLevel.text = "LEVEL MAX";
            txtSkillDescription.text = $"{passive.SkillDesc}{10 * passive.SkillBonus }";
            txtGoldCost.text = "LEVEL MAX";
            imgWeapon.sprite = passive.ImgWeapon;

            btnBuy.interactable = false;
        }
        else
        {
            txtSkillName.text = passive.SkillName;
            txtSkillLevel.text = $"LEVEL: {level}";
            txtSkillDescription.text = $"{passive.SkillDesc}{bonus}";
            txtGoldCost.text = skillGoldCost.ToString();
            imgWeapon.sprite = passive.ImgWeapon;

            btnBuy.interactable = true;
        }

    }

    public void BtnBuy()
    {
        if(gold >= GetSkillLevel(p.SkillShortName) * goldCost)
        {
            SetSkillLevel(p.SkillShortName);
            OpenPanelSkillInfo(p);
        }
    }

    float GetSkillLevel(string skillShortName)
    {
        GameData gameData = ManagerData.Load();
        gold = gameData.gold;
        switch (skillShortName)
        {
            case "dmg":
                return gameData.skillLevelBonusDmg;
            case "def":
                return gameData.skillLevelBonusDef;
            case "life":
                return gameData.skillLevelBonusLife;
            case "speed":
                return gameData.skillLevelBonusAtkSpeed;
            case "range":
                return gameData.skillLevelBonusRange;
            case "gold":
                return gameData.skillLevelBonusGold;
            default:
                throw new ArgumentException("SkillShortName Invalido!", nameof(skillShortName));
        }
    }
    void SetSkillLevel(string skillShortName)
    {
        GameData gameData = ManagerData.Load();
        switch (skillShortName)
        {
            case "dmg":
                gameData.skillLevelBonusDmg++;
                gameData.bonusDmg = gameData.skillLevelBonusDmg * p.SkillBonus;
                break;
            case "def":
                gameData.skillLevelBonusDef++;
                gameData.bonusDefense = gameData.skillLevelBonusDef * p.SkillBonus;
                break;
            case "life":
                gameData.skillLevelBonusLife++;
                gameData.bonusLife = gameData.skillLevelBonusLife * p.SkillBonus;
                break;
            case "speed":
                gameData.skillLevelBonusAtkSpeed++;
                gameData.bonusAtkSpeed = gameData.skillLevelBonusAtkSpeed * p.SkillBonus;
                break;
            case "range":
                gameData.skillLevelBonusRange++;
                gameData.bonusRangeAtk = gameData.skillLevelBonusRange * p.SkillBonus;
                break;
            case "gold":
                gameData.skillLevelBonusGold++;
                gameData.bonusGold = gameData.skillLevelBonusGold * p.SkillBonus;
                break;
            default:
                throw new ArgumentException("SkillShortName Invalido!", nameof(skillShortName));
        }
        gameData.gold -= skillGoldCost;
        GetComponent<UIControl>().GoldAmount(gameData.gold.ToString());
        ManagerData.Save(gameData);
    }

}

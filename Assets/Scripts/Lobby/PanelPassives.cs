using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class PanelPassives : MonoBehaviour
{
    private float goldCost = 2000;
    private float maxLevel = 11;

    public GameObject panelSkillInfo;
    public TextMeshProUGUI txtSkillName, txtDescription, txtGoldCost;
    public Button btnBuy;
    public Image imgWeapon;

    PassivesAttributes p;
    float level, bonus, skillGoldCost, gold;

    public void OpenPanelSkillInfo(PassivesAttributes passive)
    {
        p = passive;

        level = GetSkillLevel(passive.SkillShortName) + 1;

        passive.txtLevel.text = GetSkillLevel(passive.SkillShortName).ToString();

        bonus = level >= maxLevel ? 10 * passive.SkillBonus : level * passive.SkillBonus;
        skillGoldCost = level * this.goldCost;
        

        string skillLevel = $"<b>LEVEL: </b></b>{level}";
        string description;
        if (passive.SkillShortName == "range")
            description = $"Add " + bonus.ToString("F2") + "m" + $" to { passive.SkillDesc}";
        else if(passive.SkillShortName == "gold")
            description = $"Add " + bonus.ToString("F2") + $" to {passive.SkillDesc}";
        else
            description = $"Add " + bonus.ToString("F2") + "%" + $" to {passive.SkillDesc}";

        if (level >= maxLevel)
        {

            txtSkillName.text = passive.SkillName;
            txtDescription.text = $"LEVEL MAX <br></b>{description}";
            txtGoldCost.text = "LEVEL MAX";
            imgWeapon.sprite = passive.ImgWeapon;

            btnBuy.interactable = false;
        }
        else
        {
            txtDescription.text = $"{skillLevel}<br>{description}";
            txtGoldCost.text = $"PRICE: {skillGoldCost}";
            imgWeapon.sprite = passive.ImgWeapon;

            btnBuy.interactable = true;
        }

        panelSkillInfo.SetActive(true);
    }

    public void BtnBuy()
    {
        if(gold >= GetSkillLevel(p.SkillShortName) * goldCost)
        {
            SetSkillLevel(p.SkillShortName);
            OpenPanelSkillInfo(p);
        }
    }

    public float GetSkillLevel(string skillShortName)
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
            case "cri":
                return gameData.skillLevelCritical;
            case "gold":
                return gameData.skillLevelBonusGold;
            default:
                throw new ArgumentException("SkillShortName Invalido!", nameof(skillShortName));
        }
    }
    public void SetSkillLevel(string skillShortName)
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
            case "cri":
                gameData.skillLevelCritical++;
                gameData.bonusCritical = gameData.skillLevelCritical * p.SkillBonus;
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

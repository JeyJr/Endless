using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelPassives : MonoBehaviour
{
    [SerializeField] private float goldCost = 100; //max 1k
    private float maxLevel = 11;

    public GameObject panelSkillInfo;
    public TextMeshProUGUI txtSkillName, txtLevel, txtNextLevel, txtDescription, txtGoldCost;
    public Button btnBuy, btnEquip;
    public Image imgIcon;

    PassivesAttributes p;
    float level, bonus, skillGoldCost, gold;

    public void OpenPanelSkillInfo(PassivesAttributes passive)
    {

        btnBuy.gameObject.SetActive(true);
        btnEquip.gameObject.SetActive(false);

        p = passive;

        level = GetSkillLevel(passive.SkillShortName) + 1;

        bonus = level >= maxLevel ? 10 * passive.SkillBonus : level * passive.SkillBonus;
        skillGoldCost = level * this.goldCost;

        string description = " +" + bonus.ToString("F2") + " " + passive.SkillDesc;

        passive.txtLevel.text = GetSkillLevel(passive.SkillShortName).ToString();
        txtSkillName.text = passive.SkillName;
        txtLevel.text = "Level: " + passive.txtLevel.text;
        imgIcon.sprite = passive.ImgIcon;

        if (level >= maxLevel)
        {
            txtNextLevel.text = $"";
            txtDescription.text = description;

            txtGoldCost.text = "LEVEL MAX";
            btnBuy.interactable = false;
        }
        else
        {
            txtNextLevel.text = $"Next level: {level}";
            txtDescription.text = description;

            txtGoldCost.text = $"PRICE: {skillGoldCost}";
            btnBuy.interactable = true;
        }

        panelSkillInfo.SetActive(true);
    }

    public void BtnBuy()
    {
        if (gold >= (GetSkillLevel(p.SkillShortName) + 1) * goldCost)
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
                return gameData.skillLevelBonusCritical;
            case "gold":
                return gameData.skillLevelBonusGold;
            case "move":
                return gameData.skillLevelBonusMoveSpeed;
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
                gameData.skillLevelBonusCritical++;
                gameData.bonusCritical = gameData.skillLevelBonusCritical * p.SkillBonus;
                break;
            case "range":
                gameData.skillLevelBonusRange++;
                gameData.bonusRangeAtk = gameData.skillLevelBonusRange * p.SkillBonus;
                break;
            case "gold":
                gameData.skillLevelBonusGold++;
                gameData.bonusGold = gameData.skillLevelBonusGold * p.SkillBonus;
                break;
            case "move":
                gameData.skillLevelBonusMoveSpeed++;
                gameData.bonusMoveSpeed = gameData.skillLevelBonusMoveSpeed * p.SkillBonus;
                break;
            default:
                throw new ArgumentException("SkillShortName Invalido!", nameof(skillShortName));
        }
        gameData.gold -= skillGoldCost;
        GetComponent<UIControl>().GoldAmount(gameData.gold.ToString());
        ManagerData.Save(gameData);
    }

}

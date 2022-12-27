using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PanelStatus : MonoBehaviour
{
    [Header("DAMAGE")]
    public TextMeshProUGUI txtTotalDmg;
    public TextMeshProUGUI txtAttributesDmg;
    public TextMeshProUGUI txtPassiveDmg;
    public TextMeshProUGUI txtEquipsDmg;

    [Header("DEFENSE")]
    public TextMeshProUGUI txtTotalDef;
    public TextMeshProUGUI txtAttributesDef;
    public TextMeshProUGUI txtPassiveDef;
    public TextMeshProUGUI txtEquipsDef;

    [Header("LIFE")]
    public TextMeshProUGUI txtTotalLife;
    public TextMeshProUGUI txtAttributesLife;
    public TextMeshProUGUI txtPassiveLife;
    public TextMeshProUGUI txtEquipsLife;

    [Header("SPEED ATK")]
    public TextMeshProUGUI txtTotalSpeedAtk;
    public TextMeshProUGUI txtAttributesAgi;
    public TextMeshProUGUI txtPassiveSpeedAtk;
    public TextMeshProUGUI txtEquipsSpeedAtk;
    
    [Header("CRITICAL")]
    public TextMeshProUGUI txtTotalCritical;
    public TextMeshProUGUI txtAttributesCri;
    public TextMeshProUGUI txtPassiveCritical;
    public TextMeshProUGUI txtEquipsCritical;

    //str/40 = rangeAtk (2.5)
    [Header("RANGE ATK")]
    public TextMeshProUGUI txtTotalRangeAtk;
    public TextMeshProUGUI txtAttributesRangeAtk;
    public TextMeshProUGUI txtPassiveRangeAtk;
    public TextMeshProUGUI txtEquipsRangeAtk;


    //agi/20 = movespeed (5)
    [Header("MOVE SPEED")]
    public TextMeshProUGUI txtTotalMoveSpeed;
    public TextMeshProUGUI txtAttributesMoveSpeed;
    public TextMeshProUGUI txtPassiveMoveSpeed;
    public TextMeshProUGUI txtEquipsMoveSpeed;
    public void PanelPlayerInfoIsActive()
    {
        GameData gd = ManagerData.Load();
        TextDamage(gd);
        TextDefense(gd);
        TextLife(gd);
        TextAgi(gd);
        TextCri(gd);
        TextRangeAtk(gd);
        TextMoveSpeed(gd);
    }

    void TextDamage(GameData gd)
    {
        txtTotalDmg.text = gd.Damage.ToString("F0");
        txtAttributesDmg.text = (gd.atk  * 5).ToString("F0");
        txtPassiveDmg.text = gd.bonusDmg.ToString("F0") + "%";
        txtEquipsDmg.text = (gd.weaponDmg + gd.armorDmg + gd.helmetDmg).ToString();
    }
    void TextDefense(GameData gd)
    {
        txtTotalDef.text = gd.Defense.ToString("F2") + "%";
        txtPassiveDef.text = gd.bonusDefense.ToString("F2") + "%";
        txtAttributesDef.text = (gd.def / 2).ToString() + "%";
        txtEquipsDef.text = (gd.weaponDefense + gd.armorDefense + gd.helmetDefense).ToString("F2") + "%";
    }
    void TextLife(GameData gd)
    {
        txtTotalLife.text = gd.MaxLife.ToString("F0");
        txtPassiveLife.text = gd.bonusLife.ToString("F2") + "%";
        txtAttributesLife.text = (gd.vit * 50).ToString();
        txtEquipsLife.text = (gd.weaponLife + gd.armorLife + gd.helmetLife).ToString("F2");
    }
    void TextAgi(GameData gd)
    {
        txtTotalSpeedAtk.text = gd.AtkSpeed.ToString("F2") + "";
        txtPassiveSpeedAtk.text = gd.bonusAtkSpeed.ToString("F2") + "%";
        txtAttributesAgi.text = (gd.agi * .02f).ToString("F2") + "";
        txtEquipsSpeedAtk.text = (gd.weaponSpeedAtk + gd.armorSpeedAtk + gd.helmetSpeedAtk).ToString("F2") + "";
    }
    void TextCri(GameData gd)
    {
        txtTotalCritical.text = gd.CriticalDMG.ToString("F2") + "%";
        txtPassiveCritical.text = gd.bonusCritical.ToString("F2") + "%";
        txtAttributesCri.text = (gd.cri / 2).ToString("F2") + "%";
        txtEquipsCritical.text = (gd.weaponCritical + gd.armorCritical + gd.helmetCritical).ToString("F2") + "%";
    }

    void TextRangeAtk(GameData gd)
    {
        txtTotalRangeAtk.text = gd.RangeAtk.ToString("F2") + "m";
        txtPassiveRangeAtk.text = gd.bonusRangeAtk.ToString("F2") + "m";
        txtAttributesRangeAtk.text = (gd.atk / 40).ToString("F2") + "m";
        txtEquipsRangeAtk.text = (gd.weaponRangeAtk + gd.armorRangeAtk + gd.helmetRangeAtk).ToString("F2") + "m";
    }

    void TextMoveSpeed(GameData gd)
    {
        txtTotalMoveSpeed.text = gd.MoveSpeed.ToString("F2") + "";
        txtPassiveMoveSpeed.text = gd.bonusMoveSpeed.ToString("F2") + "";
        txtAttributesMoveSpeed.text = (gd.agi / 20).ToString("F2") + "";
        txtEquipsMoveSpeed.text = (gd.weaponMoveSpeed + gd.armorMoveSpeed + gd.helmetMoveSpeed).ToString("F2") + "";
    }

}

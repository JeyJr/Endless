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
    public TextMeshProUGUI txtWeaponDmg, txtArmorDmg, txtHelmetDmg, txtArmDmg;

    [Header("DEFENSE")]
    public TextMeshProUGUI txtTotalDef;
    public TextMeshProUGUI txtAttributesDef;
    public TextMeshProUGUI txtPassiveDef;
    public TextMeshProUGUI txtWeaponDef, txtArmorDef, txtHelmetDef, txtArmDef;

    [Header("LIFE")]
    public TextMeshProUGUI txtTotalLife;
    public TextMeshProUGUI txtAttributesLife;
    public TextMeshProUGUI txtPassiveLife;
    public TextMeshProUGUI txtWeaponLife, txtArmorLife, txtHelmetLife, txtArmLife;

    //[Header("ATK SPEED")]
    //public TextMeshProUGUI txtTotalAtkSpeed;
    //public TextMeshProUGUI txtAttributesAgi;
    //public TextMeshProUGUI txtPassiveAtkSpeed;
    //public TextMeshProUGUI txtWeaponAtkSpeed, txtArmorAtkSpeed, txtHelmetAtkSpeed, txtArmAtkSpeed;
    
    [Header("CRITICAL")]
    public TextMeshProUGUI txtTotalCritical;
    public TextMeshProUGUI txtAttributesCri;
    public TextMeshProUGUI txtPassiveCritical;
    public TextMeshProUGUI txtWeaponCritical, txtArmorCritical, txtHelmetCritical, txtArmCritical;

    [Header("RANGE ATK")]
    public TextMeshProUGUI txtTotalRange;
    public TextMeshProUGUI txtAttributesRange;
    public TextMeshProUGUI txtPassiveRange;
    public TextMeshProUGUI txtWeaponRange, txtArmorRange, txtHelmetRange, txtArmRange;


    [Header("MOVE")]
    public TextMeshProUGUI txtTotalMove;
    public TextMeshProUGUI txtAttributesMove;
    public TextMeshProUGUI txtPassiveMove;
    public TextMeshProUGUI txtWeaponMove, txtArmorMove, txtHelmetMove, txtArmMove;
    public void PanelPlayerInfoIsActive()
    {
        GameData gd = ManagerData.Load();
        TextDamage(gd);
        TextDefense(gd);
        TextLife(gd);
        //TextAgi(gd);
        TextCri(gd);
        TextRangeAtk(gd);
        TextMoveSpeed(gd);
    }

    void TextDamage(GameData gd)
    {
        txtTotalDmg.text = gd.Damage.ToString("F0");
        txtAttributesDmg.text = gd.atk.ToString("F0");
        txtPassiveDmg.text = gd.bonusDmg.ToString("F2") + "%";
        txtWeaponDmg.text = gd.weaponDmg.ToString("F0");
        txtArmorDmg.text = gd.armorDmg.ToString("F2") + "%";
        txtHelmetDmg.text = gd.helmetDmg.ToString("F2") + "%";
        txtArmDmg.text = gd.armDmg.ToString("F2") + "%";
    }
    void TextDefense(GameData gd)
    {
        txtTotalDef.text = gd.Defense.ToString("F2") + "%";
        txtAttributesDef.text = (gd.def / 2).ToString("F2") + "%";
        txtPassiveDef.text = gd.bonusDefense.ToString("F2") + "%";
        txtWeaponDef.text = gd.weaponDefense.ToString("F2") + "%";
        txtArmorDef.text = gd.armorDefense.ToString("F2") + "%";
        txtHelmetDef.text = gd.helmetDefense.ToString("F2") + "%";
        txtArmDef.text = gd.armDefense.ToString("F2") + "%";
    }
    void TextLife(GameData gd)
    {
        txtTotalLife.text = gd.MaxLife.ToString("F0");
        txtAttributesLife.text = gd.vit.ToString("F2") + "%";
        txtPassiveLife.text = gd.bonusLife.ToString("F2") + "%";
        txtWeaponLife.text = gd.weaponLife.ToString("F2") + "%";
        txtArmorLife.text = gd.armorLife.ToString("F2") + "%";
        txtHelmetLife.text = gd.helmetLife.ToString("F2") + "%";
        txtArmLife.text = gd.armLife.ToString("F2") + "%";
    }
    //void TextAgi(GameData gd)
    //{
    //    txtTotalAtkSpeed.text = gd.AtkSpeed.ToString("F2") + " sec";
    //    txtAttributesAgi.text = (gd.agi / 2).ToString("F2") + "%";
    //    txtPassiveAtkSpeed.text = gd.bonusAtkSpeed.ToString("F2") + "%";
    //    txtWeaponAtkSpeed.text = gd.weaponAtkSpeed.ToString("F2") + "%";
    //    txtArmorAtkSpeed.text = gd.armorAtkSpeed.ToString("F2") + "%";
    //    txtHelmetAtkSpeed.text = gd.helmetAtkSpeed.ToString("F2") + "%";
    //    txtArmAtkSpeed.text = gd.armAtkSpeed.ToString("F2") + "%";
    //}
    void TextCri(GameData gd)
    {
        txtTotalCritical.text = gd.CriticalDMG.ToString("F2") + "%";
        txtAttributesCri.text = (gd.cri / 2).ToString("F2") + "%";
        txtPassiveCritical.text = gd.bonusCritical.ToString("F2") + "%";
        txtWeaponCritical.text = gd.weaponCritical.ToString("F2") + "%";
        txtArmorCritical.text = gd.armorCritical.ToString("F2") + "%";
        txtHelmetCritical.text = gd.helmetCritical.ToString("F2") + "%";
        txtArmCritical.text = gd.armCritical.ToString("F2") + "%";
    }

    void TextRangeAtk(GameData gd)
    {
        txtTotalRange.text = gd.RangeAtk.ToString("F2") + " <i>unit</i>";
        txtAttributesRange.text = (gd.atk / 50).ToString("F2") + "u";
        txtPassiveRange.text = gd.bonusRangeAtk.ToString("F2") + "%";
        txtWeaponRange.text = gd.weaponRangeAtk.ToString("F2") + "u";
        txtArmorRange.text = gd.armorRangeAtk.ToString("F2") + "%";
        txtHelmetRange.text = gd.helmetRangeAtk.ToString("F2") + "%";
        txtArmRange.text = gd.armRangeAtk.ToString("F2") + "%";
    }

    void TextMoveSpeed(GameData gd)
    {
        txtTotalMove.text = gd.MoveSpeed.ToString("F2") + "";
        txtAttributesMove.text = (gd.agi / 2).ToString("F2") + "%";
        txtPassiveMove.text = gd.bonusMoveSpeed.ToString("F2") + "%";
        txtWeaponMove.text = gd.weaponMoveSpeed.ToString("F2") + "%";
        txtArmorMove.text = gd.armorMoveSpeed.ToString("F2") + "%";
        txtHelmetMove.text = gd.helmetMoveSpeed.ToString("F2") + "%";
        txtArmMove.text = gd.armMoveSpeed.ToString("F2") + "%";
    }

}

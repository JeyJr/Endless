using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelPlayerInfo : MonoBehaviour
{
    [Header("DAMAGE")]
    public TextMeshProUGUI txtTotalDmg;
    public TextMeshProUGUI txtDmgBonus;
    public TextMeshProUGUI txtAtkAttributes;
    public TextMeshProUGUI txtWeaponAtk;

    [Header("DEFENSE")]
    public TextMeshProUGUI txtTotalDef;
    public TextMeshProUGUI txtDefBonus;
    public TextMeshProUGUI txtDefAttributes;
    public TextMeshProUGUI txtWeaponDef;

    [Header("LIFE")]
    public TextMeshProUGUI txtTotalLife;
    public TextMeshProUGUI txtLifeBonus;
    public TextMeshProUGUI txtVitAttributes;
    public TextMeshProUGUI txtWeaponLife;

    [Header("SPEED ATK")]
    public TextMeshProUGUI txtTotalSpeedAtk;
    public TextMeshProUGUI txtSpeedAtkBonus;
    public TextMeshProUGUI txtAgiAttributes;
    public TextMeshProUGUI txtWeaponSpeedAtk;
    
    [Header("CRITICAL")]
    public TextMeshProUGUI txtTotalCritical;
    public TextMeshProUGUI txtCriticalBonus;
    public TextMeshProUGUI txtCriAttributes;
    public TextMeshProUGUI txtWeaponCritical;

    public void PanelPlayerInfoIsActive()
    {
        GameData gd = ManagerData.Load();
        TextDamage(gd);
        TextDefense(gd);
        TextLife(gd);
        TextAgi(gd);
        TextCri(gd);
    }

    void TextDamage(GameData gd)
    {
        txtTotalDmg.text = gd.Damage.ToString("F0");
        txtDmgBonus.text = gd.bonusDmg.ToString("F0") + "%";
        txtAtkAttributes.text = (gd.atk  * 5).ToString("F0");
        txtWeaponAtk.text = gd.weaponDmg.ToString();
    }
    void TextDefense(GameData gd)
    {
        txtTotalDef.text = gd.Defense.ToString("F2") + "%";
        txtDefBonus.text = gd.bonusDefense.ToString("F2") + "%";
        txtDefAttributes.text = (gd.def / 2).ToString() + "%";
        txtWeaponDef.text = gd.weaponDefense.ToString("F2") + "%";
    }
    void TextLife(GameData gd)
    {
        txtTotalLife.text = gd.MaxLife.ToString("F0");
        txtLifeBonus.text = gd.bonusLife.ToString("F2") + "%";
        txtVitAttributes.text = (gd.vit * 50).ToString();
        txtWeaponLife.text = gd.weaponLife.ToString("F2") + "%";
    }
    void TextAgi(GameData gd)
    {
        txtTotalSpeedAtk.text = gd.AtkSpeed.ToString("F2") + "s";
        txtSpeedAtkBonus.text = gd.bonusAtkSpeed.ToString("F2") + "%";
        txtAgiAttributes.text = (gd.agi * .03f).ToString("F2") + "s";
        txtWeaponSpeedAtk.text = gd.weaponSpeedAtk.ToString("F2") + "s";
    }
    void TextCri(GameData gd)
    {
        txtTotalCritical.text = gd.CriticalDMG.ToString("F2") + "%";
        txtCriticalBonus.text = gd.bonusCritical.ToString("F2") + "%";
        txtCriAttributes.text = (gd.cri / 2).ToString("F2") + "%";
        txtWeaponCritical.text = gd.weaponCritical.ToString("F2") + "%";
    }
}

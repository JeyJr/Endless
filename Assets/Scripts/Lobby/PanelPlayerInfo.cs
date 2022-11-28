using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelPlayerInfo : MonoBehaviour
{
    public TextMeshProUGUI txtDmg, txtDef, txtLife, txtSpeed, txtCritical, txtRange;
    public void PanelPlayerInfoIsActive()
    {
        GameData gd = ManagerData.Load();

        txtDmg.text = $"<color=green>b:{gd.bonusDmg}</color> | <color=blue>a:{gd.atk}</color> | <color=red>w:{gd.weaponDmg}</color> [{gd.Damage}]";
        txtDef.text = $"<color=green>b:{gd.bonusDefense}</color> | <color=blue>a:{gd.def}</color> | <color=red>w:{gd.weaponDefense}</color> [{gd.Defense}]";
        txtLife.text = $"<color=green>b:{gd.bonusLife}</color> | <color=blue>a:{gd.vit}</color> | <color=red>w:{gd.weaponLife}</color> [{gd.MaxLife}]";
        txtSpeed.text = $"<color=green>b:-{gd.bonusAtkSpeed}</color> | <color=blue>a:{gd.agi}</color> | <color=red>w:{gd.weaponSpeedAtk.ToString("F2")}</color> [{gd.AtkSpeed}s]";
        txtRange.text = $"<color=green>b:{gd.bonusRangeAtk}</color> | <color=red>w:{gd.weaponRangeAtk}</color> [{gd.RangeAtk}m]";
        txtCritical.text = $"<color=blue>a:{gd.cri}</color> | <color=red>w:{gd.weaponCritical}</color> [{gd.CriticalDMG}%]";

    }
}

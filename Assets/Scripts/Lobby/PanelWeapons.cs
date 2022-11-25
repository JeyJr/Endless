using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelWeapons : MonoBehaviour
{
    public GameObject panelWeaponInfo;
    public TextMeshProUGUI txtWeaponName,txtWeaponAtk, txtWeaponSpeedAtk;
    public Button btnEquip; //Comprar?
    public Image imgWeapon;

    public WeaponLibrary weaponLibrary;
    int index;


    public void BtnWeapon(WeaponAttributes weaponAttributes)
    {

        this.index = weaponAttributes.WeaponIndex;
        panelWeaponInfo.SetActive(true); //Habilita o painel

        
        txtWeaponName.text = weaponAttributes.WeaponName;
        txtWeaponAtk.text = "Atk: " + weaponAttributes.WeaponAtk.ToString("F0");
        txtWeaponSpeedAtk.text = "Speed Atk: " + weaponAttributes.WeaponSpeedAtk.ToString("F0");
        imgWeapon.sprite = weaponAttributes.ImgWeapon;
    }

    public void BtnEquip()
    {
        weaponLibrary.BtnWeaponToEquip(index);
    }

    public void BtnClose()
    {
        panelWeaponInfo.SetActive(false);
    }
}

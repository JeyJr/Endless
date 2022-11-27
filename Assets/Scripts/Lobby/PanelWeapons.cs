using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelWeapons : MonoBehaviour
{
    public GameObject panelWeaponInfo;
    public TextMeshProUGUI txtWeaponName,txtWeaponAtk, txtWeaponSpeedAtk, txtWeaponAtkRange, txtGoldCost;
    public GameObject btnEquip, btnBuy; //Comprar?
    public Image imgWeapon;

    public WeaponLibrary weaponLibrary;
    public WeaponAttributes weaponAttributes;
    public float goldCost;

    public void BtnWeapon(WeaponAttributes weaponAttributes)
    {
        this.weaponAttributes = weaponAttributes;
        panelWeaponInfo.SetActive(true); //Habilita o painel

        
        txtWeaponName.text = weaponAttributes.WeaponName;
        txtWeaponAtk.text = "Atk: " + weaponAttributes.WeaponAtk.ToString("F0");
        txtWeaponSpeedAtk.text = "Speed Atk: " + weaponAttributes.WeaponSpeedAtk.ToString("F2") + "s";
        txtWeaponAtkRange.text = "Atk Range: " + weaponAttributes.WeaponAtkRange.ToString("F2") + "m";
        imgWeapon.sprite = weaponAttributes.ImgWeapon;

        //Verificar se a arma ja foi comprada
        if (WeaponData.GetWeaponPurchased(weaponAttributes.WeaponID))
        {
            Btns(false, true);
        }
        else
        {
            Btns(true, false);
            txtGoldCost.text = weaponAttributes.GoldCost.ToString();
        }
    }

    public void BtnPurchase()
    {
        GameData gameData = ManagerData.Load();

        if (gameData.gold >= weaponAttributes.GoldCost)
        {
            gameData.gold -= weaponAttributes.GoldCost;
            WeaponData.SetWeaponPurchased(weaponAttributes.WeaponID, 1);
            Btns(false, true);
        }
    }

    void Btns(bool buy, bool equip)
    {
        btnBuy.SetActive(buy);
        btnEquip.SetActive(equip);
    }

    public void BtnEquip()
    {
        weaponLibrary.BtnWeaponToEquip(weaponAttributes.WeaponIndex);
    }
}

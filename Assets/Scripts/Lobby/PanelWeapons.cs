using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PanelWeapons : MonoBehaviour
{
    public GameObject panelWeaponInfo, player;
    public TextMeshProUGUI txtWeaponName,txtWeaponAtk, txtWeaponSpeedAtk, txtWeaponAtkRange, txtGoldCost;
    public GameObject btnEquip, btnBuy; //Comprar?
    public Image imgWeapon;

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

        GameData gameData = ManagerData.Load();

        foreach (int id in gameData.purchasedWeaponsIds)
        {
            if(id == weaponAttributes.WeaponID)
            {
                btnBuy.SetActive(false);
                btnEquip.SetActive(true);
                break;
            }
            else
            {
                btnBuy.SetActive(true);
                btnEquip.SetActive(false);
                txtGoldCost.text = weaponAttributes.GoldCost.ToString();
            }
        }

    }

    public void BtnPurchase()
    {
        
        GameData gameData = ManagerData.Load();

        if (gameData.gold >= weaponAttributes.GoldCost)
        {
            gameData.gold -= weaponAttributes.GoldCost;
            gameData.purchasedWeaponsIds.Add(weaponAttributes.WeaponID);
            
            btnBuy.SetActive(false);
            btnEquip.SetActive(true);
            GetComponent<UIControl>().GoldAmount(gameData.gold.ToString());
            ManagerData.Save(gameData);
        }
    }

    public void BtnEquip()
    {
        GameData gameData = ManagerData.Load();
        gameData.equipedWeaponId = weaponAttributes.WeaponID;
        Debug.Log("Adicionado ID da arma e realizdo o save" + gameData.equipedWeaponId);
        ManagerData.Save(gameData);

        player.GetComponentInChildren<PlayerHand>().EquipWeapon();
    }
}

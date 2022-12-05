using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PanelWeapons : MonoBehaviour
{
    public GameObject panelWeaponInfo, player;
    public TextMeshProUGUI txtName, txtWeaponAttributes, txtGoldCost;
    public GameObject btnEquip, btnBuy; //Comprar?
    public Image imgWeapon;

    public WeaponAttributes weaponAttributes;
    public float goldCost;

    public List<RectTransform> btnPosition;
    public List<GameObject> btns;
    
    public GameObject orderByImg, orderByDescImg;

    bool sequence;

    private void Start()
    {
        sequence = true;
        OrganizingTheBtnSequence(sequence);
    }

    public void BtnOrganizingWeaponWhenOpenPanel()
    {
        OrganizingTheBtnSequence(sequence);
    }

    public void OrganizingTheBtnSequence(bool value)
    {
        sequence = value;

        if(sequence)
        {
            orderByImg.SetActive(false);
            orderByDescImg.SetActive(true);
            btns = btns.OrderBy(obj => obj.name).ToList();
        }
        else
        {
            orderByImg.SetActive(true);
            orderByDescImg.SetActive(false);
            btns = btns.OrderByDescending(obj => obj.name).ToList();
        }

        for (int i = 0; i < btns.Count; i++)
        {
            btns[i].GetComponent<RectTransform>().transform.position = btnPosition[i].transform.position;
        }
    }


    public void BtnWeapon(WeaponAttributes weaponAttributes)
    {
        GameData gameData = ManagerData.Load();
        this.weaponAttributes = weaponAttributes;
        panelWeaponInfo.SetActive(true); //Habilita o painel

        string atk = "ATK: " + weaponAttributes.WeaponAtk.ToString("F0") + "<br>";
        string speedAtk = "SPEED ATK: " + weaponAttributes.WeaponSpeedAtk.ToString("F2") + "s" + "<br>";
        string atkRange = "RANGE:  " + weaponAttributes.WeaponAtkRange.ToString("F2") + "m" + "<br>";
        string life = "";
        string defense = "";
        string critical = "";

        if(weaponAttributes.WeaponLife > 0)
            life = "LIFE: " + weaponAttributes.WeaponLife.ToString("F2") + "%" + "<br>";

        if(weaponAttributes.WeaponDefense > 0)
            defense = "DEF: " + weaponAttributes.WeaponDefense.ToString("F2") + "%" + "<br>";

        if (weaponAttributes.WeaponCritical > 0)
            critical = "CRITICAL: " + weaponAttributes.WeaponCritical.ToString("F2") + "%" + "<br>";

        txtName.text = weaponAttributes.WeaponName;
        txtWeaponAttributes.text = $"{atk}{speedAtk}{atkRange}{life}{defense}{critical}";

        imgWeapon.sprite = weaponAttributes.ImgWeapon;

        //Verificar se a arma ja foi comprada
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
                txtGoldCost.text = $"<b>PRICE: </b>{weaponAttributes.GoldCost}";
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
        ManagerData.Save(gameData);

        player.GetComponentInChildren<PlayerHand>().EquipWeapon();
    }
}

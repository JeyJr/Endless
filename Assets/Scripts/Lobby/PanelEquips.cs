using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelEquips : MonoBehaviour
{
    GameObject player;
    public List<GameObject> panels;
    public List<RectTransform> btnPosition;

    [Header("Panel Info")]
    public GameObject panelInfo;
    public TextMeshProUGUI txtName, txtAttributes, txtGoldCost;
    public GameObject btnEquip, btnBuy; //Comprar?
    public Image imgIcon;

    [Header("Standard Attributes")]
    public float goldCost;

    [Header("Weapon")]
    public WeaponAttributes weaponAttributes;
    public GameObject btnsWeaponRootGroup;
    public List<GameObject> weaponBtns;

    [Header("Armor")]
    public ArmorAttributes armorAttributes;
    public GameObject btnsArmorRootGroup;
    public List<GameObject> armorBtns;


    private void OnEnable()
    {
        BtnSetEnablePanel(0);

        GetUIBtn();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    #region Weapon
    void GetUIBtn()
    {
        if (btnsWeaponRootGroup.transform.childCount > weaponBtns.Count)
        {
            weaponBtns.Clear();
            for (int i = 0; i < btnsWeaponRootGroup.transform.childCount; i++)
            {
                weaponBtns.Add(btnsWeaponRootGroup.transform.GetChild(i).gameObject);
            }
        }

        if(btnsArmorRootGroup.transform.childCount > armorBtns.Count)
        {
            armorBtns.Clear();
            for (int i = 0; i < btnsArmorRootGroup.transform.childCount; i++)
            {
                armorBtns.Add(btnsArmorRootGroup.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < weaponBtns.Count; i++)
            weaponBtns[i].GetComponent<RectTransform>().transform.position = btnPosition[i].transform.position;

        for (int i = 0; i < armorBtns.Count; i++)
            armorBtns[i].GetComponent<RectTransform>().transform.position = btnPosition[i].transform.position;
    }
    public void SetWeaponAttributes(WeaponAttributes w)
    {
        GameData gameData = ManagerData.Load();
        this.weaponAttributes = w;
        panelInfo.SetActive(true); //Habilita o painel

        SetPanelInfoInformations(
            w.WeaponAtk,
            w.WeaponSpeedAtk,
            w.WeaponAtkRange,
            w.WeaponLife,
            w.WeaponDefense,
            w.WeaponCritical,
            w.WeaponName,
            w.ImgWeapon
            );

        foreach (int id in gameData.purchasedWeaponsIds)
        {
            if (id == w.WeaponID)
            {
                btnBuy.SetActive(false);
                btnEquip.SetActive(true);
                break;
            }
            else
            {
                btnBuy.SetActive(true);
                btnEquip.SetActive(false);
                txtGoldCost.text = $"<b>PRICE: </b>{w.GoldCost}";
            }
        }
    }

    public void SetArmorAttributes(ArmorAttributes a)
    {
        GameData gameData = ManagerData.Load();
        this.armorAttributes = a;
        panelInfo.SetActive(true); //Habilita o painel

        SetPanelInfoInformations(
            a.ArmorAtk,
            0,
            a.ArmorAtkRange,
            a.ArmorLife,
            a.ArmorDefense,
            a.ArmorCritical,
            a.ArmorName,
            a.ImgSetIcon
            );

        foreach (int id in gameData.purchasedArmorIds)
        {
            if (id == a.ArmorID)
            {
                btnBuy.SetActive(false);
                btnEquip.SetActive(true);
                break;
            }
            else
            {
                btnBuy.SetActive(true);
                btnEquip.SetActive(false);
                txtGoldCost.text = $"<b>PRICE: </b>{a.GoldCost}";
            }
        }
    }
    public void SetPurchase()
    {
        GameData gameData = ManagerData.Load();

        if (panels[0].activeSelf && gameData.gold >= weaponAttributes.GoldCost)
        {
            gameData.gold -= weaponAttributes.GoldCost;
            gameData.purchasedWeaponsIds.Add(weaponAttributes.WeaponID);

            btnBuy.SetActive(false);
            btnEquip.SetActive(true);
            GetComponent<UIControl>().GoldAmount(gameData.gold.ToString());
            ManagerData.Save(gameData);
        }
        else if(panels[1].activeSelf && gameData.gold >= armorAttributes.GoldCost)
        {
            gameData.gold -= armorAttributes.GoldCost;
            gameData.purchasedArmorIds.Add(armorAttributes.ArmorID);

            btnBuy.SetActive(false);
            btnEquip.SetActive(true);
            GetComponent<UIControl>().GoldAmount(gameData.gold.ToString());
            ManagerData.Save(gameData);
        }
    }
    public void SetEquip()
    {
        GameData gameData = ManagerData.Load();

        if (panels[0].activeSelf)
        {
            gameData.equipedWeaponId = weaponAttributes.WeaponID;
            ManagerData.Save(gameData);

            player.GetComponentInChildren<PlayerSkinManager>().EquipWeapon();
        }
        else if (panels[1].activeSelf)
        {
            gameData.equipedArmorId = armorAttributes.ArmorID;
            ManagerData.Save(gameData);

            player.GetComponentInChildren<PlayerSkinManager>().EquipArmor();
        }

    }
    #endregion

    void SetPanelInfoInformations(float atk, float speedAtk, float atkRange, float life, float defense, float critical, string name, Sprite icon)
    {
        string _atk = "";
        string _speedAtk = "";
        string _atkRange = "";
        string _life = "";
        string _defense = "";
        string _critical = "";

        if (atk > 0)
            _atk = "ATK: " + atk.ToString("F0") + "<br>";

        if(speedAtk > 0)
            _speedAtk = "SPEED ATK: " + speedAtk.ToString("F2") + "s" + "<br>";

        if(atkRange > 0)
            _atkRange = "RANGE:  " + atkRange.ToString("F2") + "m" + "<br>";

        if (life > 0)
            _life = "LIFE: " + life.ToString("F2") + "%" + "<br>";

        if (defense>0)
            _defense = "DEF: " + defense.ToString("F2") + "%" + "<br>";

        if (critical > 0)
            _critical = "CRITICAL: " + critical.ToString("F2") + "%" + "<br>";

        txtAttributes.text = $"{_atk}{_speedAtk}{_atkRange}{_life}{_defense}{_critical}";
        txtName.text = name;
        imgIcon.sprite = icon;
    }
    //Switch panels weapon, armor and helmet
    public void BtnSetEnablePanel(int index)
    {
        for (int i = 0; i < panels.Count; i++)
            panels[i].SetActive(i == index ? true : false);
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelEquips : MonoBehaviour
{
    GameObject player;
    public List<GameObject> panels;
    public GameObject btnPositionsRoot;
    public List<RectTransform> btnPosition;

    [Header("Panel Info")]
    public GameObject panelInfo;
    public TextMeshProUGUI txtName, txtAttributes, txtGoldCost;
    public GameObject btnEquip, btnBuy;
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

    [Header("Helmet")]
    public HelmetAttributes helmetAttributes;
    public GameObject btnsHelmetRootGroup;
    public List<GameObject> helmetBtns;

    [Header("Arms")]
    public ArmAttributes armAttributes;
    public GameObject btnsArmRootGroup;
    public List<GameObject> armBtns;

    [Header("Format")]
    [SerializeField] private string hexAtkColor;
    [SerializeField] private string hexDefColor;
    [SerializeField] private string hexLifeColor;
    [SerializeField] private string hexSpeedAtkColor;
    [SerializeField] private string hexCriticalColor;
    [SerializeField] private string hexRangeColor;
    [SerializeField] private string hexMoveColor;

    [Header("Sprites BTN selected")]
    [SerializeField] private List<Button> btnChangeEquips;
    [SerializeField] private Sprite btnSpriteUnselected, btnSpriteSelected;

    private void OnEnable()
    {
        BtnsPositions();
        BtnSetEnablePanel(0);

        GetUIBtn();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    #region equips
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

        if (btnsArmorRootGroup.transform.childCount > armorBtns.Count)
        {
            armorBtns.Clear();
            for (int i = 0; i < btnsArmorRootGroup.transform.childCount; i++)
            {
                armorBtns.Add(btnsArmorRootGroup.transform.GetChild(i).gameObject);
            }
        }

        if (btnsHelmetRootGroup.transform.childCount > helmetBtns.Count)
        {
            helmetBtns.Clear();
            for (int i = 0; i < btnsHelmetRootGroup.transform.childCount; i++)
            {
                helmetBtns.Add(btnsHelmetRootGroup.transform.GetChild(i).gameObject);
            }
        }

        if (btnsArmRootGroup.transform.childCount > armBtns.Count)
        {
            armBtns.Clear();
            for (int i = 0; i < btnsArmRootGroup.transform.childCount; i++)
            {
                armBtns.Add(btnsArmRootGroup.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < weaponBtns.Count; i++)
            weaponBtns[i].GetComponent<RectTransform>().transform.position = btnPosition[i].transform.position;

        for (int i = 0; i < armorBtns.Count; i++)
            armorBtns[i].GetComponent<RectTransform>().transform.position = btnPosition[i].transform.position;

        for (int i = 0; i < helmetBtns.Count; i++)
            helmetBtns[i].GetComponent<RectTransform>().transform.position = btnPosition[i].transform.position;

        for (int i = 0; i < armBtns.Count; i++)
            armBtns[i].GetComponent<RectTransform>().transform.position = btnPosition[i].transform.position;
    }

    public void BtnsPositions()
    {
        if (btnPositionsRoot.transform.childCount > btnPosition.Count)
        {
            btnPosition.Clear();
            for (int i = 0; i < btnPositionsRoot.transform.childCount; i++)
            {
                btnPosition.Add(btnPositionsRoot.transform.GetChild(i).gameObject.GetComponent<RectTransform>());
            }
        }
    }


    public void SetWeaponAttributes(WeaponAttributes w)
    {

        GameData gameData = ManagerData.Load();
        this.weaponAttributes = w;
        panelInfo.SetActive(true); //Habilita o painel

        SetPanelInfoInformations(true,
            w.WeaponDmg, w.WeaponDefense, w.WeaponLife, w.WeaponAtkSpeed, w.WeaponCritical,
            w.WeaponRangeAtk,w.WeaponMoveSpeed, w.WeaponName, w.ImgWeaponIcon);

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

        SetPanelInfoInformations(false,
            a.ArmorDmg, a.ArmorDefense, a.ArmorLife, a.ArmorAtkSpeed, a.ArmorCritical,
            a.ArmorRangeAtk, a.ArmorMoveSpeed, a.ArmorName, a.ImgArmorIcon);


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

    public void SetHelmetAttributes(HelmetAttributes h)
    {
        GameData gameData = ManagerData.Load();
        helmetAttributes = h;
        panelInfo.SetActive(true); //Habilita o painel

        SetPanelInfoInformations(false,
            h.HelmetDmg, h.HelmetDefense, h.HelmetLife, h.HelmetAtkSpeed, h.HelmetCritical,
            h.HelmetRangeAtk, h.HelmetMoveSpeed, h.HelmetName, h.ImgHelmetIcon);

        foreach (int id in gameData.purchasedHelmetIds)
        {
            if (id == h.HelmetID)
            {
                btnBuy.SetActive(false);
                btnEquip.SetActive(true);
                break;
            }
            else
            {
                btnBuy.SetActive(true);
                btnEquip.SetActive(false);
                txtGoldCost.text = $"<b>PRICE: </b>{h.GoldCost}";
            }
        }
    }

    public void SetArmAttributes(ArmAttributes a)
    {
        GameData gameData = ManagerData.Load();
        armAttributes = a;
        panelInfo.SetActive(true); //Habilita o painel

        SetPanelInfoInformations(false,
            a.ArmDmg, a.ArmDefense, a.ArmLife, a.ArmAtkSpeed, a.ArmCritical,
            a.ArmRangeAtk, a.ArmMoveSpeed, a.ArmName, a.ImgArmIcon);

        foreach (int id in gameData.purchasedArmIds)
        {
            if (id == a.ArmID)
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
        }
        else if(panels[1].activeSelf && gameData.gold >= armorAttributes.GoldCost)
        {
            gameData.gold -= armorAttributes.GoldCost;
            gameData.purchasedArmorIds.Add(armorAttributes.ArmorID);

            btnBuy.SetActive(false);
            btnEquip.SetActive(true);
        }
        else if (panels[2].activeSelf && gameData.gold >= helmetAttributes.GoldCost)
        {
            gameData.gold -= helmetAttributes.GoldCost;
            gameData.purchasedHelmetIds.Add(helmetAttributes.HelmetID);

            btnBuy.SetActive(false);
            btnEquip.SetActive(true);
        }
        else if (panels[3].activeSelf && gameData.gold >= armAttributes.GoldCost)
        {
            gameData.gold -= armAttributes.GoldCost;
            gameData.purchasedArmIds.Add(armAttributes.ArmID);

            btnBuy.SetActive(false);
            btnEquip.SetActive(true);
        }

        GetComponent<UIControl>().GoldAmount(gameData.gold.ToString());
        ManagerData.Save(gameData);
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
        else if (panels[2].activeSelf)
        {
            gameData.equipedHelmetId = helmetAttributes.HelmetID;
            ManagerData.Save(gameData);
            player.GetComponentInChildren<PlayerSkinManager>().EquipHelmet();
        }
        else if (panels[3].activeSelf)
        {
            gameData.equipedArmId = armAttributes.ArmID;
            ManagerData.Save(gameData);
            player.GetComponentInChildren<PlayerSkinManager>().EquipArms();
        }

    }
    #endregion

    void SetPanelInfoInformations(bool weapon, float atk, float defense, float life, float speedAtk, float critical, float atkRange, float moveSpeed, string name, Sprite icon)
    {
        string _atk = "";
        string _defense = "";
        string _life = "";
        string _speedAtk = "";
        string _critical = "";
        string _atkRange = "";
        string _moveSpeed = "";

        if (atk > 0)
        {
            if(weapon)
                _atk = $"<color=#{hexAtkColor}> ATK: </color>" + atk.ToString("F0") + "<br>";
            else
                _atk = $"<color=#{hexAtkColor}> ATK: </color>" + atk.ToString("F2") + "% <br>";
        }
            

        if (defense>0)
            _defense = $"<color=#{hexDefColor}> DEF: </color>" + defense.ToString("F2") + "%" + "<br>";

        if (life > 0)
            _life = $"<color=#{hexLifeColor}> LIFE: </color>" + life.ToString("F2") + "%" + "<br>";

        if(speedAtk > 0)
            _speedAtk = $"<color=#{hexSpeedAtkColor}> SPEED ATK: </color>" + speedAtk.ToString("F2") + "%" + "<br>";

        if (critical > 0)
            _critical = $"<color=#{hexCriticalColor}> CRITICAL: </color>" + critical.ToString("F2") + "%" + "<br>";

        if(atkRange > 0)
        {
            if(weapon)
                _atkRange = $"<color=#{hexRangeColor}> RANGE:  </color>" + atkRange.ToString("F2") + "u" + "<br>";
            else
                _atkRange = $"<color=#{hexRangeColor}> RANGE:  </color>" + atkRange.ToString("F2") + "%" + "<br>";
        }
            

        if (moveSpeed > 0)
            _moveSpeed = $"<color=#{hexMoveColor}> MOVE: </color>" + moveSpeed.ToString("F2") + "% <br>";


        txtAttributes.text = $"{_atk}{_defense}{_life}{_speedAtk}{_critical}{_atkRange}{_moveSpeed}";
        txtName.text = name;
        imgIcon.sprite = icon;
    }
    //Switch panels weapon, armor and helmet
    public void BtnSetEnablePanel(int index)
    {
        for (int i = 0; i < panels.Count; i++)
            panels[i].SetActive(i == index ? true : false);            
    }

    //Set sprite bg color
    public void BtnSetSpriteUnselected()
    {
        for (int i = 0; i < btnChangeEquips.Count; i++)
        {
            btnChangeEquips[i].GetComponent<Image>().sprite = btnSpriteUnselected;
        }
    }

    public void BtnSetSpriteSelected(GameObject img)
    {
        img.GetComponent<Image>().sprite = btnSpriteSelected;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    [Header("Panels")]
    public GameObject[] panels;

    //Gold----------------------------
    [Header("Top Bar")]
    public TextMeshProUGUI textTotalGold;

    [SerializeField] private bool deletar;

    private void Awake()
    {
        if (deletar)
            ManagerData.DeleteData();

        if (ManagerData.Load().firstTime)
        {
            Debug.Log("Primeira vez!");

            GameData d = new();

            d.gold = 5500;
            d.firstTime = false;
            d.atk = 1;
            d.def = 1;
            d.vit = 1;
            d.agi = 1;
            d.cri = 1;

            d.purchasedWeaponsIds = new List<int>();
            d.purchasedWeaponsIds.Add(1000); //Pedaço de madeira
            d.equipedWeaponId = d.purchasedWeaponsIds[0];

            d.purchasedArmorIds = new List<int>();
            d.purchasedArmorIds.Add(1000); //StandardSet
            d.equipedArmorId = d.purchasedArmorIds[0];

            d.purchasedHelmetIds = new List<int>();
            d.purchasedHelmetIds.Add(1000); //StandardSet
            d.equipedHelmetId = d.purchasedHelmetIds[0];

            d.purchasedArmIds = new List<int>();
            d.purchasedArmIds.Add(1000); //StandardSet
            d.equipedArmId = d.purchasedArmIds[0];

            d.skillLevelBonusDmg = 0;
            d.skillLevelBonusDef = 0;
            d.skillLevelBonusLife = 0;
            d.skillLevelBonusAtkSpeed = 0;
            d.skillLevelBonusRange = 0;
            d.skillLevelBonusGold = 0;

            d.levelUnlock = 0;
            ManagerData.Save(d);
        }
        PlayerSkinManager pSkin = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerSkinManager>();
        pSkin.EquipWeapon();
        pSkin.EquipArmor();
        pSkin.EquipHelmet();
        pSkin.EquipArms();

        StartPanels();
        GoldAmount();
    }

    #region PanelsControl
    public void StartPanels()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }
    }

    public void AtivarDesativarPanels(int i)
    {
        for (int j = 0; j < panels.Length; j++)
        {
            if (j == i) 
                panels[i].SetActive(!panels[i].activeSelf);
            else 
                panels[j].SetActive(false);
        }

        if (i == 3) 
            GetComponent<PanelStatus>().PanelPlayerInfoIsActive();

        if (i == 4)
            GetComponent<PanelLevel>().EnableLevel();
    }
    #endregion

    public void GoldAmount(){
        GameData data = ManagerData.Load();
        textTotalGold.text = data.gold.ToString();
    }

    public void GoldAmount(string text)
    {
        textTotalGold.text = text;
    }

    public void BtnClose(GameObject obj) => obj.SetActive(!obj.activeSelf);
}

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

    private void Start()
    {
        //ManagerData.DeleteData(); 

        if (ManagerData.Load().firstTime)
        {
            Debug.Log("Primeira vez!");

            GameData d = new();

            d.gold = 9999999;
            d.firstTime = false;
            d.atk = 1;
            d.def = 1;
            d.vit = 1;
            d.agi = 1;
            d.cri = 1;

            ManagerData.Save(d);
        }

        StartPanels();
        GoldAmount();
    }
    #region PanelsControl
    void StartPanels()
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

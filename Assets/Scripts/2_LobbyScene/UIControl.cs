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

    private void Awake()
    {
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

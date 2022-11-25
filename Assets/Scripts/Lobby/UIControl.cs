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
        GameData.FirstTime();

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

    private void Update()
    {
        GoldAmount();
    }

    void GoldAmount(){
        textTotalGold.text = GameData.GetGold().ToString("F0");
    }
}

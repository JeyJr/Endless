using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    //Panels--------------------------
    [Header("Titulo")]
    public TextMeshProUGUI textTitle;

    [Header("Panels")]
    public GameObject[] panels;

    //Gold----------------------------
    [Header("Top Bar")]
    public TextMeshProUGUI textTotalGold;

    private void Start()
    {
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

        textTitle.text = "";
    }

    public void AtivarDesativarPanels(int i)
    {
        panels[i].SetActive(!panels[i].activeSelf);

        if (i == 0) textTitle.text = "Atributos";

        if (!panels[i].activeSelf) textTitle.text = "";
    }
    #endregion

    private void Update()
    {
        GoldAmount();
    }

    void GoldAmount(){
        textTotalGold.text = GameData.LoadGold().ToString("F0");
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIControl : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject[] panels;


    //Gold----------------------------
    [Header("Top Bar")]
    [SerializeField] private TextMeshProUGUI textTotalGold;

    //BGBtns
    [Header("BTNs")]
    [SerializeField] private GameObject bgBtnSelected;


    private void Awake()
    {
        StartPanels();
        GoldAmount();
    }

    #region PanelsControl
    public void StartPanels()
    {
        DisableAllPanels("");
    }

    void DisableAllPanels(string panelName)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if(panels[i].name != panelName)
            {
                panels[i].SetActive(false);
                bgBtnSelected.SetActive(false);
            }
        }
    }

    public void SetPanelActive(GameObject panel)
    {
        DisableAllPanels(panel.name);

        panel.SetActive(!panel.activeSelf);
        bgBtnSelected.SetActive(panel.activeSelf);

        if (panel.name == "PanelStatus") 
            GetComponent<PanelStatus>().PanelPlayerInfoIsActive();

        if (panel.name == "PanelLevel")
            GetComponent<PanelLevel>().EnableLevel();
    }

    public void SetBGBtnSelected(Transform transform)
    {
        bgBtnSelected.GetComponent<Transform>().position = new Vector3(
            transform.position.x,
            bgBtnSelected.GetComponent<Transform>().position.y,
            bgBtnSelected.GetComponent<Transform>().position.z
            );
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

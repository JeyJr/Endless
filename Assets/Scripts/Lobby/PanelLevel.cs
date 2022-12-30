using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelLevel : MonoBehaviour
{
    [SerializeField] private GameObject btnLevelsRoot;
    [SerializeField] private List<Button> btnLevel;
    [SerializeField] private GameObject panelLevelStart;
    [SerializeField] private TextMeshProUGUI txtTitle;

    string levelName;


    PanelEquips panelEquips;
    private void OnEnable()
    {
        if (btnLevelsRoot.transform.childCount > btnLevel.Count)
        {
            btnLevel.Clear();
            for (int i = 0; i < btnLevelsRoot.transform.childCount; i++)
            {
                btnLevel.Add(btnLevelsRoot.transform.GetChild(i).gameObject.GetComponent<Button>());
            }
        }

        panelEquips = GetComponent<PanelEquips>();

        if (panelEquips.btnPosition.Count == 0)
            panelEquips.BtnsPositions();

        for (int i = 0; i < btnLevel.Count; i++)
        {
            btnLevel[i].GetComponent<RectTransform>().transform.position = panelEquips.btnPosition[i].transform.position;
        }
    }


    public void EnableLevel()
    {
        GameData gameData = ManagerData.Load();

        for (int i = 0; i < btnLevel.Count; i++)
        {
            if (i <= gameData.levelUnlock)
                btnLevel[i].interactable = true;
            else
                btnLevel[i].interactable = false;
        }
    }
    public void BtnOpenPanelLevelStart(GameObject go)
    {
        int num = go.name.IndexOf("l");
        levelName = $"Level{go.name.Substring(num + 1)}";
        txtTitle.text = levelName;
        panelLevelStart.SetActive(true);
    }

    public void BtnLoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}

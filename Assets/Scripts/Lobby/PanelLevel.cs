using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelLevel : MonoBehaviour
{
    [SerializeField]
    private Button[] btnLevel;

    [SerializeField]
    private GameObject panelLevelStart;
    [SerializeField]
    private TextMeshProUGUI txtTitle;
    string levelName;

    public void EnableLevel()
    {
        GameData gameData = ManagerData.Load();

        for (int i = 0; i < btnLevel.Length; i++)
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

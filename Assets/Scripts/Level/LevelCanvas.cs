using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtGold;
    [SerializeField] private LevelController levelController;

    [Header("FPS")]
    [SerializeField] private TextMeshProUGUI txtFPS;
    private float fps, ms;

    [Header("TxtInformation")]
    [SerializeField] private GameObject panelInfo;
    [SerializeField] private TextMeshProUGUI txtInfo;

    [Header("Level Completed")]
    [SerializeField] private GameObject panelMoveToLobby;
    [SerializeField] private GameObject panelYesOrNo, panelEndLevel;
    [SerializeField] private TextMeshProUGUI txtTitlePanelEndLevel,txtTotalEnemiesKilled, txtTotalGoldReceived;
    [SerializeField] private Image iconEnemiesCompleted, iconGoldCompleted;
    [SerializeField] private Sprite imgUnchecked, imgChecked;
    public bool PlayerIsDead { get; set; }

    private void Awake()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        //UI ICONS 

        StartCoroutine(UpdateFPS());
    }

    public void BtnToInvertActiveGameObj(GameObject gameObject) { 
            gameObject.SetActive(!gameObject.activeSelf);
    }
    public void UpdateTxtGold(float value) => txtGold.text = value.ToString();

    #region GAME OVER

    public void OpenPanelGameOver()
    {
        panelMoveToLobby.SetActive(true);
        panelYesOrNo.SetActive(false);
        panelEndLevel.SetActive(true);
        OpenPanelEndLevel();

        txtTitlePanelEndLevel.text = "YOU DIED!";
    }

    #endregion

    #region PANEL END LEVEL (WIN)
    public void OpenPanelMoveToLobby() => BtnToInvertActiveGameObj(panelMoveToLobby);
    public void OpenPanelEndLevel() {
        panelEndLevel.SetActive(true);
        StartCoroutine(LevelEnemiesInformations());
        txtTitlePanelEndLevel.text = "LEVEL COMPLETED!";

        iconEnemiesCompleted.sprite = imgUnchecked;
        iconGoldCompleted.sprite = imgUnchecked;
    }
    IEnumerator LevelEnemiesInformations()
    {
        yield return new WaitForSeconds(1);
        txtTotalEnemiesKilled.text = "0";
        txtTotalGoldReceived.text = "0";
        int enemiesTotal = 0;
        for (int i = 0; i < levelController.TotalEnemiesKilled; i++)
        {
            enemiesTotal++;
            txtTotalEnemiesKilled.text = enemiesTotal.ToString("F0");

            txtTotalGoldReceived.text = 
                "0" + 
                "<color=#FF750B>+ " + enemiesTotal.ToString("F0")+ "</color> <color=#ffffff>" + 
                " (" + 
                enemiesTotal.ToString("F0") + ")</color>";

            yield return new WaitForSeconds(0.01f);
        }
        iconEnemiesCompleted.sprite = imgChecked;
        StartCoroutine(LevelGoldInformations());
    }
    IEnumerator LevelGoldInformations()
    {
        int goldTotal = 0;

        for (int i = 0; i < levelController.GoldTotal; i++)
        {
            goldTotal++;

            txtTotalGoldReceived.text =
                goldTotal.ToString("F0") +
                " <color=#FF750B>+ " + levelController.TotalEnemiesKilled + "</color> <color=#ffffff>" +
                "(" +
                (goldTotal + levelController.TotalEnemiesKilled).ToString("F0") + ")</color>";

            yield return new WaitForSeconds(0.01f);
        }

        iconGoldCompleted.sprite = imgChecked;
    }
    public void BtnBackToLobbyLevelCompleted()
    {
        int levelNum = SceneManager.GetActiveScene().name.IndexOf("l") + 1;
        levelNum = int.Parse(SceneManager.GetActiveScene().name[levelNum..]);


        GameData gameData = ManagerData.Load();
        gameData.gold += levelController.GoldTotal + levelController.TotalEnemiesKilled;

        if (levelNum > gameData.levelUnlock && levelNum < gameData.maxLevel)
            gameData.levelUnlock++;
        else
            Debug.Log("Fase ja foi concluida!");

        ManagerData.Save(gameData);
        SceneManager.LoadScene("Lobby");
    }

    #endregion

    #region FPS - MS
    private IEnumerator UpdateFPS()
    {
        while (true)
        {
            fps = 1f / Time.unscaledDeltaTime;
            ms = Time.unscaledDeltaTime * 1000f;
            txtFPS.text = "FPS: " + fps.ToString("F0") + " | " + "MS: " + ms.ToString("F0");
            yield return new WaitForSeconds(1f);
        }
    }
    #endregion

    public void TextLevelInfo(string text)
    {
        panelInfo.SetActive(true);
        txtInfo.text = text;
        StartCoroutine(TxtLevelInfoHidden());
    }
    IEnumerator TxtLevelInfoHidden()
    {
        yield return new WaitForSeconds(4);
        panelInfo.SetActive(false);
        txtInfo.text = " ";
    }
}

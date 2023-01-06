using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtGold;
    [SerializeField] private LevelController levelController;
    [SerializeField] private GameObject panelMoveToLobby;

    private void Awake()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
    }

    public void UpdateTxtGold(float value) => txtGold.text = value.ToString();
    public void BtnToInvertActiveGameObj(GameObject gameObject) => gameObject.SetActive(!gameObject.activeSelf);
    public void PanelMoveToLobby() => BtnToInvertActiveGameObj(panelMoveToLobby);


    public void BtnBackToLobbyLevelCompleted()
    {

        int levelNum = SceneManager.GetActiveScene().name.IndexOf("l") + 1;
        levelNum = int.Parse(SceneManager.GetActiveScene().name[levelNum..]);


        GameData gameData = ManagerData.Load();
        gameData.gold += levelController.GoldTotal;

        if (levelNum > gameData.levelUnlock && levelNum < gameData.maxLevel)
            gameData.levelUnlock++;
        else
            Debug.Log("Fase ja foi concluida!");

        ManagerData.Save(gameData);
        SceneManager.LoadScene("Lobby");
    }

}

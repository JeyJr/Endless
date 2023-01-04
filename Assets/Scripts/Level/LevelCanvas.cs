using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCanvas : MonoBehaviour
{
    [Header("Panel Gold")]
    [SerializeField] private TextMeshProUGUI txtGold;

    [Header("Panel MoveToNextArea")]
    [SerializeField] private GameObject panelMoveToNextArea;
    [SerializeField] private TextMeshProUGUI txtAlert;

    [Header("Objs")]
    [SerializeField] private LevelController levelController;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
    }

    public void UpdateTxtGold(float value) => txtGold.text = value.ToString();


    #region MoveToNextArea
    public void OpenPanelMoveToNextArea()
    {
        BtnToInvertActiveGameObj(panelMoveToNextArea);
    }

    public void OpenMsgMoveToNextArea(string msg)
    {
        txtAlert.text = msg;
    }


    public void BtnToInvertActiveGameObj(GameObject gameObject)
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void MoveToNextArea()
    {
        
        Debug.Log("Move To Zona 2");
        StartCoroutine(MovePlayer());
    }

    IEnumerator MovePlayer()
    {
        yield return null;

        if (levelController.ZoneOneCompleted && !levelController.ZoneTwoCompleted && !levelController.ZoneThreeCompleted)
            player.GetComponent<Transform>().localPosition = new Vector3(130, 1, 0);

        else if (levelController.ZoneOneCompleted && levelController.ZoneTwoCompleted && !levelController.ZoneThreeCompleted)
            player.GetComponent<Transform>().localPosition = new Vector3(300, 1, 0);

        else if (levelController.ZoneOneCompleted && levelController.ZoneTwoCompleted && levelController.ZoneThreeCompleted)
        {
            GameData gameData = ManagerData.Load();
            gameData.gold += levelController.GoldTotal;
            ManagerData.Save(gameData);

            SceneManager.LoadScene("Lobby");
        }
    }

    #endregion


}

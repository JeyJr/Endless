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
    [SerializeField] private GameObject panelMoveToLobby;

    [Header("UI - SkillsIcon")]
    [SerializeField] private GameObject rootUIIconsPosition;
    [SerializeField] private Sprite standarSprite;

    [Space(5)]
    public List<GameObject> uiSkillIcon;
    public List<Vector3> uiIconPosition;

    [Header("FPS")]
    [SerializeField] private TextMeshProUGUI txtFPS;
    private float fps, ms;

    [Header("TxtInformation")]
    [SerializeField] private GameObject panelInfo;
    [SerializeField] private TextMeshProUGUI txtInfo;

    private void Awake()
    {
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        //UI ICONS 

        StartCoroutine(UpdateFPS());
        StartCoroutine(GetAllPositions());
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



    #region UI - SkillsIcon
    IEnumerator GetAllPositions()
    {
        uiIconPosition.Clear();
        uiSkillIcon.Clear();

        for(int i = 0; i < rootUIIconsPosition.transform.childCount; i++)
        {
            uiSkillIcon.Add(rootUIIconsPosition.transform.GetChild(i).gameObject);
            uiIconPosition.Add(rootUIIconsPosition.transform.GetChild(i).transform.position);
        }

        uiIconPosition.Sort((a, b) => a.x.CompareTo(b.x));


        standarSprite = uiSkillIcon[0].GetComponent<Image>().sprite;

        StartCoroutine(OrganizingAllPositions());
        yield return null;
    }
    IEnumerator OrganizingAllPositions()
    {
        for (int i = 0; i < uiIconPosition.Count; i++)
        {
            uiSkillIcon[i].transform.position = uiIconPosition[i];
        }
        yield return null;
    }

   
    //habilitar e desabilitar sprite 
    public bool CheckSkillActivated(string name)
    {
        for (int i = 0; i < uiSkillIcon.Count; i++)
        {
            if (uiSkillIcon[i].GetComponent<Image>().sprite.name == name)
                return true;
        }

        return false;
    }
    public void EnableUISkillSlot(Sprite sprite) => StartCoroutine(EnableUISkill(sprite));
    public void DisableUISkillICon(string name)=>StartCoroutine(DisablebleUISkill(name));
    IEnumerator EnableUISkill(Sprite sprite)
    {
        for (int i = 0; i < uiSkillIcon.Count; i++)
        {
            if (!uiSkillIcon[i].activeSelf)
            {
                uiSkillIcon[i].GetComponent<Image>().sprite = sprite;
                uiSkillIcon[i].SetActive(true);
                break;
            }
        }
        yield return null;
    }
    IEnumerator DisablebleUISkill(string name)
    {
        for (int i = 0; i < uiSkillIcon.Count; i++)
        {
            if (uiSkillIcon[i].GetComponent<Image>().sprite.name == name)
            {
                uiSkillIcon[i].SetActive(false);
                uiSkillIcon[i].GetComponent<Image>().sprite = standarSprite;

                break;
            }
        }
        yield return null;
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

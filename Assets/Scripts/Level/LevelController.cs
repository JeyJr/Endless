using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    [SerializeField] private float totalBossesKilled;
    [SerializeField] private float totalEnemiesKilled;

    [Header("Zone")]
    [SerializeField] private bool zoneOneCompleted;
    [SerializeField] private bool zoneTwoCompleted;
    [SerializeField] private bool zoneThreeCompleted;

    public bool ZoneOneCompleted { get => zoneOneCompleted; set => zoneOneCompleted = value; }
    public bool ZoneTwoCompleted { get=> zoneTwoCompleted; set => zoneTwoCompleted = value; }
    public bool ZoneThreeCompleted { get=> zoneThreeCompleted; set => zoneThreeCompleted = value; }


    [Header("Gold")]
    [SerializeField] private float goldTotal;
    [SerializeField] private LevelCanvas levelCanvas;
    public float GoldTotal { get => goldTotal;}


    [Header("UI")]
    GameObject canvas;
    [SerializeField] private TextMeshProUGUI txtMsgAlert;
    [SerializeField] private GameObject panelMoveToNextArea;

    [Header("UI - SkillsIcon")]
    [SerializeField] private Sprite standarSprite;
    public List<GameObject> uiSkillIcon;
    public List<Vector3> uiIconPosition;

 
    private void Awake()
    {
        goldTotal = 0;
        totalBossesKilled = 0;
        totalEnemiesKilled = 0;

        //SKIN and WEAPONS
        PlayerSkinManager pSkin = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerSkinManager>();
        pSkin.EquipWeapon();
        pSkin.EquipArmor();
        pSkin.EquipHelmet();

        //GOLD IN UI TEXT
        levelCanvas = GameObject.FindGameObjectWithTag("MainUI").GetComponent<LevelCanvas>();
        levelCanvas.UpdateTxtGold(goldTotal);

        //UI ICONS 
        UISkillIconInitialSetup();
    }


    #region UI - SkillsIcon
    void UISkillIconInitialSetup()
    {
        uiIconPosition.Clear();
        uiSkillIcon.Clear();

        canvas = GameObject.FindGameObjectWithTag("MainUI");

        for (int i = 0; i < canvas.transform.GetChild(0).transform.childCount; i++)
            uiSkillIcon.Add(canvas.transform.GetChild(0).transform.GetChild(i).gameObject);


        for (int i = 0; i < uiSkillIcon.Count; i++)
        {
            uiIconPosition.Add(uiSkillIcon[i].GetComponent<RectTransform>().position);
            uiSkillIcon[i].SetActive(false);
        }
        standarSprite = uiSkillIcon[0].GetComponent<Image>().sprite;
    }


    public bool CheckSkillActivated(string name)
    {
        for (int i = 0; i < uiSkillIcon.Count; i++)
        {
            if (uiSkillIcon[i].GetComponent<Image>().sprite.name == name)
                return true;
        }

        return false;
    }

    public void EnableUISkillSlot(Sprite sprite)
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

        OrganizerUiSkillIcon();
    }


    public void DisableUISkillICon(string name)
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
        OrganizerUiSkillIcon();
    }

    public void OrganizerUiSkillIcon()
    {
        uiSkillIcon = uiSkillIcon.OrderByDescending(icon => icon.gameObject.activeSelf).ToList();

        for (int i = 0; i < uiSkillIcon.Count; i++)
            uiSkillIcon[i].GetComponent<RectTransform>().transform.position = uiIconPosition[i];
        
    }
    #endregion

    #region EnemyDead and GolTotalControl
    public void EnemyDead(float goldDroped, bool boss)
    {
        GameData gameData = ManagerData.Load();

        if (boss)
            totalBossesKilled++;
        else
            totalEnemiesKilled++;

        float gold = gameData.bonusGold + goldDroped;

        goldTotal += gold;
        levelCanvas.UpdateTxtGold(goldTotal);
    }


    #endregion

    #region LevelCompleted
    public void LevelCompleted(int num)
    {
        GameData gameData = ManagerData.Load();

        if (num > gameData.levelUnlock)
            gameData.levelUnlock++;
        else
            Debug.Log("Fase ja foi concluida!");


        ManagerData.Save(gameData);
        StartCoroutine(BackLobby());
    }

    IEnumerator BackLobby()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Lobby");
    }
    #endregion


    
}

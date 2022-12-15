using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
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


    [Header("Gold")]
    [SerializeField] private float goldTotal;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private GameObject txtGold;
    [SerializeField] private TextMeshProUGUI txtUIReceivedGold;

    [Header("UI - SkillsIcon")]
    public List<Image> uiSlotsIconSkills;
    public List<string> uiIconName;

    private void Start()
    {
        goldTotal = 0;
        totalBossesKilled = 0;
        totalEnemiesKilled = 0;
        txtUIReceivedGold.text = goldTotal.ToString();

        for (int i = 0; i < uiSlotsIconSkills.Count; i++)
        {
            uiSlotsIconSkills[i].enabled = false;
        }

        for (int i = 0; i < 7; i++)
        {
            uiIconName.Add(" ");
        }
    }



    #region UI - SkillsIcon
    public void EnableUISkillSlot(Sprite img)
    {
        for (int i = 0; i < uiSlotsIconSkills.Count; i++)
        {
            if (!uiSlotsIconSkills[i].enabled)
            {
                uiSlotsIconSkills[i].sprite = img;
                uiSlotsIconSkills[i].enabled = true;
                uiIconName[i] = img.name;
                break;
            }
        }

    }

    public void DisableUISkillICon(string name)
    {
        for (int i = 0; i < uiIconName.Count; i++)
        {
            if (uiIconName[i] == name)
            {
                uiSlotsIconSkills[i].enabled = false;
                uiIconName[i] = " ";
                break;
            }
        }
    }

    #endregion

    #region EnemyDead and GolTotalControl
    public void EnemyDead(float goldDroped, bool boss)
    {
        if (boss)
            totalBossesKilled++;
        else
            totalEnemiesKilled++;

        txtGold.GetComponent<TextMeshPro>().text = $"Gold +{goldDroped}";
        Instantiate(txtGold, spawnPosition.position, Quaternion.Euler(0, 0, 0));
        StartCoroutine(AddGoldToGoldTotal(goldDroped));
    }

    IEnumerator AddGoldToGoldTotal(float value)
    {
        for (int i = 0; i < value; i++)
        {
            goldTotal++;
            txtUIReceivedGold.text = goldTotal.ToString();
            yield return new WaitForSeconds(.2f);
        }
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

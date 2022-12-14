using System.Collections;
using System.Collections.Generic;
using TMPro;
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
           
    }

    #region UI - SkillsIcon
    public void EnableUISkillSlot(Sprite img, string name)
    {
        foreach (var item in uiSlotsIconSkills)
        {
            if(item.enabled && item.sprite.name == name)
            {
                return;
            }
            else if(!item.enabled)
            {
                item.sprite = img;
                item.sprite.name = name;
                item.enabled = true;
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

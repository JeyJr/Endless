using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    [Header("Skills")]
    [SerializeField] private GameObject skill;
    [SerializeField] private Transform spawnSkillPositionZ1; 


    private void Start()
    {
        goldTotal = 0;
        totalBossesKilled = 0;
        totalEnemiesKilled = 0;

        SpawnSkill();
    }

    public void EnemyDead(float goldDroped, bool boss)
    {
        if (boss)
            totalBossesKilled++;
        else
            totalEnemiesKilled++;

        txtGold.GetComponent<TextMeshPro>().text = $"Gold +{goldDroped}";
        Instantiate(txtGold, spawnPosition.position, Quaternion.Euler(0, 0, 0));
        goldTotal += goldDroped;

        if (totalEnemiesKilled % 10 == 0)
            SpawnSkill();
    }

    private void SpawnSkill()
    {
        Instantiate(skill, spawnSkillPositionZ1.position, Quaternion.Euler(0, 0, 0));
    }

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

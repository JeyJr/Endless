using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class LevelController : MonoBehaviour
{
    [SerializeField] private float totalBossesKilled;
    [SerializeField] private float totalEnemiesKilled;
    public float TotalEnemiesKilled { get => totalEnemiesKilled;}


    [Header("SpawnControl")]
    [SerializeField] private Transform spawnCentralPoint;
    float rangeToSpawn = 55;

    [Header("Boss Control")]
    [SerializeField] private bool bossDead;
    public bool BossDead { get => bossDead; set => bossDead = value; }

    [Header("EnemysToSpawn")]
    [SerializeField] private int enemiesSpawned;

    [Header("Gold")]
    [SerializeField] private float goldTotal, bonusGold;
    [SerializeField] private LevelCanvas levelCanvas;

    [Header("Mission")]
    [SerializeField]  private LevelQuest levelQuest;
    bool bossSpawned;

    [Header("EnemysCollection")]
    EnemysCollection enemysCollection;

    SFXControl sfxControl;

    public float GoldTotal { get => goldTotal;}

    private void Start()
    {
        goldTotal = 0;
        totalBossesKilled = 0;
        totalEnemiesKilled = 0;

        //GOLD IN UI TEXT
        levelCanvas = GameObject.FindGameObjectWithTag("MainUI").GetComponent<LevelCanvas>();
        levelQuest = GameObject.FindGameObjectWithTag("MainUI").GetComponent<LevelQuest>();
        levelCanvas.UpdateTxtGold(goldTotal);

        enemysCollection = GameObject.FindGameObjectWithTag("EnemysCollection").GetComponent<EnemysCollection>();

        GameData gameData = ManagerData.Load();
        bonusGold = gameData.bonusGold;

        CheckQuestOneCompleted();
        CheckQuestTwoCompleted();

        StartCoroutine(Instructions($"Defeat <color=#F15826>{enemysCollection.enemiesKilledToSpawnBoss}</color> enemies\n to spawn boss!"));

        sfxControl = GameObject.FindWithTag("SFX").GetComponent<SFXControl>();

    }

    public void StartSpawnEnemies()
    {
        StartCoroutine(SpawnSimpleEnemies());
    }
    public async void EnemyDead(float goldDroped, bool boss)
    {

        if (!boss)
            totalEnemiesKilled++;
        else
        {
            bossDead = true;
            totalBossesKilled++;
            StartCoroutine(Instructions("Boss defeated! \nAdvance to the portal"));
        }
            

        CheckQuestOneCompleted();
        CheckQuestTwoCompleted();

        int gold = Mathf.RoundToInt(bonusGold + goldDroped);
        Task task = goldUp(gold);
        await task;

        enemiesSpawned--;

        if(!bossDead)
            StartCoroutine(SpawnSimpleEnemies());
    }


    void CheckQuestOneCompleted()
    {
        if (totalEnemiesKilled >= enemysCollection.enemiesKilledToSpawnBoss && !bossSpawned)
            StartCoroutine(SpawnBoss());

        if (totalEnemiesKilled >= enemysCollection.enemiesKilledToSpawnBoss)
            levelQuest.SetQuestOne(true, $"Dead enemies {enemysCollection.enemiesKilledToSpawnBoss} / {totalEnemiesKilled}");
        else
            levelQuest.SetQuestOne(false, $"Dead enemies {enemysCollection.enemiesKilledToSpawnBoss} / {totalEnemiesKilled}");
    }

    void CheckQuestTwoCompleted()
    {
        if (bossDead)
            levelQuest.SetQuestTwo(true, $"Boss defeated!");
        else
            levelQuest.SetQuestTwo(false, $"Defeat the boss");
    }


    IEnumerator SpawnSimpleEnemies()
    {
        yield return new WaitForSeconds(2);
        while(enemiesSpawned < enemysCollection.maxEnemiesToSpawn)
        {
            int index = Mathf.RoundToInt(Random.Range(0, enemysCollection.enemiesToSpawn.Count));

            Vector3 pos = new Vector3(
                Random.Range(spawnCentralPoint.position.x - rangeToSpawn, spawnCentralPoint.position.x + rangeToSpawn),
                spawnCentralPoint.position.y,
                spawnCentralPoint.position.z);

            Instantiate(enemysCollection.enemiesToSpawn[index], pos, Quaternion.identity);
            enemiesSpawned++;
            yield return new WaitForEndOfFrame();
        }
    }
    async Task goldUp(int value)
    {
        sfxControl.PlayClip(SFXClip.coin);

        for (int i = 0; i < value; i++)
        {
            goldTotal++;
            levelCanvas.UpdateTxtGold(goldTotal);
            await Task.Delay(100);
        }
    }
    IEnumerator Instructions(string msg)
    {
        yield return new WaitForSeconds(3);
        levelCanvas.TextLevelInfo(msg);
    }
    IEnumerator SpawnBoss()
    {
        bossSpawned = true;
        yield return new WaitForSeconds(5);
        StartCoroutine(Instructions("Defeat the stage <color=#F15826>boss</color>!"));
        Vector3 pos = new Vector3(spawnCentralPoint.position.x, spawnCentralPoint.position.y, 15);
        Instantiate(enemysCollection.boss, pos, Quaternion.identity);
        sfxControl.PlayClip(SFXClip.bossSpawned);
    }
}

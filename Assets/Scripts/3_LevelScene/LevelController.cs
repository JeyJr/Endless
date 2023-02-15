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
    public bool startSpawn;
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
        Task task = GoldUp(gold);
        await task;

        enemiesSpawned--;


    }

    private void Update()
    {
        if(enemiesSpawned < enemysCollection.maxEnemiesToSpawn && startSpawn)
        {
            SpawnSimpleEnemies();
        }

        if(totalEnemiesKilled >= enemysCollection.enemiesKilledToSpawnBoss && !bossSpawned)
        {
            bossSpawned = true;

            Vector3 pos = new Vector3(spawnCentralPoint.position.x, spawnCentralPoint.position.y, 15);
            Instantiate(enemysCollection.boss, pos, Quaternion.identity);
            sfxControl.PlayClip(SFXClip.bossSpawned);

            StartCoroutine(Instructions("Defeat the stage <color=#F15826>boss</color>!"));
        }
    }

    void CheckQuestOneCompleted()
    {
        if (totalEnemiesKilled >= enemysCollection.enemiesKilledToSpawnBoss)
            levelQuest.SetQuestOne(true, $"Dead enemies {enemysCollection.enemiesKilledToSpawnBoss} / {totalEnemiesKilled}");
        else
            levelQuest.SetQuestOne(false, $"Dead enemies {enemysCollection.enemiesKilledToSpawnBoss} / {totalEnemiesKilled}");
    }
    void CheckQuestTwoCompleted()
    {
        if (bossDead)
        {
            levelQuest.SetQuestTwo(true, $"Boss defeated!");
            startSpawn = false;
        }
        else
        {
            levelQuest.SetQuestTwo(false, $"Defeat the boss");
        }
    }
    void SpawnSimpleEnemies()
    {
        int index = Mathf.RoundToInt(Random.Range(0, enemysCollection.enemiesToSpawn.Count));

        Vector3 pos = new Vector3(
            Random.Range(spawnCentralPoint.position.x - rangeToSpawn, spawnCentralPoint.position.x + rangeToSpawn),
            spawnCentralPoint.position.y,
            spawnCentralPoint.position.z);

        Instantiate(enemysCollection.enemiesToSpawn[index], pos, Quaternion.identity);
        enemiesSpawned++;
    }
    private async Task GoldUp(int value)
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
}

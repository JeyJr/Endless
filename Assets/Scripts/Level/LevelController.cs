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
    [SerializeField] private GameObject boss;
    [SerializeField] private bool bossDead;
    public bool BossDead { get => bossDead; set => bossDead = value; }

    [Header("EnemysToSpawn")]
    [SerializeField] private int enemiesSpawned;
    [SerializeField] private int maxEnemiesToSpawn;
    [SerializeField] private List<GameObject> enemiesToSpawn;



    [Header("Gold")]
    [SerializeField] private float goldTotal, bonusGold;
    [SerializeField] private LevelCanvas levelCanvas;

    [Header("Mission")]
    [SerializeField]  private int enemiesKilledToSpawnBoss;
    [SerializeField]  private LevelQuest levelQuest;
    bool bossSpawned;
    
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

        GameData gameData = ManagerData.Load();
        bonusGold = gameData.bonusGold;

        CheckQuestOneCompleted();
        CheckQuestTwoCompleted();

        StartCoroutine(SpawnSimpleEnemies());
        StartCoroutine(InitialInstructions($"Defeat <color=#F15826>{enemiesKilledToSpawnBoss}</color> enemies\n to spawn boss!"));

    }
    public async void EnemyDead(float goldDroped, bool boss)
    {

        if (!boss)
            totalEnemiesKilled++;
        else
        {
            bossDead = true;
            totalBossesKilled++;
            StartCoroutine(InitialInstructions("Boss defeated! \nAdvance to the portal"));
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
        if (totalEnemiesKilled >= enemiesKilledToSpawnBoss && !bossSpawned)
            StartCoroutine(SpawnBoss());

        if (totalEnemiesKilled >= enemiesKilledToSpawnBoss)
            levelQuest.SetQuestOne(true, $"Dead enemies {enemiesKilledToSpawnBoss} / {totalEnemiesKilled}");
        else
            levelQuest.SetQuestOne(false, $"Dead enemies {enemiesKilledToSpawnBoss} / {totalEnemiesKilled}");
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
        while(enemiesSpawned < maxEnemiesToSpawn)
        {
            int index = Mathf.RoundToInt(Random.Range(0, enemiesToSpawn.Count));

            Vector3 pos = new Vector3(
                Random.Range(spawnCentralPoint.position.x - rangeToSpawn, spawnCentralPoint.position.x + rangeToSpawn),
                spawnCentralPoint.position.y,
                spawnCentralPoint.position.z);

            Instantiate(enemiesToSpawn[index], pos, Quaternion.identity);
            enemiesSpawned++;
            yield return new WaitForEndOfFrame();
        }
    }
    async Task goldUp(int value)
    {
        for (int i = 0; i < value; i++)
        {
            goldTotal++;
            levelCanvas.UpdateTxtGold(goldTotal);
            await Task.Delay(100);
        }
    }
    IEnumerator InitialInstructions(string msg)
    {
        yield return new WaitForSeconds(3);
        levelCanvas.TextLevelInfo(msg);
    }
    IEnumerator SpawnBoss()
    {
        bossSpawned = true;
        yield return new WaitForSeconds(5);
        StartCoroutine(InitialInstructions("Defeat the stage <color=#F15826>boss</color>!"));
        Instantiate(boss, spawnCentralPoint.position, Quaternion.identity);
    }
}

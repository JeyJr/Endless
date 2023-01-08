using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelController : MonoBehaviour
{
    [SerializeField] private float totalBossesKilled;
    [SerializeField] private float totalEnemiesKilled;

    [Header("Boss Control")]
    [SerializeField] private bool bossDead;
    public bool BossDead { get => bossDead; set => bossDead = value; }

    [Header("EnemysToSpawn")]
    [SerializeField] private List<GameObject> enemiesToSpawn;

    [Header("Gold")]
    [SerializeField] private float goldTotal, bonusGold;
    [SerializeField] private LevelCanvas levelCanvas;
    public float GoldTotal { get => goldTotal;}

    private void Start()
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


        GameData gameData = ManagerData.Load();
        bonusGold = gameData.bonusGold;
    }
    public void EnemyDead(float goldDroped, bool boss)
    {
        if (boss)
            totalBossesKilled++;
        else
            totalEnemiesKilled++;

        float gold = bonusGold + goldDroped;

        goldTotal += gold;
        levelCanvas.UpdateTxtGold(goldTotal);
    }

}

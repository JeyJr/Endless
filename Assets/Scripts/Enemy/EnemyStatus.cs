using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float atk;
    [SerializeField] private float def;
    [SerializeField] private float vit;
    [SerializeField] private float agi;
    [SerializeField] private float cri;
    [SerializeField] private float goldDrop;
    [SerializeField] private float buffDrop;

    [Space(5)]
    [Header("Status")]
    [SerializeField] private float maxLife;
    [SerializeField] private float life;

    public float Damage { get => atk * 1; }
    public float Defense { get => def / 2; }
    public float Life { get => life; }
    public float AtkSpeed { get => 7 - (agi * 0.05f); }
    public float Critical { get => cri; }
    public float GoldDrop { get => goldDrop; }
    public float BuffDrop { get => buffDrop; }

    [Space(5)]
    [Header("Txt To Spawn")]
    public SpawnText spawnTextDMG;
    [SerializeField] private GameObject spawnSkill;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float yDropSkill;

    [Space(5)]
    [Header("Others Control")]
    [SerializeField] private bool boss;
    [SerializeField] private bool enemyInTestZone;
    [SerializeField] private string enemyAnimName;
    [SerializeField] private GameObject thisEnemyFullObj;
    bool enemyIsAlive;

    public bool IsAttacking { get; set;}
    public bool EnemyIsAlive { get => enemyIsAlive; private set => enemyIsAlive = value; }
    public string EnemyAnimName { get => enemyAnimName;}

    private void Awake()
    {
        maxLife = vit * 1;
        life = maxLife;
        EnemyIsAlive = true;
    }

    public void LoseLife(float dmg, bool critical)
    {
        float realDMG = dmg - ((dmg * Defense) / 100);
        spawnTextDMG.SpawnTextDamage(realDMG, critical);

        if (!enemyInTestZone)
            life -= realDMG;

        if (life <= 0)
        {
            EnemyIsAlive = false;
            GetComponent<Animator>().Play($"Base Layer.{enemyAnimName}_Dead", 0);
        }
    }


    //this method is called on the last frame: Anim Dead;
    public void EnemyDrops()
    {
        if (Random.Range(0, 100) <= buffDrop)
        {
            Vector3 pos = new(spawnPosition.position.x, yDropSkill, 6);
            Instantiate(spawnSkill, pos, Quaternion.Euler(0, 0, 0));
        }

        GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>().EnemyDead(GoldDrop, boss);
        Destroy(thisEnemyFullObj);
    }
}

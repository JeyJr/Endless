using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private float atk;
    [SerializeField] private float def;
    [SerializeField] private float vit;
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
    [Header("Anim Control")]
    [SerializeField] private string enemyAnimName;
    public bool IsAttacking { get; set; }
    public string EnemyAnimName { get => enemyAnimName; }

    [Space(5)]
    [Header("Others Control")]
    [SerializeField] private bool boss;
    [SerializeField] private bool enemyInTestZone;
    [SerializeField] private GameObject thisEnemyFullObj;
    bool isAlive;

    public bool IsAlive { get => isAlive; private set => isAlive = value; }

    private void Awake()
    {
        maxLife = vit * 1;
        life = maxLife;
        IsAlive = true;

        atk = Random.Range(atk, (atk + 1) * 2);
        def = Random.Range(def, (def + 1) * 2);
        vit = Random.Range(vit, (vit + 1) * 2);
        cri = Random.Range(cri, (cri + 1) * 2);
    }

    public void LoseLife(float dmg, bool critical)
    {
        float realDMG = dmg - ((dmg * Defense) / 100);
        spawnTextDMG.SpawnTextDamage(realDMG, critical);

        if (!enemyInTestZone)
            life -= realDMG;

        if (life <= 0)
        {
            IsAlive = false;
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


    //Called on animations atk
    public void TargetDmg() => GetComponentInParent<EnemyBehavior>().SetTargetDamage(Damage, Critical);
    public void SetAwait(){
        IsAttacking = !IsAttacking;
        GetComponentInParent<EnemyBehavior>().SetAwait();
    }
}

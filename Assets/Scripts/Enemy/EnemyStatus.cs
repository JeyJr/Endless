using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [Header("Attributes")]
    private float skillDrop  = 5;
    [SerializeField] private float goldDrop;

    [SerializeField] private float atk;
    [SerializeField] private float def;
    [SerializeField] private float vit;
    [SerializeField] private float cri;
    public float GoldDrop { get => goldDrop; }
    public float SkillDrop { get => skillDrop; }
    public float Damage { get => atk * 2; }
    public float Defense { get => def / 2; }
    public float Life { get => life; }
    public float Critical { get => cri; }


    [Space(5)]
    [Header("Status")]
    [SerializeField] private float maxLife;
    [SerializeField] private float life;

    [Space(5)]
    [Header("Txt To Spawn")]
    public SpawnText spawnTextDMG;
    [SerializeField] private Transform spawnPosition;

    [Space(5)]
    [Header("Anim Control")]
    [SerializeField] private string enemyAnimName;
    public bool IsAttacking { get; set; }
    public string EnemyAnimName { get => enemyAnimName; }

    [Header("SpawnSkills")]
    [SerializeField] private List<GameObject> skillsToSpawn;

    [Space(5)]
    [Header("Others Control")]
    [SerializeField] private bool boss;
    [SerializeField] private bool enemyInTestZone;
    [SerializeField] private GameObject thisEnemyFullObj;
    bool isAlive;

    public bool IsAlive { get => isAlive; private set => isAlive = value; }

    SFXControl sfxControl;

    private void Awake()
    {
        maxLife = vit * 10;
        life = maxLife;
        IsAlive = true;

        GetComponent<EnemyLifeBar>().SetSliderInitialValues(maxLife, life);
        sfxControl = GameObject.FindWithTag("SFX").GetComponent<SFXControl>();
    }

    public void LoseLife(float dmg, bool critical)
    {
        float realDMG = dmg - ((dmg * Defense) / 100);
        spawnTextDMG.SpawnTextDamage(realDMG, critical);

        if (!enemyInTestZone)
            life -= realDMG;

        if (life <= 0 && !enemyInTestZone)
        {
            IsAlive = false;
            GetComponent<Animator>().Play($"Base Layer.{enemyAnimName}_Dead", 0);
        }

        sfxControl.PlayClip(SFXClip.hitTarget);
        GetComponent<EnemyLifeBar>().SetLifeBarValues(life);
    }

    //this method is called on the last frame: Anim Dead;
    public void EnemyDrops()
    {
        if(Random.Range(0, 100) <= SkillDrop)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 4, -5);
            Instantiate
            (
                skillsToSpawn[Mathf.RoundToInt(Random.Range(0, skillsToSpawn.Count))], 
                pos, 
                Quaternion.identity
            );
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

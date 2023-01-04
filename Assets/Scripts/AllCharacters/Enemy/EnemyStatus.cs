using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] private float atk, def, vit, agi, cri, goldDrop, buffDrop;
    
    private float damage;
    private float maxLife;
    private float life;
    private float defense;
    private float atkSpeed;
    private float critical;

    public SpawnText spawnTextDMG;
    [SerializeField] private GameObject spawnSkill;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float yDropSkill;

    [SerializeField] private bool enemyInTestZone;

    public float Damage { get => damage; }
    public float AtkSpeed { get => atkSpeed;}
    public float Life{ get => life; }
    public bool Critical { get => Random.Range(0, 100) <= critical; }
    public float GoldDrop { get => goldDrop;}


    private void Awake()
    {
        damage = atk * 1;
        maxLife = vit * 1;
        life = maxLife;
        defense = def / 2;
        atkSpeed = 7 - (agi * 0.05f);
        critical = cri; 
    }

    public void LoseLife(float dmg, bool critical)
    {
        float realDMG = dmg - ((dmg * defense) / 100);
        spawnTextDMG.SpawnTextDamage(realDMG, critical);

        if (!enemyInTestZone)
            life -= realDMG;
        
        if (life <= 0 && Random.Range(0, 100) <= buffDrop)
        {
            Vector3 pos = new(spawnPosition.position.x, yDropSkill, 6);
            Instantiate(spawnSkill, pos, Quaternion.Euler(0, 0, 0));
        }
    }

    
}

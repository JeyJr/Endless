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

    public SpawnTextDMG spawnTextDMG;
    [SerializeField] private GameObject spawnSkill;
    [SerializeField] private Transform spawnPosition;

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
        life -= realDMG;

        spawnTextDMG.Spawn(realDMG, critical);

        if(life<= 0 && Random.Range(0, 100) <= buffDrop)
            Instantiate(spawnSkill, spawnPosition.position, Quaternion.Euler(0, 0, 0));
    }

    
}

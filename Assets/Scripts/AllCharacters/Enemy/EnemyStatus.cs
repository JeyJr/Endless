using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] private float atk, def, vit, agi, cri;

    
    private float damage;
    private float maxLife;
    private float life;
    private float defense;
    private float atkSpeed;
    private float critical;
    public SpawnTextDMG spawnTextDMG;


    public float Damage { get => damage; }
    public float AtkSpeed { get => atkSpeed;}
    public bool Critical { get => Random.Range(0, 100) <= critical; }

    public float Life{ get => life; }

    private void Awake()
    {
        damage = atk * 5;
        maxLife = vit * 50;
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
    }

    
}

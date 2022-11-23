using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    [SerializeField] private float maxLife;
    [SerializeField] private float life;
    [SerializeField] private float defense;

    public SpawnTextDMG spawnTextDMG;

    public void LoseLife(float dmg, bool critical)
    {
        float realDMG = dmg - ((dmg * defense) / 100);
        life -= realDMG;

        spawnTextDMG.Spawn(realDMG, critical);
    }
}

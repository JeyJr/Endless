using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private float maxLife;
    [SerializeField] private float life;
    [SerializeField] private float defense;


    public SpawnTextDMG spawnTextDMG;

    private void Start()
    {
        GameData gameData = ManagerData.Load();

        maxLife = gameData.MaxLife;
        life = maxLife;
        defense = gameData.Defense;
    }

    public void LoseLife(float dmg, bool critical)
    {
        float realDMG = dmg - ((dmg * defense) / 100);
        life -= realDMG;

        spawnTextDMG.Spawn(realDMG, critical);
    }
}

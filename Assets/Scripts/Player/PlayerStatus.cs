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
        maxLife = GameData.GetMaxLife();
        life = maxLife;
        defense = GameData.GetDefense();
    }

    private void Update()
    {
        maxLife = GameData.GetMaxLife();
        defense = GameData.GetDefense();
    }


    public void LoseLife(float dmg, bool critical)
    {
        float realDMG = dmg - ((dmg * defense) / 100);
        life -= realDMG;

        spawnTextDMG.Spawn(realDMG, critical);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private float maxLife;
    [SerializeField] private float life;
    [SerializeField] private float defense;
    [SerializeField] private float dmg;
    [SerializeField] private float cri;
    [SerializeField] private float range;

    public SpawnTextDMG spawnTextDMG;
    public Slider lifeBar;

    public float Life { get => life; set
        {
            if (life < maxLife)
                life += value;
            else
                life = maxLife;
        }
    }

    public float MaxLife { get => maxLife; }

    private void Start()
    {
        GameData gameData = ManagerData.Load();

        maxLife = gameData.MaxLife;
        life = maxLife;
        defense = gameData.Defense;
        dmg = gameData.Damage;
        cri = gameData.CriticalDMG;
        range = gameData.RangeAtk;

        UpdateLifeBar();
    }

    public void LoseLife(float dmg, bool critical)
    {
        float realDMG;

        if (critical)
            realDMG = (dmg - ((dmg * defense) / 100)) * 2;
        else
            realDMG = dmg - ((dmg * defense) / 100);
            
        life -= realDMG;
        UpdateLifeBar();
        spawnTextDMG.Spawn(realDMG, critical);
    }

    public void UpdateLifeBar()
    {
        lifeBar.maxValue = maxLife;
        lifeBar.value = life;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private float maxLife;
    [SerializeField] private float life;


    public SpawnText spawnText;
    public Slider lifeBar;
    public float MaxLife { get => maxLife; }
    
    private void Awake()
    {
        GameData gameData = ManagerData.Load();
        maxLife = gameData.MaxLife;
        life = maxLife;

        UpdateLifeBar();
    }

    public float Life { get => life; set
        {
            if (life < maxLife)
                life += value;
            else
                life = maxLife;

            spawnText.SpawnTextSkill(value, "Life+ ");
        }
    }



    public void LoseLife(float dmg, float critical)
    {
        float realDMG;
        bool criticalDMG =  Random.Range(0, 100) <= critical;
        realDMG = (dmg - ((dmg * ManagerData.Load().Defense) / 100));
       
        
        Debug.Log("Critical dmg: " + criticalDMG);

        if (criticalDMG)
            realDMG += realDMG * critical / 100; 

        life -= realDMG;
        UpdateLifeBar();
        spawnText.SpawnTextDamage(realDMG, criticalDMG);
    }

    public void UpdateLifeBar()
    {
        lifeBar.maxValue = maxLife;
        lifeBar.value = life;
    }
}

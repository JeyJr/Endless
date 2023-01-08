using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        gameData.buffSkillPowerUp = 0;
        gameData.buffSkillDefense = 0;
        gameData.buffSkillMaxLife = 0;
        gameData.buffSkillAtkSpeed = 0;
        gameData.buffSkillCritical = 0;
        gameData.buffSkillRangeAtk = 0;
        gameData.buffSkillMoveSpeed = 0;

        ManagerData.Save(gameData);
    }

    public float Life { get => life; set
        {
            if (life < maxLife)
                life += value;
            else
                life = maxLife;

            spawnText.SpawnTextSkill(value, "Life+ ");
            UpdateLifeBar();
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

using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] private float maxLife;
    [SerializeField] private float life;

    public SpawnText spawnText;
    public Slider lifeBar;

    [Header("RIP")]
    [SerializeField] private GameObject ripStone;
    public bool ImAlive { get; set; }



    private void Start()
    {
        GameData gameData = ManagerData.Load();
        maxLife = gameData.MaxLife;
        life = maxLife;
        ImAlive = true;

        lifeBar = GameObject.FindGameObjectWithTag("MainUI").
            transform.Find("panelLifeBar").
            transform.Find("sliderLifeBar").
            GetComponent<Slider>();

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

            spawnText.SpawnTextSkill(value, "+ ");
            UpdateLifeBar();
        }
    }

    public void LoseLife(float dmg, float critical)
    {
        float realDMG;
        bool criticalDMG = Random.Range(0, 100) <= critical;
        realDMG = (dmg - ((dmg * ManagerData.Load().Defense) / 100));
        realDMG = realDMG < 1 ? 1 : realDMG;

        if (criticalDMG)
            realDMG += realDMG * critical / 100;

        life -= realDMG;
        UpdateLifeBar();
        spawnText.SpawnTextDamage(realDMG, criticalDMG);
    }

    private void Update()
    {
        if (life <= 0 && ImAlive)
        {
            ImAlive = false;
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 100, -2);
        Instantiate(ripStone, pos, Quaternion.Euler(0, 0, 0));
        yield return new WaitForSeconds(3);
        GameObject.FindWithTag("MainUI").GetComponent<LevelCanvas>().OpenPanelGameOver();
    }

    public void UpdateLifeBar()
    {
        lifeBar.maxValue = maxLife;
        lifeBar.value = life;
    }


}

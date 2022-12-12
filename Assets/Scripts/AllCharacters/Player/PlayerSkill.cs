using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] private GameObject txtSkill;
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private LayerMask target;


    [Header("Life Recovery")]
    [SerializeField] private float lifeRecoveredTimesCounter;
    [SerializeField] private float lifeRecoveryValue;
    [SerializeField] private float delayTimeToRecovery;

    [Header("Move Speed")]
    [SerializeField] private float moveSpeedBuffValue;
    [SerializeField] private float delayTimeToEndMoveSpeedBuff; 
    
    [Header("Damage")]
    [SerializeField] private float damageBuffValue;
    [SerializeField] private float delayTimeToEndDamageBuff;

    [Header("Atk Speed")]
    [SerializeField] private float atkSpeedBuffValue;
    [SerializeField] private float delayTimeToEndatkSpeedBuff;
    [SerializeField] private PlayerHand playerHand;

    [Header("Range Atk")]
    [SerializeField] private float rangeAtkBuffValue;
    [SerializeField] private float delayTimeToRangeAtkBuff;


    [Header("Ring of Fire")]
    [SerializeField] private float ringOfFireTimesCounter;
    [SerializeField] private float ringOfFirePercentDmg;
    [SerializeField] private float delayTimeToRingOfFireBuff;
    [SerializeField] private GameObject ringOfFire;
    
    [Header("WindBlade")]
    [SerializeField] private float windBladeTimesCounter;
    [SerializeField] private float windBladePercentDmg;
    [SerializeField] private float delayTimeToWindBladeBuff;
    [SerializeField] private GameObject windBlade;




    private void Awake()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillAtkSpeed = 0;
        gameData.buffSkillDamage = 0;
        gameData.buffSkillRangeAtk = 0;

        ManagerData.Save(gameData);
    }

    #region LifeRecovery
    public void SkillLifeRecovery()
    {
        StartCoroutine(LifeRecovery());
    }

    IEnumerator LifeRecovery()
    {
        float bonusValue = playerStatus.MaxLife * lifeRecoveryValue / 100;
        for (int i = 0; i < lifeRecoveredTimesCounter; i++)
        {
            playerStatus.Life = (bonusValue);

            txtSkill.GetComponent<TextMeshPro>().text = "Life +" + bonusValue.ToString("F0");
            Instantiate(txtSkill, spawnPosition.position, Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(delayTimeToRecovery);
        }
    }
    #endregion

    #region MoveSpeed
    public void SkillMoveSpeed()
    {
        StartCoroutine(MoveSpeed());
    }

    IEnumerator MoveSpeed()
    {
        float actualSpeed = playerController.PlayerMoveSpeed;
        playerController.PlayerMoveSpeed += moveSpeedBuffValue;
        yield return new WaitForSeconds(delayTimeToEndMoveSpeedBuff);
        playerController.PlayerMoveSpeed = actualSpeed;
    }
    #endregion

    #region PowerUp
    public void SkillPowerUp()
    {
        StartCoroutine(PowerUp());
       }

    IEnumerator PowerUp()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillDamage = damageBuffValue;
        ManagerData.Save(gameData);

        yield return new WaitForSeconds(delayTimeToEndDamageBuff);
        
        gameData.buffSkillDamage = 0;
        ManagerData.Save(gameData);

    }
    #endregion

    #region AtkSpeed
    public void SkillAtkSpeed()
    {
        StartCoroutine(AtkSpeed());
    }

    IEnumerator AtkSpeed()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillAtkSpeed = atkSpeedBuffValue;
        ManagerData.Save(gameData);

        playerHand.StopDelay();

        yield return new WaitForSeconds(delayTimeToEndatkSpeedBuff);

        gameData.buffSkillAtkSpeed = 0;
        ManagerData.Save(gameData);
    }
    #endregion

    #region RangeAtk
    public void SkillRangeAtk()
    {
        StartCoroutine(RangeAtk());
    }

    IEnumerator RangeAtk()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillRangeAtk = rangeAtkBuffValue;
        ManagerData.Save(gameData);

        yield return new WaitForSeconds(delayTimeToRangeAtkBuff);

        gameData.buffSkillRangeAtk = 0;
        ManagerData.Save(gameData);
    }
    #endregion

    #region Ring Of Fire
    public void SkillRingOfFire()
    {
        StartCoroutine(RingOfFire());
    }

    IEnumerator RingOfFire()
    {
        GameData gameData = ManagerData.Load();

        for (int i = 0; i < ringOfFireTimesCounter; i++)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, 1);
            ringOfFire.GetComponent<RingOfFire>().target = target;
            ringOfFire.GetComponent<RingOfFire>().damage = gameData.weaponDmg * ringOfFirePercentDmg / 100;
            Instantiate(ringOfFire, pos, Quaternion.Euler(0,0,0));
            yield return new WaitForSeconds(delayTimeToRingOfFireBuff);
        }
    }
    #endregion

    #region Wind Blade
    public void SkillWindBlade()
    {
        StartCoroutine(WindBlade());
    }

    IEnumerator WindBlade()
    {
        GameData gameData = ManagerData.Load();

        for (int i = 0; i < windBladeTimesCounter; i++)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, 1);
            windBlade.GetComponent<WindBlade>().target = target;
            windBlade.GetComponent<WindBlade>().damage = gameData.weaponDmg * windBladePercentDmg / 100;
            windBlade.GetComponent<WindBlade>().mirrored = transform.rotation.y == 0 ? false : true; 
            Instantiate(windBlade, pos, Quaternion.identity);
            yield return new WaitForSeconds(delayTimeToWindBladeBuff);
        }
    }
    #endregion
}

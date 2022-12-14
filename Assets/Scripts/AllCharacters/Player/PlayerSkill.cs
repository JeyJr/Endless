using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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
    [SerializeField] private string skillNameLifeRecovery;
    [SerializeField] private bool lifeRecoveryIsActive;
    [SerializeField] private float lifeRecoveredTimesCounter;
    [SerializeField] private float lifeRecoveryValue;
    [SerializeField] private float delayTimeToRecovery;

    [Header("Move Speed")]
    [SerializeField] private string skillNameMoveSpeed;
    [SerializeField] private bool moveSpeedIsActive;
    [SerializeField] private float moveSpeedBuffValue;
    [SerializeField] private float delayTimeToEndMoveSpeedBuff; 
    
    [Header("PowerUp")]
    [SerializeField] private string skillNamePowerUp;
    [SerializeField] private bool powerUpIsActive;
    [SerializeField] private float powerUpBuffValue;
    [SerializeField] private float delayTimeToEndPowerUpBuff;

    [Header("Atk Speed")]
    [SerializeField] private string skillNameAtkSpeed;
    [SerializeField] private bool atkSpeedIsActive;
    [SerializeField] private float atkSpeedBuffValue;
    [SerializeField] private float delayTimeToEndatkSpeedBuff;
    [SerializeField] private PlayerHand playerHand;

    [Header("Range Atk")]
    [SerializeField] private string skillNameRangeAtk;
    [SerializeField] private bool rangeAtkIsActive;
    [SerializeField] private float rangeAtkBuffValue;
    [SerializeField] private float delayTimeToRangeAtkBuff;


    [Header("Ring of Fire")]
    [SerializeField] private string skillNameRingOfFire;
    [SerializeField] private bool ringOfFireIsActive;
    [SerializeField] private float ringOfFireTimesCounter;
    [SerializeField] private float ringOfFirePercentDmg;
    [SerializeField] private float delayTimeToRingOfFireBuff;
    [SerializeField] private GameObject ringOfFire;
    
    [Header("WindBlade")]
    [SerializeField] private string skillNameWindBlade;
    [SerializeField] private bool windBladeIsActive;
    [SerializeField] private float windBladeTimesCounter;
    [SerializeField] private float windBladePercentDmg;
    [SerializeField] private float delayTimeToWindBladeBuff;
    [SerializeField] private GameObject windBlade;


    [Header("UI ICON SKill")]
    [SerializeField] private List<Sprite> skillIcon;
    private LevelController levelController;

    private void Awake()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillAtkSpeed = 0;
        gameData.buffSkillPowerUp = 0;
        gameData.buffSkillRangeAtk = 0;

        ManagerData.Save(gameData);

        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
    }

    #region LifeRecovery
    public bool GetLifeRecoveryActive() => lifeRecoveryIsActive;
    public void ActiveSkillLifeRecovery() => StartCoroutine(LifeRecovery());

    IEnumerator LifeRecovery()
    {
        lifeRecoveryIsActive = true;
        levelController.EnableUISkillSlot(skillIcon[0], skillNameLifeRecovery);


        float bonusValue = playerStatus.MaxLife * lifeRecoveryValue / 100;
        for (int i = 0; i < lifeRecoveredTimesCounter; i++)
        {
            playerStatus.Life = (bonusValue);

            txtSkill.GetComponent<TextMeshPro>().text = "Life +" + bonusValue.ToString("F0");
            Instantiate(txtSkill, spawnPosition.position, Quaternion.Euler(0, 0, 0));
            playerStatus.UpdateLifeBar();
            yield return new WaitForSeconds(delayTimeToRecovery);
        }

        SetUISkillIConValue(skillNameLifeRecovery);

        lifeRecoveryIsActive = false;
    }
    #endregion

    #region MoveSpeed
    public bool GetMoveSpeedActive() => moveSpeedIsActive;
    public void ActiveSkillMoveSpeed() => StartCoroutine(ActiveMoveSpeed());


    IEnumerator ActiveMoveSpeed()
    {
        moveSpeedIsActive = true;
        levelController.EnableUISkillSlot(skillIcon[1], skillNameMoveSpeed);

        float actualSpeed = playerController.PlayerMoveSpeed;
        playerController.PlayerMoveSpeed = moveSpeedBuffValue;
        yield return new WaitForSeconds(delayTimeToEndMoveSpeedBuff);
        playerController.PlayerMoveSpeed = actualSpeed;

        SetUISkillIConValue(skillNameMoveSpeed);
        moveSpeedIsActive = false;
    }
    #endregion

    #region PowerUp
    public bool GetPowerUpActive() => powerUpIsActive;
    public void ActiveSkillPowerUp() => StartCoroutine(ActivePowerUp());



    IEnumerator ActivePowerUp()
    {
        powerUpIsActive = true;
        levelController.EnableUISkillSlot(skillIcon[2], skillNamePowerUp);

        GameData gameData = ManagerData.Load();
        gameData.buffSkillPowerUp = powerUpBuffValue;
        ManagerData.Save(gameData);

        yield return new WaitForSeconds(delayTimeToEndPowerUpBuff);
        
        gameData.buffSkillPowerUp = 0;
        ManagerData.Save(gameData);

        powerUpIsActive = false;

    }
    #endregion

    #region AtkSpeed
    public bool GetAtkSpeedActive() => atkSpeedIsActive;
    public void ActiveSkillAtkSpeed() => StartCoroutine(ActiveAtkSpeed());


    IEnumerator ActiveAtkSpeed()
    {
        atkSpeedIsActive = true;
        levelController.EnableUISkillSlot(skillIcon[3], skillNameAtkSpeed);


        GameData gameData = ManagerData.Load();
        gameData.buffSkillAtkSpeed = atkSpeedBuffValue;
        ManagerData.Save(gameData);

        playerHand.StopDelay();

        yield return new WaitForSeconds(delayTimeToEndatkSpeedBuff);

        gameData.buffSkillAtkSpeed = 0;
        ManagerData.Save(gameData);

        atkSpeedIsActive = false;
    }
    #endregion

    #region RangeAtk
    public bool GetRangeAtkActive() => rangeAtkIsActive;
    public void ActiveSkillRangeAtk() => StartCoroutine(ActiveRangeAtk());


    IEnumerator ActiveRangeAtk()
    {
        rangeAtkIsActive = true;
        levelController.EnableUISkillSlot(skillIcon[4], skillNameRangeAtk);

        GameData gameData = ManagerData.Load();
        gameData.buffSkillRangeAtk = rangeAtkBuffValue;
        ManagerData.Save(gameData);

        yield return new WaitForSeconds(delayTimeToRangeAtkBuff);

        gameData.buffSkillRangeAtk = 0;
        ManagerData.Save(gameData);

        rangeAtkIsActive = false;
    }
    #endregion

    #region Ring Of Fire
    public bool GetRingOfFireActive() => ringOfFireIsActive;
    public void ActiveSkillRingOfFire() => StartCoroutine(ActiveRingOfFire());

    IEnumerator ActiveRingOfFire()
    {
        ringOfFireIsActive = true;
        levelController.EnableUISkillSlot(skillIcon[5], skillNameRingOfFire);

        GameData gameData = ManagerData.Load();

        for (int i = 0; i < ringOfFireTimesCounter; i++)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, 4);
            ringOfFire.GetComponent<RingOfFire>().target = target;
            ringOfFire.GetComponent<RingOfFire>().damage = gameData.weaponDmg * ringOfFirePercentDmg / 100;
            Instantiate(ringOfFire, pos, Quaternion.Euler(0,0,0));
            yield return new WaitForSeconds(delayTimeToRingOfFireBuff);
        }
        ringOfFireIsActive = false;
    }
    #endregion

    #region Wind Blade
    public bool GetWindBladeActive() => windBladeIsActive;
    public void ActiveSkillWindBlade() => StartCoroutine(ActiveWindBlade());


    IEnumerator ActiveWindBlade()
    {
        windBladeIsActive = true;
        levelController.EnableUISkillSlot(skillIcon[6], skillNameWindBlade);

        GameData gameData = ManagerData.Load();

        for (int i = 0; i < windBladeTimesCounter; i++)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, 4);
            windBlade.GetComponent<WindBlade>().target = target;
            windBlade.GetComponent<WindBlade>().damage = gameData.weaponDmg * windBladePercentDmg / 100;
            windBlade.GetComponent<WindBlade>().mirrored = transform.rotation.y == 0 ? false : true; 
            Instantiate(windBlade, pos, Quaternion.identity);
            yield return new WaitForSeconds(delayTimeToWindBladeBuff);
        }

        windBladeIsActive = false;
    }
    #endregion

    void SetUISkillIConValue(string name)
    {
        for (int i = 0; i < levelController.uiSlotsIconSkills.Count; i++)
        {
            if (levelController.uiSlotsIconSkills[i].sprite.name == name)
            {
                levelController.uiSlotsIconSkills[i].sprite = null;
                levelController.uiSlotsIconSkills[i].enabled = false;
                break;
            }
        }
    }
}

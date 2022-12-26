using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] private Sprite lifeRecoveryIcon;

    [Header("Move Speed")]
    [SerializeField] private string skillNameMoveSpeed;
    [SerializeField] private bool moveSpeedIsActive;
    [SerializeField] private float moveSpeedBuffValue;
    [SerializeField] private float delayTimeToEndMoveSpeedBuff;
    [SerializeField] private Sprite moveSpeedIcon;

    [Header("PowerUp")]
    [SerializeField] private string skillNamePowerUp;
    [SerializeField] private bool powerUpIsActive;
    [SerializeField] private float powerUpBuffValue;
    [SerializeField] private float delayTimeToEndPowerUpBuff;
    [SerializeField] private Sprite powerUpIcon;

    [Header("Atk Speed")]
    [SerializeField] private string skillNameAtkSpeed;
    [SerializeField] private bool atkSpeedIsActive;
    [SerializeField] private float atkSpeedBuffValue;
    [SerializeField] private float delayTimeToEndatkSpeedBuff;
    [SerializeField] private PlayerHand playerHand;
    [SerializeField] private Sprite atkSpeedIcon;


    [Header("Range Atk")]
    [SerializeField] private string skillNameRangeAtk;
    [SerializeField] private bool rangeAtkIsActive;
    [SerializeField] private float rangeAtkBuffValue;
    [SerializeField] private float delayTimeToRangeAtkBuff;
    [SerializeField] private Sprite rangeAtkIcon;


    [Header("Ring of Fire")]
    [SerializeField] private string skillNameRingOfFire;
    [SerializeField] private bool ringOfFireIsActive;
    [SerializeField] private float ringOfFireTimesCounter;
    [SerializeField] private float ringOfFirePercentDmg;
    [SerializeField] private float delayTimeToRingOfFireBuff;
    [SerializeField] private Sprite ringOfFireIcon;
    [SerializeField] private GameObject ringOfFire;
    
    [Header("WindBlade")]
    [SerializeField] private string skillNameWindBlade;
    [SerializeField] private bool windBladeIsActive;
    [SerializeField] private float windBladeTimesCounter;
    [SerializeField] private float windBladePercentDmg;
    [SerializeField] private float delayTimeToWindBladeBuff;
    [SerializeField] private Sprite windBladeIcon;
    [SerializeField] private GameObject windBlade;

    public bool LifeRecoveryIsActive { get => lifeRecoveryIsActive; private set => lifeRecoveryIsActive = value; }
    public bool MoveSpeedIsActive { get => moveSpeedIsActive; private set => moveSpeedIsActive = value; }
    public bool PowerUpIsActive { get => powerUpIsActive; private set => powerUpIsActive = value; }
    public bool AtkSpeedIsActive { get => atkSpeedIsActive; private set=> atkSpeedIsActive = value; }
    public bool RangeAtkIsActive { get => rangeAtkIsActive; private set => rangeAtkIsActive = value; }
    public bool RingOfFireIsActive { get => ringOfFireIsActive; private set => ringOfFireIsActive = value; }
    public bool WindBladeIsActive { get => windBladeIsActive; private set => windBladeIsActive = value; }


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

        if(GameObject.FindGameObjectWithTag("LevelController") != null)
            levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
    }

    #region LifeRecovery
    public void ActiveSkillLifeRecovery() {
        lifeRecoveryIsActive = true;
        lifeRecoveryIcon.name = skillNameLifeRecovery;
        levelController.EnableUISkillSlot(lifeRecoveryIcon);

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
            playerStatus.UpdateLifeBar();
            yield return new WaitForSeconds(delayTimeToRecovery);
        }
        levelController.DisableUISkillICon(skillNameLifeRecovery);
        lifeRecoveryIsActive = false;
    }
    #endregion

    #region MoveSpeed
    public bool GetMoveSpeedActive() => moveSpeedIsActive;
    public void ActiveSkillMoveSpeed() {
        moveSpeedIsActive = true;
        moveSpeedIcon.name = skillNameMoveSpeed;
        levelController.EnableUISkillSlot(moveSpeedIcon);

        StartCoroutine(ActiveMoveSpeed());
    }


    IEnumerator ActiveMoveSpeed()
    {
        playerController.PlayerMoveSpeed = ManagerData.Load().MoveSpeed + moveSpeedBuffValue;
        yield return new WaitForSeconds(delayTimeToEndMoveSpeedBuff);
        playerController.PlayerMoveSpeed = ManagerData.Load().MoveSpeed;

        levelController.DisableUISkillICon(skillNameMoveSpeed);
        moveSpeedIsActive = false;
    }
    #endregion

    #region PowerUp
    public void ActiveSkillPowerUp() {
        powerUpIsActive = true;
        powerUpIcon.name = skillNamePowerUp;
        levelController.EnableUISkillSlot(powerUpIcon);

        StartCoroutine(ActivePowerUp());
    }



    IEnumerator ActivePowerUp()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillPowerUp = gameData.weaponDmg * powerUpBuffValue /100;
        ManagerData.Save(gameData);

        yield return new WaitForSeconds(delayTimeToEndPowerUpBuff);

        gameData.buffSkillPowerUp = 0;
        ManagerData.Save(gameData);

        levelController.DisableUISkillICon(skillNamePowerUp);
        powerUpIsActive = false;

    }
    #endregion

    #region AtkSpeed

    public void ActiveSkillAtkSpeed() {
        atkSpeedIsActive = true;
        atkSpeedIcon.name = skillNameAtkSpeed;
        levelController.EnableUISkillSlot(atkSpeedIcon);

        StartCoroutine(ActiveAtkSpeed());
    } 


    IEnumerator ActiveAtkSpeed()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillAtkSpeed = atkSpeedBuffValue;
        ManagerData.Save(gameData);

        playerHand.StopDelay();

        yield return new WaitForSeconds(delayTimeToEndatkSpeedBuff);

        gameData.buffSkillAtkSpeed = 0;
        ManagerData.Save(gameData);

        levelController.DisableUISkillICon(skillNameAtkSpeed);
        atkSpeedIsActive = false;
    }
    #endregion

    #region RangeAtk
    public void ActiveSkillRangeAtk() {
        rangeAtkIsActive = true;
        rangeAtkIcon.name = skillNameRangeAtk;
        levelController.EnableUISkillSlot(rangeAtkIcon);

        StartCoroutine(ActiveRangeAtk());
    }


    IEnumerator ActiveRangeAtk()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillRangeAtk = rangeAtkBuffValue;
        ManagerData.Save(gameData);

        yield return new WaitForSeconds(delayTimeToRangeAtkBuff);

        gameData.buffSkillRangeAtk = 0;
        ManagerData.Save(gameData);

        levelController.DisableUISkillICon(skillNameRangeAtk);
        rangeAtkIsActive = false;
    }
    #endregion

    #region Ring Of Fire
    public void ActiveSkillRingOfFire() 
    {
        RingOfFireIsActive = true;
        ringOfFireIcon.name = skillNameRingOfFire;
        levelController.EnableUISkillSlot(ringOfFireIcon);

        StartCoroutine(ActiveRingOfFire());
    }

    IEnumerator ActiveRingOfFire()
    {
        GameData gameData = ManagerData.Load();

        for (int i = 0; i < ringOfFireTimesCounter; i++)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y, 4);
            ringOfFire.GetComponent<RingOfFire>().target = target;
            ringOfFire.GetComponent<RingOfFire>().damage = gameData.weaponDmg * ringOfFirePercentDmg / 100;
            Instantiate(ringOfFire, pos, Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(delayTimeToRingOfFireBuff);
        }
        levelController.DisableUISkillICon(skillNameRingOfFire);
        RingOfFireIsActive = false;
    }
    #endregion

    #region Wind Blade
    public void ActiveSkillWindBlade() 
    {
        WindBladeIsActive = true;
        windBladeIcon.name = skillNameWindBlade;
        levelController.EnableUISkillSlot(windBladeIcon);

        StartCoroutine(ActiveWindBlade());
    }

    IEnumerator ActiveWindBlade()
    {
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

        levelController.DisableUISkillICon(skillNameWindBlade);
        WindBladeIsActive = false;
    }
    #endregion

}

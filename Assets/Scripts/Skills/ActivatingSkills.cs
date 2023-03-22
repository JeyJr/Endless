using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ActivatingSkills : MonoBehaviour
{
    [Header("Skill attributes")]
    [SerializeField] private int id;
    int delayTimeInt;
    [SerializeField] private float delayTime;
    [SerializeField] private float buffValue;
    [SerializeField] private float xTimes;
    LevelCanvas levelCanvas;
    LevelCanvasUISkillICon uiSkillIcon;

    [Header("Skills Behavior")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private LayerMask target;
    [SerializeField] private GameObject ringOfFire, windBlade;

    SFXControl sfxControl;


    private void OnEnable()
    {
        sfxControl = GameObject.FindWithTag("SFX").GetComponent<SFXControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            levelCanvas = GameObject.FindGameObjectWithTag("MainUI").GetComponent<LevelCanvas>();
            uiSkillIcon = GameObject.FindGameObjectWithTag("MainUI").GetComponent<LevelCanvasUISkillICon>();

            playerController = other.GetComponent<PlayerController>();
            playerStatus = other.GetComponentInChildren<PlayerStatus>();

            Sprite sprite = gameObject.GetComponent<SpriteRenderer>().sprite;

            if (!uiSkillIcon.CheckIcon(sprite))
            {
                uiSkillIcon.AddIcon(sprite);
                gameObject.GetComponent<Transform>().position = new Vector3(-100, -100, -100);
                StartSkill(delayTime, buffValue, xTimes, id);
            }
        }
    }

    public void StartSkill(float delayTime, float buffValue, float xTimes, int id)
    {
        sfxControl.PlayClip(SFXClip.getSkill);

        this.delayTime = delayTime;
        this.buffValue = buffValue;
        this.xTimes = xTimes;
        this.id = id;

        delayTimeInt = (int)delayTime * 1000;

        switch (id)
        {
            case 1000:
                StartCoroutine(TaskPowerUp());
                break;
            case 1001:
                StartCoroutine(TaskDefense());
                break;
            case 1002:
                StartCoroutine(TaskLifeRecovery());
                break;
            case 1003:
                StartCoroutine(TaskAtkSpeed());
                break;
            case 1004:
                StartCoroutine(TaskCritical());
                break;
            case 1005:
                StartCoroutine(TaskRangeAtk());
                break;
            case 1006:
                StartCoroutine(TaskMoveSpeed());
                break;
            case 1007:
                StartCoroutine(TaskActiveRingOfFire());
                break;
            case 1008:
                StartCoroutine(TaskActiveWindBlade());
                break;
        }
    }

    IEnumerator TaskPowerUp()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillPowerUp = buffValue;
        ManagerData.Save(gameData);

        levelCanvas.TextLevelInfo($"Buff Power\n" + "<color=#ff9f63> +" + buffValue.ToString("F2") + "%</color>");

        yield return new WaitForSeconds(delayTimeInt);

        gameData.buffSkillPowerUp = 0;
        ManagerData.Save(gameData);

        DisableUIIcon();
    }
    IEnumerator TaskDefense()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillDefense = buffValue;
        ManagerData.Save(gameData);

        levelCanvas.TextLevelInfo($"Buff Defense\n" + "<color=#63efff> +" + buffValue.ToString("F2") + "%</color>");
        yield return new WaitForSeconds(delayTimeInt);

        gameData.buffSkillDefense = 0;
        ManagerData.Save(gameData);

        try
        {
            DisableUIIcon();
        }
        catch (Exception)
        {
            throw;
        }
    }

    IEnumerator TaskLifeRecovery()
    {
        levelCanvas.TextLevelInfo($"Skill:\n" + "<color=#92ff63> Life Recovery </color>");
        for (int i = 0; i < xTimes; i++)
        {
            playerStatus.Life = buffValue;
            yield return new WaitForSeconds(delayTimeInt);
        }

        try
        {
            DisableUIIcon();
        }
        catch (Exception)
        {
            throw;
        }
    }
    IEnumerator TaskAtkSpeed()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillAtkSpeed = buffValue;
        ManagerData.Save(gameData);
        levelCanvas.TextLevelInfo($"Buff AtkSpeed\n" + "<color=#ff6363> +" + buffValue.ToString("F2") + "%</color>");

        yield return new WaitForSeconds(delayTimeInt);

        gameData.buffSkillAtkSpeed = 0;
        ManagerData.Save(gameData);

        try
        {
            DisableUIIcon();
        }
        catch (Exception)
        {
            throw;
        }
    }
    IEnumerator TaskCritical()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillCritical = buffValue;
        ManagerData.Save(gameData);

        levelCanvas.TextLevelInfo($"Buff Critical\n" + "<color=#ff6363> +" + buffValue.ToString("F2") + "%</color>");

        yield return new WaitForSeconds(delayTimeInt);

        gameData.buffSkillCritical = 0;
        ManagerData.Save(gameData);

        try
        {
            DisableUIIcon();
        }
        catch (Exception)
        {
            throw;
        }
    }
    IEnumerator TaskRangeAtk()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillRangeAtk = buffValue;
        ManagerData.Save(gameData);

        levelCanvas.TextLevelInfo($"Buff attack Range\n" + "<color=#f263ff> +" + buffValue.ToString("F2") + "%</color>");

        yield return new WaitForSeconds(delayTimeInt);

        gameData.buffSkillRangeAtk = 0;
        ManagerData.Save(gameData);

        try
        {
            DisableUIIcon();
        }
        catch (Exception)
        {
            throw;
        }
    }
    IEnumerator TaskMoveSpeed()
    {
        playerController.UpdatePlayerMoveSpeed(buffValue);
        levelCanvas.TextLevelInfo($"Buff movement speed\n" + "<color=#b463ff> +" + buffValue.ToString("F2") + "%</color>");

        yield return new WaitForSeconds(delayTimeInt);
        playerController.UpdatePlayerMoveSpeed(0);

        DisableUIIcon();
    }
    IEnumerator TaskActiveRingOfFire()
    {
        GameData gameData = ManagerData.Load();

        levelCanvas.TextLevelInfo($"Skill:\nFire Ring");

        for (int i = 0; i < xTimes; i++)
        {
            Vector3 pos = new(playerController.GetComponent<Transform>().position.x, playerController.GetComponent<Transform>().position.y + 1.5f, -11);
            ringOfFire.GetComponent<RingOfFire>().target = target;
            ringOfFire.GetComponent<RingOfFire>().damage = gameData.weaponDmg * buffValue / 100;
            Instantiate(ringOfFire, pos, Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(delayTimeInt);
        }

        DisableUIIcon();
    }

    IEnumerator TaskActiveWindBlade()
    {
        GameData gameData = ManagerData.Load();

        levelCanvas.TextLevelInfo($"Skill: \nBlades Of Wind");
        for (int i = 0; i < xTimes; i++)
        {
            Vector3 pos = new(playerController.GetComponent<Transform>().position.x, playerController.GetComponent<Transform>().position.y + 1.5f, -11);

            windBlade.GetComponent<WindBlade>().target = target;
            windBlade.GetComponent<WindBlade>().damage = gameData.weaponDmg * buffValue / 100;
            windBlade.GetComponent<WindBlade>().mirrored = playerController.GetComponent<Transform>().rotation.y == 0 ? false : true;
            Instantiate(windBlade, pos, Quaternion.identity);
            yield return new WaitForSeconds(delayTimeInt);

        }

        DisableUIIcon();
    }
    void DisableUIIcon()
    {
        uiSkillIcon.RemoveIcon(gameObject.GetComponent<SpriteRenderer>().sprite);
        Destroy(this.gameObject);
    }
}

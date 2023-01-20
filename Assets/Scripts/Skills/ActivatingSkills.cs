using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
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
    Task task;
    [SerializeField] private LayerMask target;
    [SerializeField] private GameObject ringOfFire, windBlade;

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

    public async void StartSkill(float delayTime, float buffValue, float xTimes, int id)
    {
        this.delayTime = delayTime;
        this.buffValue = buffValue;
        this.xTimes = xTimes;
        this.id = id;

        delayTimeInt = (int)delayTime * 1000;

        switch (id)
        {
            case 1000:
                task = TaskPowerUp();
                break;
            case 1001:
                task = TaskDefense();
                break;
            case 1002:
                task = TaskLifeRecovery();
                break;
            case 1003:
                task = TaskAtkSpeed();
                break;
            case 1004:
                task = TaskCritical();
                break;
            case 1005:
                task = TaskRangeAtk();
                break;
            case 1006:
                task = TaskMoveSpeed();
                break;
            case 1007:
                task = TaskActiveRingOfFire();
                break;
            case 1008:
                task = TaskActiveWindBlade();
                break;
        }
        await task;
    }

    async Task TaskPowerUp()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillPowerUp = buffValue;
        ManagerData.Save(gameData);

        levelCanvas.TextLevelInfo($"Buff Power\n" + "<color=#ff9f63> +" + buffValue.ToString("F2") + "%</color>");
        await Task.Delay(delayTimeInt);
        //def 
        //lif 
        //sped 
        //cri 
        //rang 
        //mvoe 
        gameData.buffSkillPowerUp = 0;
        ManagerData.Save(gameData);

        DisableUIIcon();
    }
    async Task TaskDefense()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillDefense = buffValue;
        ManagerData.Save(gameData);

        levelCanvas.TextLevelInfo($"Buff Defense\n" + "<color=#63efff> +" + buffValue.ToString("F2") + "%</color>");
        await Task.Delay(delayTimeInt);

        gameData.buffSkillDefense = 0;
        ManagerData.Save(gameData);

        DisableUIIcon();
    }
    async Task TaskLifeRecovery()
    {
        levelCanvas.TextLevelInfo($"Skill:\n" + "<color=#92ff63> Life Recovery </color>");
        for (int i = 0; i < xTimes; i++)
        {
            playerStatus.Life = buffValue;
            await Task.Delay(delayTimeInt);
        }
        DisableUIIcon();
    }
    async Task TaskAtkSpeed()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillAtkSpeed = buffValue;
        ManagerData.Save(gameData);
        levelCanvas.TextLevelInfo($"Buff AtkSpeed\n" + "<color=#ff6363> +" + buffValue.ToString("F2") + "%</color>");

        await Task.Delay(delayTimeInt);

        gameData.buffSkillAtkSpeed = 0;
        ManagerData.Save(gameData);

        DisableUIIcon();
    }
    async Task TaskCritical()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillCritical = buffValue;
        ManagerData.Save(gameData);

        levelCanvas.TextLevelInfo($"Buff Critical\n" + "<color=#ff6363> +" + buffValue.ToString("F2") + "%</color>");

        await Task.Delay(delayTimeInt);

        gameData.buffSkillCritical = 0;
        ManagerData.Save(gameData);

        DisableUIIcon();
    }
    async Task TaskRangeAtk()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillRangeAtk = buffValue;
        ManagerData.Save(gameData);

        levelCanvas.TextLevelInfo($"Buff attack Range\n" + "<color=#f263ff> +" + buffValue.ToString("F2") + "%</color>");

        await Task.Delay(delayTimeInt);

        gameData.buffSkillRangeAtk = 0;
        ManagerData.Save(gameData);

        DisableUIIcon();
    }
    async Task TaskMoveSpeed()
    {
        playerController.UpdatePlayerMoveSpeed(buffValue);
        levelCanvas.TextLevelInfo($"Buff movement speed\n" + "<color=#b463ff> +" + buffValue.ToString("F2") + "%</color>");

        await Task.Delay(delayTimeInt);
        playerController.UpdatePlayerMoveSpeed(0);

        DisableUIIcon();
    }
    async Task TaskActiveRingOfFire()
    {
        GameData gameData = ManagerData.Load();

        levelCanvas.TextLevelInfo($"Skill:\nFire Ring");


        for (int i = 0; i < xTimes; i++)
        {
            Vector3 pos = new(playerController.GetComponent<Transform>().position.x, playerController.GetComponent<Transform>().position.y + 1.5f, 4);
            ringOfFire.GetComponent<RingOfFire>().target = target;
            ringOfFire.GetComponent<RingOfFire>().damage = gameData.weaponDmg * buffValue / 100;
            Instantiate(ringOfFire, pos, Quaternion.Euler(0, 0, 0));
            await Task.Delay(delayTimeInt);
        }

        DisableUIIcon();
    }
    async Task TaskActiveWindBlade()
    {
        GameData gameData = ManagerData.Load();

        levelCanvas.TextLevelInfo($"Skill: \nBlades Of Wind");
        for (int i = 0; i < xTimes; i++)
        {
            Vector3 pos = new(playerController.GetComponent<Transform>().position.x, playerController.GetComponent<Transform>().position.y + 1.5f, 4);

            windBlade.GetComponent<WindBlade>().target = target;
            windBlade.GetComponent<WindBlade>().damage = gameData.weaponDmg * buffValue / 100;
            windBlade.GetComponent<WindBlade>().mirrored = playerController.GetComponent<Transform>().rotation.y == 0 ? false : true;
            Instantiate(windBlade, pos, Quaternion.identity);
            await Task.Delay(delayTimeInt);
        }

        DisableUIIcon();
    }
    void DisableUIIcon()
    {
        uiSkillIcon.RemoveIcon(gameObject.GetComponent<SpriteRenderer>().sprite);
        Destroy(this.gameObject);
    }
}

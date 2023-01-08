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
            playerController = other.GetComponent<PlayerController>();
            playerStatus = other.GetComponentInChildren<PlayerStatus>();

            gameObject.GetComponent<SpriteRenderer>().sprite.name = id.ToString();
            if (!levelCanvas.CheckSkillActivated(id.ToString()))
            {
                gameObject.GetComponent<Transform>().position = new Vector3(-100, -100, -100);
                levelCanvas.EnableUISkillSlot(gameObject.GetComponent<SpriteRenderer>().sprite);
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

        await Task.Delay(delayTimeInt);

        gameData.buffSkillPowerUp = 0;
        ManagerData.Save(gameData);

        DisableUIIcon();
    }
    async Task TaskDefense()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillDefense = buffValue;
        ManagerData.Save(gameData);

        await Task.Delay(delayTimeInt);

        gameData.buffSkillDefense = 0;
        ManagerData.Save(gameData);

        DisableUIIcon();
    }
    async Task TaskLifeRecovery()
    {
        for (int i = 0; i < xTimes; i++)
        {
            Debug.Log("Life Recovery : " + buffValue);
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

        await Task.Delay(delayTimeInt);

        gameData.buffSkillRangeAtk = 0;
        ManagerData.Save(gameData);

        DisableUIIcon();
    }
    async Task TaskMoveSpeed()
    {
        playerController.UpdatePlayerMoveSpeed(buffValue);
        await Task.Delay(delayTimeInt);
        playerController.UpdatePlayerMoveSpeed(0);

        DisableUIIcon();
    }
    async Task TaskActiveRingOfFire()
    {
        GameData gameData = ManagerData.Load();

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
        levelCanvas.DisableUISkillICon(id.ToString());
        try
        {
            Destroy(this.gameObject);
        }
        catch (Exception)
        {
            Debug.Log("Objeto ja foi destruido!");
        }

    }

}

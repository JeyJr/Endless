using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SkillsBehavior : MonoBehaviour
{
    GameObject player;
    PlayerController playerController;
    PlayerStatus playerStatus;
    LevelController levelController;
    float delayTime;
    float buffValue;
    float xTimes;
    int id;

    [SerializeField] private LayerMask target;
    [SerializeField] private GameObject ringOfFire, windBlade;

    public void StartSkill(float delayTime, float buffValue, float xTimes, int id)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerStatus = player.GetComponent<PlayerStatus>();

        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();

        this.delayTime = delayTime;
        this.buffValue = buffValue;
        this.xTimes = xTimes;
        this.id = id;

        

        switch (id)
        {
            case 1000:
                StartCoroutine(PowerUp());
                break;
            case 1001:
                StartCoroutine(Defense());
                break;
            case 1002:
                StartCoroutine(LifeRecovery());
                break;
            case 1003:
                StartCoroutine(AtkSpeed());
                break;
            case 1004:
                StartCoroutine(Critical());
                break;
            case 1005:
                StartCoroutine(RangeAtk());
                break;
            case 1006:
                StartCoroutine(MoveSpeed());
                break;
            case 1007:
                StartCoroutine(ActiveRingOfFire());
                break;
            case 1008:
                StartCoroutine(ActiveWindBlade());
                break;
        }
    }

    IEnumerator PowerUp()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillPowerUp = buffValue;
        ManagerData.Save(gameData);

        yield return new WaitForSeconds(delayTime);

        gameData.buffSkillPowerUp = 0;
        ManagerData.Save(gameData);

        DisableUIIcon();
    }
    IEnumerator Defense()
    {
        GameData gameData = ManagerData.Load();

        gameData.buffSkillDefense = buffValue;
        ManagerData.Save(gameData);

        yield return new WaitForSeconds(delayTime);

        gameData.buffSkillDefense = 0;
        ManagerData.Save(gameData);

        DisableUIIcon();
    }
    IEnumerator LifeRecovery()
    {
        float lifeRec = ManagerData.Load().MaxLife * buffValue / 100;
        for (int i = 0; i < xTimes; i++)
        {
            playerStatus.Life = lifeRec;
            playerStatus.UpdateLifeBar();
            yield return new WaitForSeconds(delayTime);
        }

        DisableUIIcon();
    }
    IEnumerator AtkSpeed()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillAtkSpeed = buffValue;
        ManagerData.Save(gameData);

        yield return new WaitForSeconds(delayTime);

        gameData.buffSkillAtkSpeed = 0;
        ManagerData.Save(gameData);
        
        DisableUIIcon();
    }
    IEnumerator Critical()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillCritical = buffValue;
        ManagerData.Save(gameData);

        yield return new WaitForSeconds(delayTime);

        gameData.buffSkillCritical = 0;
        ManagerData.Save(gameData);

        DisableUIIcon();
    }
    IEnumerator RangeAtk()
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillRangeAtk = buffValue;
        ManagerData.Save(gameData);

        yield return new WaitForSeconds(delayTime);

        gameData.buffSkillRangeAtk = 0;
        ManagerData.Save(gameData);

        DisableUIIcon();
    }
    IEnumerator MoveSpeed()
    {
        playerController.UpdatePlayerMoveSpeed(buffValue);
        yield return new WaitForSeconds(delayTime);
        playerController.UpdatePlayerMoveSpeed(0);

        DisableUIIcon();
    }
    IEnumerator ActiveRingOfFire()
    {
        GameData gameData = ManagerData.Load();

        for (int i = 0; i < xTimes; i++)
        {
            Vector3 pos = new (player.GetComponent<Transform>().position.x, player.GetComponent<Transform>().position.y + 1.5f, 4);
            ringOfFire.GetComponent<RingOfFire>().target = target;
            ringOfFire.GetComponent<RingOfFire>().damage = gameData.weaponDmg * buffValue / 100;
            Instantiate(ringOfFire, pos, Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(delayTime);
        }

        DisableUIIcon();
    }
    IEnumerator ActiveWindBlade()
    {
        GameData gameData = ManagerData.Load();

        for (int i = 0; i < xTimes; i++)
        {
            Vector3 pos = new(player.GetComponent<Transform>().position.x, player.GetComponent<Transform>().position.y + 1.5f, 4);

            windBlade.GetComponent<WindBlade>().target = target;
            windBlade.GetComponent<WindBlade>().damage = gameData.weaponDmg * buffValue / 100;
            windBlade.GetComponent<WindBlade>().mirrored = player.GetComponent<Transform>().rotation.y == 0 ? false : true;
            Instantiate(windBlade, pos, Quaternion.identity);
            yield return new WaitForSeconds(delayTime);
        }

        DisableUIIcon();
    }

    void DisableUIIcon(){
        levelController.DisableUISkillICon(id.ToString());
        Destroy(this.gameObject);
    }

}

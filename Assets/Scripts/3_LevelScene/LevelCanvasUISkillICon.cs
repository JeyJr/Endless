using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LevelCanvasUISkillICon : MonoBehaviour
{
    [SerializeField] private GameObject mainUISkillsIcons;
    [SerializeField] private List<GameObject> imageRefPosition = new List<GameObject>();
    [SerializeField] private List<Skill> skills = new List<Skill>();

    void Start()
    {
        for(int i = 0; i < mainUISkillsIcons.transform.childCount; i++)
        {
            GameObject icon = mainUISkillsIcons.transform.GetChild(i).gameObject;
            imageRefPosition.Add(icon);
            icon.SetActive(false);
        }
    }

    public bool CheckID(int id)
    {
        return skills.Any(s => s.ID == id);
    }

    
    public void EnableBuff( int id, Sprite sprite, float buffValue, float buffDurationTime, BuffType buffType)
    {
        Skill newSkill = new Skill(id, sprite, buffValue, buffDurationTime, buffType);
        skills.Add(newSkill);

        //Atribuir ao primeiro obj desabilitado o icone de newObj e habilita-lo na HUD
        var img = imageRefPosition.FirstOrDefault(obj => !obj.activeSelf);
        img.GetComponent<Image>().sprite = newSkill.Sprite;
        img.SetActive(true);


        StartCoroutine(ReorganizeList());
        StartCoroutine(StartSkill(newSkill));
    }


    IEnumerator StartSkill(Skill skill)
    {
        SetBuffValue(skill.BuffValue, skill.BuffType); //Add buffValue
        yield return new WaitForSeconds(skill.BuffDurationTime);

        var img = imageRefPosition.Find(obj => obj.GetComponent<Image>().sprite == skill.Sprite);
        img.SetActive(false);
        img.GetComponent <Image>().sprite = null;

        SetBuffValue(0, skill.BuffType); //Finaliza o buff

        skills.Remove(skill);
        StartCoroutine(ReorganizeList());
    }

    private void SetBuffValue(float buffValue, BuffType buffType)
    {
        var data = ManagerData.Load();

        switch (buffType)
        {
            case BuffType.atk:
                data.buffSkillPowerUp = buffValue;
                break;
            case BuffType.def:
                data.buffSkillDefense = buffValue;
                break;
            case BuffType.cri:
                data.buffSkillCritical = buffValue;
                break;
            case BuffType.range:
                data.buffSkillRangeAtk = buffValue;
                break;
            case BuffType.move:
                GameObject.FindWithTag("Player").GetComponent<PlayerController>().UpdatePlayerMoveSpeed(buffValue);
                break;
            default:
                throw new ArgumentException("buffType invalido!: ", nameof(buffValue));
        }

        ManagerData.Save(data);
    }

    IEnumerator ReorganizeList()
    {
        var objEnabled = imageRefPosition.FindAll(m => m.activeSelf);

        for (int i = 0; i < imageRefPosition.Count; i++)
        {
            imageRefPosition[i].SetActive(false);
        }

        for (int i = 0; i < objEnabled.Count; i++)
        {
            imageRefPosition[i].GetComponent<Image>().sprite = objEnabled[i].GetComponent<Image>().sprite;
            imageRefPosition[i].SetActive(true);
        }

        yield return null;
    }
}

class Skill{

    int id;
    Sprite sprite;
    float buffValue;
    float buffDurationTime;
    BuffType buffType;

    public Skill(int id, Sprite sprite, float buffVlaue, float buffDurationTime, BuffType buffType)
    {
        this.id = id;
        this.sprite = sprite;
        this.buffValue = buffVlaue;
        this.buffDurationTime = buffDurationTime;
        this.buffType = buffType;
    }

    public int ID { get => id; }
    public Sprite Sprite { get => sprite; }
    public float BuffValue { get => buffValue; }
    public float BuffDurationTime { get => buffDurationTime; }
    public BuffType BuffType { get => buffType; }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefiningThePlayerSkill : MonoBehaviour
{
    [SerializeField] private List<Sprite> skillIcon;
    public int skillNum;
    TextSkillInfo textSkillInfo;

    private void Awake()
    {
        skillNum = Mathf.RoundToInt(Random.Range(0, skillIcon.Count));
        GetComponent<SpriteRenderer>().sprite = skillIcon[skillNum];
        Destroy(this.gameObject, 15);

        textSkillInfo = GameObject.Find("TextPlayerSkills").GetComponent<TextSkillInfo>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            switch (skillNum)
            {
                case 0:
                    textSkillInfo.SetTextSkillInfo("Life recovery activated");
                    other.GetComponent<PlayerSkill>().SkillLifeRecovery();
                    break;
                case 1:
                    textSkillInfo.SetTextSkillInfo("Activated movement speed bonus");
                    other.GetComponent<PlayerSkill>().SkillMoveSpeed();
                    break;
                case 2:
                    textSkillInfo.SetTextSkillInfo("Damage bonus activated");
                    other.GetComponent<PlayerSkill>().SkillPowerUp();
                    break;
                case 3:
                    textSkillInfo.SetTextSkillInfo("Attack speed bonus activated");
                    other.GetComponent<PlayerSkill>().SkillAtkSpeed();
                    break;
                case 4:
                    textSkillInfo.SetTextSkillInfo("Atk range bonus activated");
                    other.GetComponent<PlayerSkill>().SkillRangeAtk();
                    break;
                case 5:
                    textSkillInfo.SetTextSkillInfo("Skill Ring Of Fire activated");
                    other.GetComponent<PlayerSkill>().SkillRingOfFire();
                    break;
                case 6:
                    textSkillInfo.SetTextSkillInfo("Skill Wind Blade activated");
                    other.GetComponent<PlayerSkill>().SkillWindBlade();
                    break;
                default:
                    textSkillInfo.SetTextSkillInfo("Life recovery activated");
                    other.GetComponent<PlayerSkill>().SkillLifeRecovery();
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}

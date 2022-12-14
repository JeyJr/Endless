using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class DefiningThePlayerSkill : MonoBehaviour
{
    [SerializeField] private List<Sprite> skillIcon;
    [SerializeField] private int skillNum;
  

    private void Awake()
    {
        skillNum = Mathf.RoundToInt(Random.Range(0, skillIcon.Count));
        GetComponent<SpriteRenderer>().sprite = skillIcon[skillNum];
        Destroy(this.gameObject, 15);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            if(skillNum == 0 && !other.GetComponent<PlayerSkill>().GetLifeRecoveryActive())
            {
                other.GetComponent<PlayerSkill>().ActiveSkillLifeRecovery();
                Destroy(this.gameObject);
            }
            else if (skillNum == 1 && !other.GetComponent<PlayerSkill>().GetMoveSpeedActive())
            {
                other.GetComponent<PlayerSkill>().ActiveSkillMoveSpeed();
                Destroy(this.gameObject);
            }
            else if (skillNum == 2 && !other.GetComponent<PlayerSkill>().GetPowerUpActive())
            {
                other.GetComponent<PlayerSkill>().ActiveSkillMoveSpeed();
                Destroy(this.gameObject);
            }
            else if (skillNum == 3 && !other.GetComponent<PlayerSkill>().GetAtkSpeedActive())
            {
                other.GetComponent<PlayerSkill>().ActiveSkillMoveSpeed();
                Destroy(this.gameObject);
            }
            else if (skillNum == 4 && !other.GetComponent<PlayerSkill>().GetRangeAtkActive())
            {
                other.GetComponent<PlayerSkill>().ActiveSkillMoveSpeed();
                Destroy(this.gameObject);
            }
            else if (skillNum == 5 && !other.GetComponent<PlayerSkill>().GetRingOfFireActive())
            {
                other.GetComponent<PlayerSkill>().ActiveSkillMoveSpeed();
                Destroy(this.gameObject);
            }
            else if (skillNum == 6 && !other.GetComponent<PlayerSkill>().GetWindBladeActive())
            {
                other.GetComponent<PlayerSkill>().ActiveSkillMoveSpeed();
                Destroy(this.gameObject);
            }
        }
    }
}

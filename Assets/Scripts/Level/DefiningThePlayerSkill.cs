using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


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

            if (skillNum == 0 && !other.GetComponent<PlayerSkill>().LifeRecoveryIsActive)
            {
                other.GetComponent<PlayerSkill>().ActiveSkillLifeRecovery();
                Destroy(this.gameObject);
            }
            else if (skillNum == 1 && !other.GetComponent<PlayerSkill>().MoveSpeedIsActive)
            {
                other.GetComponent<PlayerSkill>().ActiveSkillMoveSpeed();
                Destroy(this.gameObject);
            }
            else if (skillNum == 2 && !other.GetComponent<PlayerSkill>().PowerUpIsActive)
            {
                other.GetComponent<PlayerSkill>().ActiveSkillPowerUp();
                Destroy(this.gameObject);
            }
            else if (skillNum == 3 && !other.GetComponent<PlayerSkill>().AtkSpeedIsActive)
            {
                other.GetComponent<PlayerSkill>().ActiveSkillAtkSpeed();
                Destroy(this.gameObject);
            }
            else if (skillNum == 4 && !other.GetComponent<PlayerSkill>().RangeAtkIsActive)
            {
                other.GetComponent<PlayerSkill>().ActiveSkillRangeAtk();
                Destroy(this.gameObject);
            }
            else if (skillNum == 5 && !other.GetComponent<PlayerSkill>().RingOfFireIsActive)
            {
                other.GetComponent<PlayerSkill>().ActiveSkillRingOfFire();
                Destroy(this.gameObject);
            }
            else if (skillNum == 6 && !other.GetComponent<PlayerSkill>().WindBladeIsActive)
            {
                other.GetComponent<PlayerSkill>().ActiveSkillWindBlade();
                Destroy(this.gameObject);
            }
        }
    }

}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefiningThePlayerSkill : MonoBehaviour
{
    [SerializeField] private List<Sprite> skillIcon;
    public int skillNum;

    private void Awake()
    {
        skillNum = Mathf.RoundToInt(Random.Range(0, skillIcon.Count));
        GetComponent<SpriteRenderer>().sprite = skillIcon[skillNum];

        if(skillNum > 0)
        {
            Debug.Log("Não é skill recovery");
            Destroy(this.gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerSkill>().SkillLifeRecovery();
            Destroy(this.gameObject);
        }
    }
}

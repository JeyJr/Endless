using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatingSkills : MonoBehaviour
{
    [Header("Skill attributes")]
    [SerializeField] private int id;
    [SerializeField] private float delayTime;
    [SerializeField] private float buffValue;
    [SerializeField] private float xTimes;

    [SerializeField] private SkillsBehavior SkillsBehavior;
    LevelController levelController;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
            gameObject.GetComponent<SpriteRenderer>().sprite.name = id.ToString();

            if (!levelController.CheckSkillActivated(id.ToString()))
            {
                gameObject.GetComponent<Transform>().position = new Vector3(-100, -100, -100);

                levelController.EnableUISkillSlot(gameObject.GetComponent<SpriteRenderer>().sprite);

                SkillsBehavior.StartSkill(delayTime, buffValue, xTimes, id);
            }

        }
    }
}

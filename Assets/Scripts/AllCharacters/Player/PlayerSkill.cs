using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] private GameObject txtSkills;
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private Transform spawnPosition;

    [Header("Life Recovery")]
    public float lifeRecoveredTimesCounter;
    public float lifeRecoveryValue;
    public float delayTimeToRecovery;

    #region LifeRecovery
    public void SkillLifeRecovery()
    {
        StartCoroutine(LifeRecovery());
    }

    IEnumerator LifeRecovery()
    {
        for (int i = 0; i < lifeRecoveredTimesCounter; i++)
        {
            playerStatus.Life = lifeRecoveryValue;
            txtSkills.GetComponent<TextMeshPro>().text = $"Life +{lifeRecoveryValue}";
            Instantiate(txtSkills, spawnPosition.position, Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(delayTimeToRecovery);
        }
    }
    #endregion
}

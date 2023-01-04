using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnText : MonoBehaviour
{
    public TextMeshPro txtDmg, txtSkill;
    public Transform spawnPosition;
    [SerializeField] private float y;
    public void SpawnTextDamage(float dmg, bool critical)
    {
        StartCoroutine(TextDamage(dmg, critical));
    }

    IEnumerator TextDamage(float dmg, bool critical)
    {
        txtDmg.text = dmg.ToString("F0");

        if (critical)
            txtDmg.color = Color.red;
        else
            txtDmg.color = Color.white;

        Vector3 pos = spawnPosition.position;
        pos.y += y;

        Instantiate(txtDmg, pos, Quaternion.Euler(0, 0, 0));
        yield return null;
    }


    public void SpawnTextSkill(float value, string desc)
    {
        StartCoroutine(TextSkill(value, desc));
    }

    IEnumerator TextSkill(float value, string desc)
    {
        txtSkill.text = desc + value.ToString("F0");

        Vector3 pos = spawnPosition.position;
        pos.y += y;

        Instantiate(txtSkill, pos, Quaternion.Euler(0, 0, 0));
        yield return null;
    }

}

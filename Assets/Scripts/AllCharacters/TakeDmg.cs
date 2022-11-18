using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TakeDmg : MonoBehaviour
{
    public TextMeshPro txtDmg;
    public Transform spawnPosition;

    public void TakeDamage(float dmg)
    {
        StartCoroutine(SpawnTextDamage(dmg));
    }

    IEnumerator SpawnTextDamage(float dmg)
    {
        yield return null;
        txtDmg.text = dmg.ToString("F0");

        Vector3 pos = spawnPosition.position;
        pos.z = -2;
        Instantiate(txtDmg, pos, Quaternion.Euler(0, 0, 0));
    }
}

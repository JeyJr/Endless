using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAtk : MonoBehaviour
{
    public Transform startPosition;
    [SerializeField] private float atkRange;
    [SerializeField] private LayerMask enemyMask;

    public void Atk()
    {
        GameData gameData = ManagerData.Load();
        atkRange = gameData.weaponRangeAtk;

        RaycastHit2D[] hit = Physics2D.RaycastAll(startPosition.position, startPosition.right, atkRange, enemyMask);

        if(hit != null)
        {
            for (int i = 0; i < hit.Length; i++)
            {

                if (Critical(gameData.CriticalDMG))
                    hit[i].collider.GetComponent<EnemyStatus>().LoseLife(CriticalDMG(gameData.Damage), true);
                else
                    hit[i].collider.GetComponent<EnemyStatus>().LoseLife(SimpleDMG(gameData.Damage), false);
            }
        }
    }

    bool Critical(float cri)
    {
        float value = Random.Range(0, 100);
        return value <= cri;
    }

    float CriticalDMG(float damage) 
    {
        return Random.Range(damage * 2, (damage * 2) * 1.2f);
    }
    float SimpleDMG(float damage)
    {
        return Random.Range(damage, damage * 1.2f);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(startPosition.position, startPosition.right * atkRange, Color.red);
    }
}

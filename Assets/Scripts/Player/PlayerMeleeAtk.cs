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
        atkRange = WeaponData.GetWeaponAtkRange();
        RaycastHit2D[] hit = Physics2D.RaycastAll(startPosition.position, startPosition.right, atkRange, enemyMask);

        if(hit != null)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                GameData gameData = ManagerData.Load();

                if (gameData.CriticalDMG)
                    hit[i].collider.GetComponent<EnemyStatus>().LoseLife(CriticalDMG(gameData.Damage), true);
                else
                    hit[i].collider.GetComponent<EnemyStatus>().LoseLife(SimpleDMG(gameData.Damage), false);
            }
        }
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

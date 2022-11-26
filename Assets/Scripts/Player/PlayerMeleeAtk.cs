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
                if (GameData.GetCritical())
                    hit[i].collider.GetComponent<EnemyStatus>().LoseLife(CriticalDMG(), true);
                else
                    hit[i].collider.GetComponent<EnemyStatus>().LoseLife(SimpleDMG(), false);
            }
        }
    }

    float CriticalDMG() 
    {
        return Random.Range(GameData.GetDMG() * 2, (GameData.GetDMG() * 2) * 1.2f);
    }
    float SimpleDMG()
    {
        return Random.Range(GameData.GetDMG(), GameData.GetDMG()* 1.2f);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(startPosition.position, startPosition.right * atkRange, Color.red);
    }
}

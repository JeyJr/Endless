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
        RaycastHit2D[] hit = Physics2D.RaycastAll(startPosition.position, startPosition.right, atkRange, enemyMask);

        if(hit != null)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                float dmg = GameData.GetDMG();
                hit[i].collider.GetComponent<TakeDmg>().TakeDamage(Random.Range(dmg, dmg * 1.5f));
            }
        }
    }


    private void OnDrawGizmos()
    {
        Debug.DrawRay(startPosition.position, startPosition.right * atkRange, Color.red);
    }
}

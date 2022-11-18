using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAtk : MonoBehaviour
{
    public Transform startPosition;
    [SerializeField] private float atkRange;
    [SerializeField] private LayerMask enemyMask;

    //Deletar
    public float damage;

    public void Atk()
    {
        RaycastHit2D[] hit = Physics2D.RaycastAll(startPosition.position, startPosition.right, atkRange, enemyMask);

        if(hit != null)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                hit[i].collider.GetComponent<TakeDmg>().TakeDamage(damage);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Debug.DrawRay(startPosition.position, startPosition.right * atkRange, Color.red);
    }
}

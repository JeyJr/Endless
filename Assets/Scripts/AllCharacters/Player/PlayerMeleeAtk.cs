using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMeleeAtk : MonoBehaviour
{
    [SerializeField] private float atkRange; //DELETAR
    [SerializeField] private LayerMask enemyMask;

    [SerializeField] private Transform atkPosition;
    

    public void Atk()
    {
        GameData gameData = ManagerData.Load();

        atkRange = gameData.RangeAtk;

        //RaycastHit[] hit = Physics.BoxCastAll(pos, boxScale, boxDirection, atkPosition.rotation, maxDistance, enemyMask);
        RaycastHit2D[] hit = Physics2D.RaycastAll(atkPosition.position, atkPosition.right, atkRange, enemyMask);

        if (hit != null)
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
        Debug.DrawRay(atkPosition.position, atkPosition.right * atkRange, Color.yellow);

        //Gizmos.color = Color.yellow;

        //Vector3 pos = atkPosition.position;
        //pos.x += x;

        //Gizmos.DrawRay(pos, boxDirection * maxDistance);
        //Gizmos.DrawWireCube(pos + boxDirection * maxDistance, boxScale * 2);

    }
}

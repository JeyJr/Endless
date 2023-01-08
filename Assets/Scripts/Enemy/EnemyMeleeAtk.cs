using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAtk : MonoBehaviour
{
    [Header("RayCasts Control")]
    [SerializeField] private Transform rayCastAtkInitialPosition;
    [SerializeField] private float atkRange;
    [SerializeField] private LayerMask target;

    [Header("Animations")]
    [SerializeField] private Animator anim;

    [Header("Atk Control")]
    [SerializeField] private float delayToAtkAgain;
    EnemyStatus enemyStatus;

    private void Start()
    {
        enemyStatus = GetComponent<EnemyStatus>();
    }

    private void Update()
    {
        //if (!IsAttacking && !waitingState && enemyStatus.EnemyIsAlive)
        //    DetectingTargetToAtk();
    }

    //Called in anim Atk
    void DetectingTargetToAtk()
    {
        RaycastHit2D h = Physics2D.Raycast(
            rayCastAtkInitialPosition.position,
            rayCastAtkInitialPosition.right,
            atkRange,
            target );

        if(h.collider != null )
        {
            SetTrueToTheStateOfAtk();
            anim.Play($"Base Layer.{enemyStatus.EnemyAnimName}_Atk", 0);
        }
        else if(h.collider != null)
            h.collider.gameObject.GetComponent<PlayerStatus>().LoseLife(enemyStatus.Damage, enemyStatus.Critical);

    }

    public void SetTrueToTheStateOfAtk() {
        enemyStatus.IsAttacking = true;
        StartCoroutine(StartTimerToAtkAgain());
    }

    //Called on anim Atk
    public void SetFalseToTheStateOfAtk()
    {
        enemyStatus.IsAttacking = false;
    }

    IEnumerator StartTimerToAtkAgain()
    {
        yield return new WaitForSeconds(delayToAtkAgain);
    }


    private void OnDrawGizmos()
    {
        Debug.DrawRay(rayCastAtkInitialPosition.position, rayCastAtkInitialPosition.right * atkRange, Color.blue);
    }
}

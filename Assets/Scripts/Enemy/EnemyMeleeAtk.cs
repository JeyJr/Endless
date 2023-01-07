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
    bool isAttacking, waitingState;
    [SerializeField] private float delayToAtkAgain;
    public bool IsAttacking { get => isAttacking;}
    EnemyStatus enemyStatus;

    private void Start()
    {
        enemyStatus = GetComponent<EnemyStatus>();
    }

    private void Update()
    {
        if (!IsAttacking && !waitingState && enemyStatus.EnemyIsAlive)
            DetectingTargetToAtk();
    }

    //Called in anim Atk
    void DetectingTargetToAtk()
    {
        RaycastHit2D h = Physics2D.Raycast(
            rayCastAtkInitialPosition.position,
            rayCastAtkInitialPosition.right,
            atkRange,
            target );

        if(h.collider != null && !IsAttacking)
        {
            ChangeTheStateOfAtk();
            StartCoroutine(PlayAtkAnimation());
        }
        else if(h.collider != null && IsAttacking)
        {
            h.collider.gameObject.GetComponent<PlayerStatus>().LoseLife(enemyStatus.Damage, enemyStatus.Critical);
        }
    }

    IEnumerator PlayAtkAnimation()
    {
        anim.Play($"Base Layer.{enemyStatus.EnemyAnimName}_Atk", 0);
        waitingState = true;
        yield return new WaitForSeconds(delayToAtkAgain);
        waitingState = false;
    }

    //Called on last framd anim Atk
    public void ChangeTheStateOfAtk() {
        isAttacking = !isAttacking;
        enemyStatus.IsAttacking = isAttacking;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(rayCastAtkInitialPosition.position, rayCastAtkInitialPosition.right * atkRange, Color.blue);
    }
}

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
    [SerializeField] private string enemyNameInAnimations;
    [SerializeField] private Animator anim;

    [Header("Atk Control")]
    bool isAttacking, waitingState;
    public bool IsAttacking { get => isAttacking;}

    private void Update()
    {
        if (!isAttacking && !waitingState)
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

        if(h.collider != null && !isAttacking)
        {
            ChangeTheStateOfAtk();
            StartCoroutine(PlayAtkAnimation());
        }
        else if(h.collider != null && isAttacking)
        {
            h.collider.gameObject.GetComponent<PlayerStatus>().LoseLife(20, false);
        }
    }

    IEnumerator PlayAtkAnimation()
    {
        anim.Play($"Base Layer.{enemyNameInAnimations}_Atk", 0);
        waitingState = true;
        yield return new WaitForSeconds(5);
        waitingState = false;
    }

    //Called on last framd anim Atk
    public void ChangeTheStateOfAtk() => isAttacking = !isAttacking;

    private void OnDrawGizmos()
    {
        Debug.DrawRay(rayCastAtkInitialPosition.position, rayCastAtkInitialPosition.right * atkRange, Color.blue);
    }
}

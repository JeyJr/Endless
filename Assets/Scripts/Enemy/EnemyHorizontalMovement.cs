using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontalMovement : MonoBehaviour
{
    [Header("RayCasts Control")]
    [SerializeField] private Transform rayCastFrontInitialPosition;
    [SerializeField] private Transform rayCastBackInitialPosition;
    [SerializeField] private float frontRange, backRange;
    [SerializeField] private LayerMask target;

    [Header("Movement Control")]
    [SerializeField] private float moveSpeed;
    bool changeDir;

    [Header("Animations")]
    [SerializeField] private Animator anim;
    [SerializeField] private EnemyStatus enemyStatus;

    private void Start()
    {
        enemyStatus = GetComponentInChildren<EnemyStatus>();
    }

    private void Update()
    {
        if (!enemyStatus.IsAttacking && enemyStatus.EnemyIsAlive)
        {
            DetectTargetOnTheFrontSide();
            DetectTargetOnTheBackSide();
        }
    }


    void DetectTargetOnTheFrontSide()
    {
        RaycastHit2D h = Physics2D.Raycast(
            rayCastFrontInitialPosition.position, 
            transform.right, 
            frontRange, 
            target);

        if(h.collider != null)
        {
            Vector3 targetPos = new Vector3(h.collider.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            anim.Play($"Base Layer.{enemyStatus.EnemyAnimName}_Run");
        }
        else
        {
            anim.Play($"Base Layer.{enemyStatus.EnemyAnimName}_Idle");
        }
    }

    void DetectTargetOnTheBackSide()
    {
        RaycastHit2D h = Physics2D.Raycast(
            rayCastBackInitialPosition.position,
            -transform.right,
            backRange,
            target);

        if (h.collider != null)
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        if (!changeDir)
        {
            changeDir = true;
            transform.localEulerAngles = new Vector3(0, transform.localRotation.y == 0 ? 180 : 0, 0);
            StartCoroutine(DelayToChangeDir());
        }
    }

    IEnumerator DelayToChangeDir()
    {
        yield return new WaitForSeconds(2);
        changeDir = false;
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(rayCastFrontInitialPosition.position, rayCastFrontInitialPosition.right * frontRange, Color.yellow);
        Debug.DrawRay(rayCastBackInitialPosition.position, -rayCastBackInitialPosition.right * backRange, Color.red);
    }
}

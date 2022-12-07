using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySimpleBehavior : MonoBehaviour
{
    EnemyStatus enemyStatus;
    Animator anim;
    [SerializeField] private PlayerStatus playerStatus;

    [Header("MOVE")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isMoving;
    [SerializeField] private Transform areaToMove;
    [SerializeField] private float rangeToMove, delayRandomMove;
    [SerializeField] private Vector3 randomTarget;

    [Header("ATK")]
    [SerializeField] private bool isAtk;
    [SerializeField] private float atkRange, backRange;
    [SerializeField] private LayerMask targetToAtk;

    private void Awake()
    {
        enemyStatus = GetComponent<EnemyStatus>();
        anim = GetComponent<Animator>();

        StartCoroutine(SetRandomTarget());
    }

    private void FixedUpdate()
    {

        if (!isAtk && isMoving)
        {
            EnemyRandomMovement();
        }

        DetectingPlayerInAtkRange();
        DetectPlayerBehind();
        Anims();
    }

    #region EnemyMovement
    //void EnemyChasingPlayer()
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    //}
    void EnemyRandomMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, randomTarget, moveSpeed * Time.deltaTime);

        if (transform.position.x > randomTarget.x)
            RotateEnemy(180);
        else
            RotateEnemy(0);

        if (transform.position == randomTarget)
        {
            Debug.Log("Chegou no destino!");
            isMoving = false;
            StartCoroutine(SetRandomTarget());
        }
    }
    void DetectPlayerBehind()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, -transform.right, backRange, targetToAtk);

        if (hit2D.collider != null)
        {
            if(hit2D.collider.transform.position.x > transform.position.x)
                RotateEnemy(0);
            else
                RotateEnemy(180);
        }
            
    }
    void RotateEnemy(float value)
    {
        transform.localEulerAngles = new Vector3(0, value, 0);
    }
    IEnumerator SetRandomTarget()
    {
        yield return new WaitForSeconds(delayRandomMove);
        isMoving = true;

        float x = Random.Range(areaToMove.position.x - rangeToMove, areaToMove.position.x + rangeToMove);
        float y = Random.Range(areaToMove.position.y - rangeToMove, areaToMove.position.y + rangeToMove);

        randomTarget = new Vector3(x, y, 0);
    }

    #endregion

    #region EnemyAtk

    //Called in last frame of animation Atk
    public void SetIsAtk() { 
        isAtk = !isAtk;
    }

    public void DetectingPlayerInAtkRange()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.right, atkRange, targetToAtk);

        if(hit2D.collider != null)
        {
            randomTarget = transform.position;
            isAtk = true;
        }
    }

    public void AttackingPlayer()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.right, atkRange, targetToAtk);
        if(hit2D.collider != null)
            playerStatus.LoseLife(enemyStatus.Damage, enemyStatus.Critical);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(transform.position, transform.right * atkRange, Color.yellow);
        Debug.DrawRay(transform.position, -transform.right * backRange, Color.red);
    }


    #endregion

    #region ANIMS

    private void Anims()
    {
        if(enemyStatus.Life > 0)
        {
            if (isAtk)
            {
                anim.Play($"Base Layer.{gameObject.name}Atk");
            }
            else
            {
                if (isMoving)
                    anim.Play($"Base Layer.{gameObject.name}Run");
                else
                    anim.Play($"Base Layer.{gameObject.name}Idle");
            }
        }
        else
        {
            anim.Play($"Base Layer.{gameObject.name}Dying");
        }
    }


    //Called in last frame of animation Dying
    public void DestroyDeadEnemy()
    {
        Destroy(this.gameObject);
    }
    #endregion
}

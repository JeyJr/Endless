using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    EnemyStatus enemyStatus;
    Animator anim;

    [Header("Idle")]
    Task taskAwait;
    public bool waitLoopActions;
    public int delayToWait;

    [Header("MoveControl")]
    Task taskFindPlayer;
    private float mSpeed;
    Vector3 targetToMove;

    [Header("AtkControl")]
    Task taskAtkPlayer;
    bool findPlayerPosition, detectPlayerInRangeToAtk;

    [Header("Raycast")]
    public Transform atkPos;
    public float atkRange;
    public LayerMask target;


    private void Start()
    {
        enemyStatus = GetComponentInChildren<EnemyStatus>();
        anim = GetComponentInChildren<Animator>();


        delayToWait = Random.Range(delayToWait, delayToWait * 2);
        mSpeed = Random.Range(1f, 5);
    }

    private void Update()
    {
        if (enemyStatus.IsAlive)
        {
            if (!enemyStatus.IsAttacking && !waitLoopActions)
            {
                MoveControl();
            }
            else if(enemyStatus.IsAttacking && !waitLoopActions)
            {
                AtkControl();
            }
        }
        else
        {
            PlayAnim("Dead");
            StopCoroutine(DetectPlayerRangeToAtk());
        }
    }

    //IDLE
    public async void SetAwait()
    {
        taskAwait = Wait();
        await taskAwait;
    }
    async Task Wait()
    {
        waitLoopActions = true;
        PlayAnim("Idle");
        await Task.Delay(delayToWait * 1000);
        waitLoopActions = false;
    }

    //MOVE
    async void MoveControl()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetToMove, mSpeed * Time.deltaTime);
        
        PlayAnim("Run");

        if (!findPlayerPosition)
        {
            findPlayerPosition = true;
            taskFindPlayer = TaskFindPlayerPosition();
            await taskFindPlayer;
        }

        if (!detectPlayerInRangeToAtk && enemyStatus.Life > 0)
        {
            detectPlayerInRangeToAtk = true;

            try
            {
                StartCoroutine(DetectPlayerRangeToAtk());            
            }
            catch (System.Exception)
            {
                throw new System.Exception("Coroutine não iniciada!");
            }
        }
    }
    async Task TaskFindPlayerPosition()
    {
        targetToMove = new Vector3(
            GameObject.FindWithTag("Player").GetComponent<Transform>().position.x,
            transform.position.y,
            8
            );

        if (transform.position.x > targetToMove.x)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            GetComponentInChildren<EnemyLifeBar>().SetLiferBarBehavior(true);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            GetComponentInChildren<EnemyLifeBar>().SetLiferBarBehavior(false);
        }

        await Task.Delay(1000);
        findPlayerPosition = false;
    }
   
    //ATK
    void AtkControl()
    {
        PlayAnim("Atk");
    }
    IEnumerator DetectPlayerRangeToAtk()
    {
        if (enemyStatus.IsAlive)
        {
            RaycastHit2D hit = Physics2D.Raycast(atkPos.position, atkPos.right, atkRange, target);

            if (hit.collider != null)
            {
                enemyStatus.IsAttacking = true;
            }

            yield return new WaitForSeconds(1);
            detectPlayerInRangeToAtk = false;
        }
    }
    public void SetTargetDamage(float dmg, float critical)
    {
        RaycastHit2D hit = Physics2D.Raycast(atkPos.position, atkPos.right, atkRange, target);
        
        if(hit.collider != null)
        {
            hit.collider.GetComponentInChildren<PlayerStatus>().LoseLife(dmg, critical);
        }
    }

    //Anims
    void PlayAnim(string name) => anim.Play($"Base Layer.{enemyStatus.EnemyAnimName}_{name}");
    
    private void OnDrawGizmos()
    {
        Debug.DrawRay(atkPos.position, atkPos.right * atkRange, Color.blue);
    }
}

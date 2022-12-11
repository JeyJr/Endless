using System.Collections;
using UnityEngine;


public class EnemySimpleBehavior : MonoBehaviour
{
    [Space(5)]
    [Header("RayCast to Detecting target")]
    [SerializeField] private LayerMask targetToAtk;
    [SerializeField] private LayerMask targetZone;

    [SerializeField] private Transform frontPosition, backPosition, atkPosition;
    [SerializeField] private float frontRange, backRange, atkRange;

    [Space(5)]
    [Header("Basic Movement")]
    [SerializeField] private bool atkInDelayTime,isAtk, isChasingTarget;
    [SerializeField] private float moveSpeed;

    [Space(5)]
    [Header("Animation")]
    [SerializeField] private Animator anim;
    EnemyStatus enemyStatus;
    PlayerStatus playerStatus;
    [SerializeField] private string enemyName;

    [Header("Boss")]
    [SerializeField] private bool enemyBoss;

    [Header("Death")]
    [SerializeField] private LevelController levelController;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyStatus = GetComponent<EnemyStatus>();
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        levelController = GameObject.FindGameObjectWithTag("LevelController").GetComponent<LevelController>();
        Rotate(Random.Range(0, 10) > 5);
    }

    private void Update()
    {
        if (enemyStatus.Life > 0)
        {
            if (!isAtk)        
            {
                PlayAnimations();
                TargetInAtkArea();
                DetectTargetInFront();
            }
        }
        else
        {
            anim.Play($"Base Layer.{enemyName}Dying");
        }

        DetectTargetBehind();
    }

    #region DetectTarget
    void DetectTargetInFront()
    {
        RaycastHit2D hit = Physics2D.Raycast(frontPosition.position, frontPosition.right, frontRange, targetToAtk); 
        
        if(hit.collider != null)
        {
            Vector3 targetPos = new Vector3(hit.collider.transform.position.x, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            isChasingTarget = true;
        }
        else
        {
            isChasingTarget = false;
        }
    }
    void DetectTargetBehind()
    {
        RaycastHit2D hit = Physics2D.Raycast(backPosition.position, -backPosition.right, backRange, targetToAtk);

        if (hit.collider != null)
        {
            if (hit.collider.transform.position.x > transform.position.x)
                Rotate(true);
            else
                Rotate(false);
        }
    }
    void TargetInAtkArea()
    {
        RaycastHit2D hit = Physics2D.Raycast(atkPosition.position, atkPosition.right, atkRange, targetToAtk);

        if (hit.collider != null)
        {
            StartCoroutine(StartAtk());
            isAtk = true;
        }
    }
    void Rotate(bool mirrored)
    {
        transform.rotation = Quaternion.Euler(0, mirrored ? 0 : 180, 0);
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(frontPosition.position, frontPosition.right * frontRange, Color.blue);
        Debug.DrawRay(backPosition.position, -backPosition.right * backRange, Color.yellow);
        Debug.DrawRay(atkPosition.position, atkPosition.right * atkRange, Color.red);
    }
    #endregion

    #region ATK

    IEnumerator StartAtk()
    {
        anim.Play($"Base Layer.{enemyName}Idle");
        yield return new WaitForSeconds(enemyStatus.AtkSpeed);
        anim.Play($"Base Layer.{enemyName}Atk");
    }

    //Called in enemy anim atk
    public void HitTarget()
    {
        RaycastHit2D hit = Physics2D.Raycast(atkPosition.position, atkPosition.right, atkRange, targetToAtk);

        if (hit.collider != null)
        {
            //Debug.Log($"Teve Colisão, DMG: {enemyStatus.Damage}, CRI: {enemyStatus.Critical}");
            playerStatus.LoseLife(enemyStatus.Damage, enemyStatus.Critical);
        }
    }

    //Called in last frame on enemy anim atk
    public void ChangeIsAtk() {
        isAtk = !isAtk;
    }

    #endregion

    #region Anims
    void PlayAnimations() {

        if (isChasingTarget)
            anim.Play($"Base Layer.{enemyName}Run");
        else
            anim.Play($"Base Layer.{enemyName}Idle");
    }

    #endregion


    //Called in the last frame Dying
    public void DestroyThisEnemy()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 2, targetZone);

        if (hit.collider != null)
        {
            hit.collider.GetComponentInParent<ZoneControl>().EnemyInTheZoneDie(this.gameObject);
            
            if(enemyBoss)
                hit.collider.GetComponentInParent<ZoneControl>().BossIsDead();
        }

        levelController.EnemyDead(enemyStatus.GoldDrop, enemyBoss);

        Destroy(this.gameObject);
    }

}





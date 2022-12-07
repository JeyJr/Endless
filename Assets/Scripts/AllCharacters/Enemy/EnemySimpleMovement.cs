using System.Collections;
using UnityEngine;

public class EnemySimpleMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool followPlayer;

    [SerializeField] private Transform areaToMove;
    [SerializeField] private float rangeToMove;
    [SerializeField] private float delayRandomMove;
    [SerializeField] private Vector3 randomTarget;


    bool setNewPosition;

    private void Start()
    {
        randomTarget = new Vector3(areaToMove.position.x + rangeToMove, areaToMove.position.y - rangeToMove, 0);
        //StartCoroutine(SetRandomTarget(0));
    }

    private void FixedUpdate()
    {
        if (followPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else if(!followPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, randomTarget, moveSpeed * Time.deltaTime);

            if (!setNewPosition)
            {
                setNewPosition = true;
                StartCoroutine(SetRandomTarget(delayRandomMove));
            }
        }

    }

    IEnumerator SetRandomTarget(float delay)
    {
        yield return new WaitForSeconds(delay);

        float x = Random.Range(areaToMove.position.x -rangeToMove, areaToMove.position.x + rangeToMove); 
        float y = Random.Range(areaToMove.position.y -rangeToMove, areaToMove.position.y + rangeToMove);
;
        randomTarget = new Vector3(x, y, 0);
        setNewPosition = false;
    }
}

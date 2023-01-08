using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TextSpawnedBehavior : MonoBehaviour
{
    [SerializeField] private float mSpeed;
    [SerializeField] private float distance;

    [SerializeField] private float delayToDestroy;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, - 3);
        mSpeed = Random.Range(2, 4);
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        float elapsedTime = 0;
        Vector3 startingPos = transform.position;
        Vector3 endPos = transform.position + transform.up * distance;

        while (elapsedTime < delayToDestroy)
        {
            transform.position = Vector3.Lerp(startingPos, endPos, (elapsedTime / delayToDestroy));
            elapsedTime += Time.deltaTime * mSpeed;
            yield return new WaitForEndOfFrame();
        }

        Destroy(this.gameObject);
    }

}

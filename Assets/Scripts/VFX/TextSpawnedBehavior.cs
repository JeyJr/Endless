using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSpawnedBehavior : MonoBehaviour
{
    [SerializeField] private float mSpeed;
    [SerializeField] private float delayToDestroy;

    private void Awake()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, - 3);
        mSpeed = Random.Range(2, 4);
        Destroy(this.gameObject, delayToDestroy);
    }
    void Update()
    {
        transform.Translate(Vector3.up * mSpeed * Time.deltaTime);
    }
}

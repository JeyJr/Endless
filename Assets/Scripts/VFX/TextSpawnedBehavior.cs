using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextSpawnedBehavior : MonoBehaviour
{
    [SerializeField] private float mSpeed;
    [SerializeField] private float delayToDestroy;

    private void Awake()
    {
        Destroy(this.gameObject, delayToDestroy);
    }
    void Update()
    {
        transform.Translate(Vector3.up * mSpeed * Time.deltaTime);
    }
}

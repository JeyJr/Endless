using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSpawnedBehavior : MonoBehaviour
{
    float speed = 1f;

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    public void DestruirObj() => Destroy(this.gameObject);
}

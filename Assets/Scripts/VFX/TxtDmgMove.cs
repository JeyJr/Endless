using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TxtDmgMove : MonoBehaviour
{
    float speed = 1f;

    private void Awake()
    {
        Destroy(this.gameObject, 1f);
    }

    
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}

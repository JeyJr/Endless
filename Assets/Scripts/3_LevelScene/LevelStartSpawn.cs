using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartSpawn : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject.FindWithTag("LevelController").GetComponent<LevelController>().startSpawn = true;
            Destroy(this.gameObject);
        }
    }
}

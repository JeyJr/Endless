using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelCanvas canvas = GameObject.FindGameObjectWithTag("MainUI").GetComponent<LevelCanvas>();
            canvas.OpenPanelMoveToLobby();
        }
    }
}

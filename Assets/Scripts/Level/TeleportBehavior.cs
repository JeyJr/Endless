using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBehavior : MonoBehaviour
{
    [Header("Positions")]
    [SerializeField] private Transform newPosition;
    [SerializeField] private GameObject player;

    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        //Player Animation teleport
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(TeleportPlayer());
        }
    }

    IEnumerator TeleportPlayer()
    {
        yield return null;
        player.GetComponent<Transform>().position = newPosition.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBehavior : MonoBehaviour
{
    [Header("Positions")]
    [SerializeField] private Transform newPosition;
    [SerializeField] private GameObject player;
    [SerializeField] private bool teleportToLobby;

    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        //Player Animation teleport
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !teleportToLobby)
        {
            StartCoroutine(TeleportPlayer());
        }
    }

    IEnumerator TeleportPlayer()
    {
        yield return null;
        player.GetComponent<Transform>().position = newPosition.position;
    }


    void TeleportToLobby()
    {
        //Abrir panel usuario confirmar volta para lobby
        //Se sim, salvar gold 
    }
}

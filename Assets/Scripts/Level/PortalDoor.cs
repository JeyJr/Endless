using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDoor : MonoBehaviour
{
    [SerializeField] private GameObject invisibleWall;
    [SerializeField] private LevelController levelController;
    Animator anim;

    public bool openDoor;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("Base Layer.PortalDoor_Idle", 0);
        levelController = GameObject.FindWithTag("LevelController").GetComponent<LevelController>();
    }

    //Called in the last frame on OpeningWall
    public void DestroyDoorAndInvisibleWall()
    {
        Destroy(this.gameObject);
        Destroy(invisibleWall);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && levelController.BossDead)
        {
            anim.Play("Base Layer.PortalDoor_Opening", 0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && levelController.BossDead)
        {
            anim.Play("Base Layer.PortalDoor_Opening", 0);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalDoor : MonoBehaviour
{
    [SerializeField] private GameObject invisibleWall;
    Animator anim;

    public bool openDoor;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("Base Layer.PortalDoor_Idle", 0);
    }

    //Called in the last frame on OpeningWall
    public void DestroyDoorAndInvisibleWall()
    {
        Destroy(this.gameObject);
        Destroy(invisibleWall);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && openDoor)
        {
            anim.Play("Base Layer.PortalDoor_Opening", 0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && openDoor)
        {
            anim.Play("Base Layer.PortalDoor_Opening", 0);
        }
    }


}

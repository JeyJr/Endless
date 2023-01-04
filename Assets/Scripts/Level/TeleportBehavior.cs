using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportBehavior : MonoBehaviour
{
    [SerializeField] private bool goToZone2, goToZone3, goToLobby;

    #region TeleportControl
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelCanvas canvas = GameObject.FindGameObjectWithTag("MainUI").GetComponent<LevelCanvas>();

            canvas.OpenPanelMoveToNextArea();

            if (goToZone2)
                canvas.OpenMsgMoveToNextArea("MOVE TO ZONE 2?");
            else if (goToZone3)
                canvas.OpenMsgMoveToNextArea("MOVE TO ZONE 3?");
            else if (goToLobby)
                canvas.OpenMsgMoveToNextArea("MOVE TO LOBBY?");
        }
    }
    #endregion



}

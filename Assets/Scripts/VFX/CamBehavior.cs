using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CamBehavior : MonoBehaviour
{
    [Header("CamBehavior")]
    public Transform cam;
    Vector3 currentVelocity;

    [SerializeField] private float xCam, yCam, smooth, speed;
    [SerializeField] private bool lobby;

    PlayerController playerController;
    Transform playerPos;

    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerPos = playerController.GetComponent<Transform>();
    }
    private void Update()
    {
        if (playerController.IsMirrored && playerController.IsMoving)
            CamPosition(-xCam);
        else if (!playerController.IsMirrored && playerController.IsMoving)
            CamPosition(xCam);
        else
            CamPosition(xCam - xCam);
    }

    void CamPosition(float x)
    {
        if (SceneManager.GetActiveScene().name != "Lobby")
        {
            Vector3 target = new(playerPos.position.x + x, playerPos.position.y + yCam, playerPos.position.z - 50);
            transform.position = Vector3.SmoothDamp(transform.position, target, ref currentVelocity, smooth, speed);
        }
    }
}

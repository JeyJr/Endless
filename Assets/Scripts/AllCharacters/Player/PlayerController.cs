using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{
    [Header("CamBehavior")]
    public Transform camPos;
    Vector3 currentVelocity;

    [Header("PlayerMovement")]
    public PlayerMove playerInput;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float playerMoveSpeed = 4f;
    public float PlayerMoveSpeed { get => playerMoveSpeed; set => playerMoveSpeed = value; }

    PlayerHand playerHand;
    PlayerStatus playerStatus;
    public Vector2 MoveInput{ get; set; }


    //Y Detect
    [SerializeField] private LayerMask target;
    [SerializeField] private Transform yPosition;
    [SerializeField] private float yTopRange, yTop;



    void Start()
    {
        camPos = Camera.main.GetComponent<Transform>();

        controller = GetComponent<CharacterController>();
        playerInput = new PlayerMove();
        playerInput.Enable();
  
        playerHand = GetComponentInChildren<PlayerHand>();
        playerStatus = GetComponent<PlayerStatus>();
    }

    void Update()
    {
        MoveInput = playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(MoveInput.x, 0f, 0f);
        controller.Move(move * Time.deltaTime * PlayerMoveSpeed);

        if (move.x < 0)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            playerHand.delayBar.GetComponent<Slider>().direction = Slider.Direction.RightToLeft;
            playerStatus.lifeBar.GetComponent<Slider>().direction = Slider.Direction.RightToLeft;
            CamPosition(-3, 0, 10, 0.1f, 100);
            //PlayerAnimMove
        }
        else if (move.x > 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            playerHand.delayBar.GetComponent<Slider>().direction = Slider.Direction.LeftToRight;
            playerStatus.lifeBar.GetComponent<Slider>().direction = Slider.Direction.LeftToRight;

            CamPosition(3, 0, 10, 0.1f, 100);
            //PlayerAnimMove
        }
        else
        {
            CamPosition(0, 0, 10, 0.2f, 10);
            //Stop anim move
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyZone"))
        {
            other.GetComponent<ZoneControl>().StartSpawnEnemys();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("EnemyZone"))
        {
            other.GetComponent<ZoneControl>().PlayerOutEnemyZone();
        }
    }


    #region CamBehavior

    void CamPosition(float x, float y, float z, float smoothT, float speed)
    {
        Vector3 target = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z - z);
        camPos.transform.position = Vector3.SmoothDamp(camPos.transform.position, target, ref currentVelocity, smoothT, speed);
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public PlayerMove playerInput;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float playerSpeed = 2.0f;
    PlayerMeleeAtk meleeAtk;
    PlayerHand playerHand;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = new PlayerMove();
        playerInput.Enable();
        meleeAtk = GetComponentInChildren<PlayerMeleeAtk>();
        playerHand = GetComponentInChildren<PlayerHand>();
    }

    void Update()
    {
        Vector2 moveInput = playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move.x < 0)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            meleeAtk.PlayerMirrored = true;
            playerHand.delayBar.GetComponent<Slider>().direction = Slider.Direction.RightToLeft;
            //PlayerAnimMove
        }
        else if (move.x > 0)
        {
            meleeAtk.PlayerMirrored = false;
            transform.localEulerAngles = new Vector3(0, 0, 0);
            playerHand.delayBar.GetComponent<Slider>().direction = Slider.Direction.LeftToRight;
            //PlayerAnimMove
        }
        else
        {
            //Stop anim move
        }

    }

}

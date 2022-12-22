using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("CamBehavior")]
    public Transform camPos;
    Vector3 currentVelocity;
    [SerializeField] private bool lobby;

    [Header("PlayerMovement")]
    public PlayerMove playerInput;
    [SerializeField] private CharacterController controller;
    [SerializeField] private float playerMoveSpeed = 4f;
    public float PlayerMoveSpeed { get => playerMoveSpeed; set => playerMoveSpeed = value; }
    PlayerHand playerHand;
    PlayerStatus playerStatus;
    public Vector2 MoveInput{ get; set; }

    [Header("SkinOrganization")]
    [SerializeField] private Transform skinHead;

    [Header("FootAnimations")]
    [SerializeField] private Animator rightFootAnim;
    [SerializeField] private Animator leftFootAnim;


    void Start()
    {
        camPos = Camera.main.GetComponent<Transform>();

        controller = GetComponent<CharacterController>();
        playerInput = new PlayerMove();
        playerInput.Enable();
  
        playerHand = GetComponentInChildren<PlayerHand>();
        playerStatus = GetComponent<PlayerStatus>();

        if (GameObject.FindGameObjectWithTag("LevelController") == null)
            lobby = true;
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
            FootAnims("Run");
        }
        else if (move.x > 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            playerHand.delayBar.GetComponent<Slider>().direction = Slider.Direction.LeftToRight;
            playerStatus.lifeBar.GetComponent<Slider>().direction = Slider.Direction.LeftToRight;

            CamPosition(3, 0, 10, 0.1f, 100);
            FootAnims("Run");
        }
        else
        {

            CamPosition(0, 0, 10, 0.2f, 10);
            FootAnims("Idle");
        }

        SkinHeadZPosition();
    }


    void SkinHeadZPosition()
    {
        skinHead.position = new Vector3(skinHead.position.x, skinHead.position.y, -0.05f);
    }
    void FootAnims(string animName)
    {
        rightFootAnim.Play($"Base Layer.RightFoot_{animName}", 0);
        leftFootAnim.Play($"Base Layer.LeftFoot_{animName}", 0);
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
        if (!lobby)
        {
            Vector3 target = new Vector3(transform.position.x + x, transform.position.y + y, transform.position.z - z);
            camPos.transform.position = Vector3.SmoothDamp(camPos.transform.position, target, ref currentVelocity, smoothT, speed);
        }
    }
    #endregion
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("CamBehavior")]
    public Transform cam;
    Vector3 currentVelocity;

    [SerializeField] private float xCam, yCam, smooth, speed;
    [SerializeField] private bool lobby;



    [Header("PlayerMovement")]
    public PlayerMove playerInput;
    [SerializeField] private CharacterController controller;
    private float playerMoveSpeed;
    public float PlayerMoveSpeed { get => playerMoveSpeed; set => playerMoveSpeed = value; }
    PlayerMeleeAtk playerMeleeAtk;
    PlayerStatus playerStatus;
    public Vector2 MoveInput{ get; set; }

    [Header("Skin Hierarchy Organization")]
    [SerializeField] private Transform head;
    [SerializeField] private Transform leftLeg;
    [SerializeField] private Transform rightLeg;
    [SerializeField] private Transform canvas;

    [Header("Animations")]
    [SerializeField] private Animator rightLegAnim;
    [SerializeField] private Animator rightFootAnim;
    [SerializeField] private Animator leftLegAnim;
    [SerializeField] private Animator leftFootAnim;


    void Start()
    {

        GameData gameData = ManagerData.Load();
        
        controller = GetComponent<CharacterController>();
        playerInput = new PlayerMove();
        playerInput.Enable();
  
        playerMeleeAtk = GetComponentInChildren<PlayerMeleeAtk>();
        playerStatus = GetComponent<PlayerStatus>();

        if (GameObject.FindGameObjectWithTag("LevelController") == null)
            lobby = true;

        //Cam behavior
        cam = Camera.main.GetComponent<Transform>();
        //cam.transform.position = new(cam.position.x, 6, cam.position.z);

        if (!lobby && gameData.RangeAtk > cam.GetComponent<Camera>().orthographicSize)
        {
            cam.GetComponent<Camera>().orthographicSize = gameData.RangeAtk;
        }

        UpdatePlayerMoveSpeed(gameData.buffSkillMoveSpeed);

    }

    void Update()
    {
        MoveInput = playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(MoveInput.x, 0f, 0f);
        controller.Move(move * Time.deltaTime * PlayerMoveSpeed);
        

        if (move.x < 0)
        {
            transform.localEulerAngles = new Vector3(0, 180, 0);
            playerMeleeAtk.delayBar.GetComponent<Slider>().direction = Slider.Direction.RightToLeft;
            playerStatus.lifeBar.GetComponent<Slider>().direction = Slider.Direction.RightToLeft;
            CamPosition(-xCam);
            Anims("Run");
        }
        else if (move.x > 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            playerMeleeAtk.delayBar.GetComponent<Slider>().direction = Slider.Direction.LeftToRight;
            playerStatus.lifeBar.GetComponent<Slider>().direction = Slider.Direction.LeftToRight;

            CamPosition(xCam);
            Anims("Run");
        }
        else
        {
            CamPosition(xCam - xCam);
            Anims("Idle");
        }

        ZPosition();
    }


    public void UpdatePlayerMoveSpeed(float buffValue)
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillMoveSpeed = buffValue;

        ManagerData.Save(gameData);

        playerMoveSpeed = gameData.MoveSpeed;
    }

    void ZPosition()
    {
        head.position = new Vector3(head.position.x, head.position.y, -0.05f);
        leftLeg.position = new Vector3(leftLeg.position.x, leftLeg.position.y, 0.02f);
        rightLeg.position = new Vector3(rightLeg.position.x, rightLeg.position.y, 0.02f);
        canvas.position = new Vector3(canvas.position.x, canvas.position.y, -5.5f);
    }
    void Anims(string animName)
    {
        leftLegAnim.Play($"Base Layer.LeftLeg_{animName}", 0);
        rightLegAnim.Play($"Base Layer.RightLeg_{animName}", 0);

        leftFootAnim.Play($"Base Layer.LFoot_{animName}", 0);
        rightFootAnim.Play($"Base Layer.RFoot_{animName}", 0);
    }
    
   
    
    #region CamBehavior

    void CamPosition(float x)
    {
        if (SceneManager.GetActiveScene().name != "Lobby")
        {
            Vector3 target = new(transform.position.x + x, transform.position.y + yCam, transform.position.z - 10);
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, target, ref currentVelocity, smooth, speed);
        }
    }
    #endregion
}

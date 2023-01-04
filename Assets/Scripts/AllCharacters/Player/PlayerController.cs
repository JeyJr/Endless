using UnityEditor.SearchService;
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
    PlayerHand playerHand;
    PlayerStatus playerStatus;
    public Vector2 MoveInput{ get; set; }

    [Header("SkinOrganization")]
    [SerializeField] private Transform skinHead;

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
  
        playerHand = GetComponentInChildren<PlayerHand>();
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
            playerHand.delayBar.GetComponent<Slider>().direction = Slider.Direction.RightToLeft;
            playerStatus.lifeBar.GetComponent<Slider>().direction = Slider.Direction.RightToLeft;
            CamPosition(-xCam);
            Anims("Run");
        }
        else if (move.x > 0)
        {
            transform.localEulerAngles = new Vector3(0, 0, 0);
            playerHand.delayBar.GetComponent<Slider>().direction = Slider.Direction.LeftToRight;
            playerStatus.lifeBar.GetComponent<Slider>().direction = Slider.Direction.LeftToRight;

            CamPosition(xCam);
            Anims("Run");
        }
        else
        {
            CamPosition(xCam - xCam);
            Anims("Idle");
        }

        SkinHeadZPosition();
    }


    public void UpdatePlayerMoveSpeed(float buffValue)
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillMoveSpeed = buffValue;

        ManagerData.Save(gameData);

        playerMoveSpeed = gameData.MoveSpeed;
    }

    void SkinHeadZPosition()
    {
        skinHead.position = new Vector3(skinHead.position.x, skinHead.position.y, -0.05f);
    }
    void Anims(string animName)
    {
        leftLegAnim.Play($"Base Layer.LeftLeg_{animName}", 0);
        rightLegAnim.Play($"Base Layer.RightLeg_{animName}", 0);

        leftFootAnim.Play($"Base Layer.LFoot_{animName}", 0);
        rightFootAnim.Play($"Base Layer.RFoot_{animName}", 0);
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

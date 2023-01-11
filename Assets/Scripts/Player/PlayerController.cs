using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("PlayerMovement")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private PlayerMove playerInput;
    [SerializeField] private float playerMoveSpeed;
    [SerializeField] private bool isMirrored, isMoving;

    Vector2 MoveInput{ get; set; }
    public float PlayerMoveSpeed { get => playerMoveSpeed; set => playerMoveSpeed = value; }
    public bool IsMirrored { get => isMirrored; private set => isMirrored = value; }
    public bool IsMoving { get => isMoving; private set => isMoving = value; }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = new PlayerMove();
        playerInput.Enable();
        UpdatePlayerMoveSpeed(ManagerData.Load().buffSkillMoveSpeed);
    }
    void Update()
    {
        MoveInput = playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 move = new Vector3(MoveInput.x, 0f, 0f);
        controller.Move(move * Time.deltaTime * PlayerMoveSpeed);
        
        if (move.x < 0)
            SetIsMovingAndMirrored(true, true, 180);
        else if (move.x > 0)
            SetIsMovingAndMirrored(true, false, 0);
        else
            SetIsMoving(false);
    }

    public void UpdatePlayerMoveSpeed(float buffValue)
    {
        GameData gameData = ManagerData.Load();
        gameData.buffSkillMoveSpeed = buffValue;

        ManagerData.Save(gameData);

        playerMoveSpeed = gameData.MoveSpeed;
    }
    void SetIsMovingAndMirrored(bool _isMoving, bool _isMorrored, float y)
    {
        IsMoving = _isMoving;
        IsMirrored = _isMorrored;
        transform.localEulerAngles = new Vector3(0, y, 0);
    }
    void SetIsMoving(bool isMoving)
    {
        IsMoving = isMoving;
    }
}

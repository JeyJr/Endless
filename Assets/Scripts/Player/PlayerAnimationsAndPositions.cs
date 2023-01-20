using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerAnimationsAndPositions : MonoBehaviour
{
    [Header("Player Refs")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerStatus playerStatus;

    [Header("OBJ HEAD")]
    [SerializeField] private GameObject head;

    [Header("OBJ ARMs")]
    [SerializeField] private GameObject rightArm;
    [SerializeField] private GameObject leftArm;

    [Header("OBJ LEGs")]
    [SerializeField] private GameObject rightLeg;
    [SerializeField] private GameObject rightFoot;
    [SerializeField] private GameObject leftLeg;
    [SerializeField] private GameObject leftFoot;

    [Header("Anims State Control")]
    private bool isAttacking;
    public bool IsAttacking { get => isAttacking; set => isAttacking = value; }


    [Header("Position Control")]
    [SerializeField] private float xRightArm;
    [SerializeField] private float zRightArm;

    [SerializeField] private float xLeftArm;
    [SerializeField] private float zLeftArm;

    [SerializeField] private float zHead;


    //Arms
    Sprite left, right;


    private void Start()
    {
        //SKIN and WEAPONS
        PlayerSkinManager pSkin = GetComponentInChildren<PlayerSkinManager>();
        pSkin.EquipWeapon();
        pSkin.EquipArmor();
        pSkin.EquipHelmet();
        pSkin.EquipArms();
    }

    private void Update()
    {
        if (!isAttacking)
            AnimationsToMove();

        MembersPosition();
        ChangeArmsSprites();
    }

    void MembersPosition()
    {
        rightArm.GetComponent<Transform>().position = new Vector3(
            transform.position.x + xRightArm,
            rightArm.GetComponent<Transform>().position.y,
            playerController.IsMirrored ? zRightArm : -zRightArm);

        leftArm.GetComponent<Transform>().position = new Vector3(
            transform.position.x + xLeftArm,
            leftArm.GetComponent<Transform>().position.y,
            playerController.IsMirrored ? zLeftArm : -zLeftArm);

        head.GetComponent<Transform>().position = new Vector3(
            transform.position.x,
            head.GetComponent<Transform>().position.y,
            zHead);
    }
    void ChangeArmsSprites()
    {
        if (playerController.IsMirrored)
        {
            leftArm.GetComponent<SpriteRenderer>().sprite = right;
            rightArm.GetComponent<SpriteRenderer>().sprite = left;
        }
        else
        {
            leftArm.GetComponent<SpriteRenderer>().sprite = left;
            rightArm.GetComponent<SpriteRenderer>().sprite = right;
        }
    }

    public void SetArmSprites(Sprite left, Sprite right)
    {
        this.left = left;
        this.right = right;
    }

    void AnimationsToMove()
    {
        string animName;
        if (playerController.IsMoving)
            animName = "Run";
        else
            animName = "Idle";

        //LEG
        leftLeg.GetComponent<Animator>().Play($"Base Layer.LeftLeg_{animName}", 0);
        rightLeg.GetComponent<Animator>().Play($"Base Layer.RightLeg_{animName}", 0);

        //FOOT
        leftFoot.GetComponent<Animator>().Play($"Base Layer.LFoot_{animName}", 0);
        rightFoot.GetComponent<Animator>().Play($"Base Layer.RFoot_{animName}", 0);

        //ARM
        rightArm.GetComponent<Animator>().Play($"Base Layer.RightArm_Idle", 0);
    }
    public void PlayAnimAtk()
    {
        if(playerStatus.ImAlive)
            StartCoroutine(AnimationToAttack());
    }
    IEnumerator AnimationToAttack()
    {
        rightArm.GetComponent<Animator>().Play($"Base Layer.RightArm_Atk", 0);
        yield return null;
    }

}

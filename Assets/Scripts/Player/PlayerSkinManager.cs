using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinManager : MonoBehaviour
{
    [SerializeField] private Equips equips;
    [SerializeField] private Transform mainPos;


    [Header("Main")]
    [SerializeField] private SpriteRenderer weapon;
    [SerializeField] private SpriteRenderer head;
    [SerializeField] private SpriteRenderer body;

    [Header("Arms")]
    [SerializeField] private SpriteRenderer rightArm;
    [SerializeField] private SpriteRenderer leftArm;

    [Header("Legs")]
    [SerializeField] private SpriteRenderer rightLeg;
    [SerializeField] private SpriteRenderer leftLeg;

    [Header("Center")]
    [SerializeField] private SpriteRenderer rightLegCenter;
    [SerializeField] private SpriteRenderer leftLegCenter;

    [Header("Foot")]
    [SerializeField] private SpriteRenderer rightFoot;
    [SerializeField] private SpriteRenderer leftFoot;

    #region Equip
    public void EquipWeapon()
    {
        weapon.sprite = equips.GetWeaponToEquip().sprite;
    }
    public void EquipArmor()
    {
        Sprite[] armor = equips.GetArmorToEquip();
        
        body.sprite = armor[0];

        rightLegCenter.sprite = armor[1];
        leftLegCenter.sprite = armor[1];
        
        rightLeg.sprite = armor[2];
        leftLeg.sprite = armor[2];

        leftFoot.sprite = armor[3];
        rightFoot.sprite = armor[3];
    }
    public void EquipHelmet()
    {
        head.sprite = equips.GetHelmetToEquip();
    }
    public void EquipArms()
    {
        Sprite[] arms = equips.GetArmsToEquip();
        rightArm.sprite = arms[0];
        leftArm.sprite = arms[1];

        GetComponentInParent<PlayerAnimationsAndPositions>().SetArmSprites(leftArm.sprite, rightArm.sprite);
    }
    #endregion
}

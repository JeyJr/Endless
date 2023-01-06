using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinManager : MonoBehaviour
{
    public Equips equips;

    public SpriteRenderer weapon;
    public SpriteRenderer head;
    public SpriteRenderer body;
    public SpriteRenderer rightArm, rightHand, leftArm, leftHand;
    public SpriteRenderer rightLeg, rightFoot, leftLeg, leftFoot;
    public SpriteRenderer rightLegCenter, leftLegCenter;

    private void Awake()
    {
        
    }

    public void EquipWeapon()
    {
        weapon.sprite = equips.GetWeaponToEquip().sprite;
    }

    public void EquipArmor()
    {
        Sprite[] sprites = equips.GetArmorToEquip();
        
        body.sprite = sprites[0];

        rightArm.sprite = sprites[1];
        leftArm.sprite = sprites[1];

        rightHand.sprite = sprites[2];
        leftHand.sprite = sprites[2];

        rightLegCenter.sprite = sprites[3];
        leftLegCenter.sprite = sprites[3];
        
        rightLeg.sprite = sprites[4];
        leftLeg.sprite = sprites[4];

        leftFoot.sprite = sprites[5];
        rightFoot.sprite = sprites[5];

    }

    public void EquipHelmet()
    {
        head.sprite = equips.GetHelmetToEquip();
    }
}

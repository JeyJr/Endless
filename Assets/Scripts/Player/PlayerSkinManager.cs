using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinManager : MonoBehaviour
{
    public Equips equips;

    [Header("Main")]
    public SpriteRenderer weapon;
    public SpriteRenderer head;
    public SpriteRenderer body;

    [Header("Arms")]
    public SpriteRenderer rightArm;
    public SpriteRenderer leftArm;

    [Header("Legs")]
    public SpriteRenderer rightLeg;
    public SpriteRenderer leftLeg;

    [Header("Center")]
    public SpriteRenderer rightLegCenter;
    public SpriteRenderer leftLegCenter;

    [Header("Foot")]
    public SpriteRenderer rightFoot;
    public SpriteRenderer leftFoot;


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
    }
}

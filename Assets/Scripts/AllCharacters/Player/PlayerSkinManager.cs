using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinManager : MonoBehaviour
{
    public Equips equips;

    public SpriteRenderer weapon;
    public SpriteRenderer head;
    public SpriteRenderer body;
    public SpriteRenderer leftArm, rightArm;
    public SpriteRenderer leftFoot, rightFoot;

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
        leftArm.sprite = sprites[1];
        rightArm.sprite = sprites[1];
        leftFoot.sprite = sprites[2];
        rightFoot.sprite = sprites[2];
    }

    public void EquipHelmet()
    {
    }
}

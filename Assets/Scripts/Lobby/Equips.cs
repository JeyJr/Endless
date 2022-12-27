using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Equips : MonoBehaviour
{
    public List<GameObject> weapons;

    public GameObject standardPlayerArmor;
    public List<GameObject> armor;

    public List<GameObject> helmet;

    public SpriteRenderer GetWeaponToEquip()
    {
        GameData gameData = ManagerData.Load();

        foreach (var weapon in weapons)
        {
            if (weapon.GetComponent<WeaponAttributes>().WeaponID == gameData.equipedWeaponId)
            {
                var w = weapon.GetComponent<WeaponAttributes>();
                gameData.weaponDmg = w.WeaponDmg;
                gameData.weaponDefense = w.WeaponDefense;
                gameData.weaponLife = w.WeaponLife;
                gameData.weaponSpeedAtk = w.WeaponSpeedAtk;
                gameData.weaponCritical = w.WeaponCritical;
                gameData.weaponRangeAtk = w.WeaponRangeAtk;
                gameData.weaponMoveSpeed = w.WeaponMoveSpeed;
                gameData.equipedWeaponId = w.WeaponID;

                ManagerData.Save(gameData);

                return w.GetComponent<SpriteRenderer>();
            }
        }

        return null;
    }
    public Sprite[] GetArmorToEquip()
    {
        GameData gameData = ManagerData.Load();

        foreach (var armor in armor)
        {
            if (armor.GetComponent<ArmorAttributes>().ArmorID == gameData.equipedArmorId)
            {
                var a = armor.GetComponent<ArmorAttributes>();

                gameData.armorDmg = a.ArmorDmg;
                gameData.armorDefense = a.ArmorDefense;
                gameData.armorLife = a.ArmorLife;
                gameData.armorSpeedAtk = a.ArmorSpeedAtk;
                gameData.armorCritical = a.ArmorCritical;
                gameData.armorRangeAtk = a.ArmorRangeAtk;
                gameData.armorMoveSpeed = a.ArmorMoveSpeed;
                gameData.equipedArmorId = a.ArmorID;

                ManagerData.Save(gameData);

                Sprite[] sprites = new Sprite[3];
                sprites[0] = a.ImgArmorBody;
                sprites[1] = a.ImgArmorArm;
                sprites[2] = a.ImgArmorFoot;
                return sprites;
            }
        }

        Sprite[] standardSprites = new Sprite[3];
        standardSprites[0] = standardPlayerArmor.GetComponent<ArmorAttributes>().ImgArmorBody;
        standardSprites[1] = standardPlayerArmor.GetComponent<ArmorAttributes>().ImgArmorArm;
        standardSprites[2] = standardPlayerArmor.GetComponent<ArmorAttributes>().ImgArmorFoot;

        return standardSprites;
    }
    public Sprite GetHelmetToEquip()
    {
        GameData gameData = ManagerData.Load();

        foreach (var helmet in helmet)
        {
            if (helmet.GetComponent<HelmetAttributes>().HelmetID == gameData.equipedHelmetId)
            {
                var h = helmet.GetComponent<HelmetAttributes>();
                gameData.helmetDmg = h.HelmetDmg;
                gameData.helmetDefense = h.HelmetDefense;
                gameData.helmetLife = h.HelmetLife;
                gameData.helmetSpeedAtk = h.HelmetSpeedAtk;
                gameData.helmetCritical = h.HelmetCritical;
                gameData.helmetRangeAtk = h.HelmetRangeAtk;
                gameData.helmetMoveSpeed = h.HelmetMoveSpeed;
                gameData.equipedHelmetId = h.HelmetID;

                ManagerData.Save(gameData);

                return h.ImgHelmet;
            }
        }
        return helmet[0].GetComponent<HelmetAttributes>().ImgHelmet;
    }
}

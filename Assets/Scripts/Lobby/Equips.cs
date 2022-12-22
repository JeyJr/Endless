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
                gameData.weaponDmg = w.WeaponAtk;
                gameData.weaponSpeedAtk = w.WeaponSpeedAtk;
                gameData.weaponRangeAtk = w.WeaponAtkRange;
                gameData.equipedWeaponId = w.WeaponID;
                gameData.weaponLife = w.WeaponLife;
                gameData.weaponDefense = w.WeaponDefense;
                gameData.weaponCritical = w.WeaponCritical;

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
                gameData.equipedArmorId = a.ArmorID;

                gameData.armorDmg = a.ArmorAtk;
                gameData.armorRangeAtk = a.ArmorAtkRange;
                gameData.armorLife = a.ArmorLife;
                gameData.armorDefense = a.ArmorDefense;
                gameData.armorCritical = a.ArmorCritical;

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
                gameData.equipedHelmetId = h.HelmetID;
                gameData.helmetDmg = h.HelmetAtk;
                gameData.helmetLife = h.HelmetLife;
                gameData.helmetDefense = h.HelmetDefense;
                gameData.helmetCritical = h.HelmetCritical;

                ManagerData.Save(gameData);

                return h.ImgHelmet;
            }
        }

        return helmet[0].GetComponent<HelmetAttributes>().ImgHelmet;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Equips : MonoBehaviour
{
    public List<GameObject> weapons;

    public List<GameObject> armor;

    public List<GameObject> helmet;

    public List<GameObject> arms;

    [SerializeField] private bool lobby;

    private void OnEnable()
    {
        CheckEquipID(ManagerData.Load());

        if (!lobby)
            Destroy(this.gameObject, 3f);
    }
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
                gameData.weaponAtkSpeed = w.WeaponAtkSpeed;
                gameData.weaponCritical = w.WeaponCritical;
                gameData.weaponRangeAtk = w.WeaponRangeAtk;
                gameData.weaponMoveSpeed = w.WeaponMoveSpeed;
                gameData.equipedWeaponId = w.WeaponID;

                ManagerData.Save(gameData);

                return w.GetComponent<SpriteRenderer>();
            }
        }

        throw new ArgumentException("Erro ID arma!");
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
                gameData.armorAtkSpeed = a.ArmorAtkSpeed;
                gameData.armorCritical = a.ArmorCritical;
                gameData.armorRangeAtk = a.ArmorRangeAtk;
                gameData.armorMoveSpeed = a.ArmorMoveSpeed;
                gameData.equipedArmorId = a.ArmorID;

                ManagerData.Save(gameData);

                Sprite[] sprites = new Sprite[4];
                sprites[0] = a.ImgBody;
                sprites[1] = a.ImgCenter;
                sprites[2] = a.ImgLeg;
                sprites[3] = a.ImgFoot;
                return sprites;
            }
        }
        throw new ArgumentException("Erro ID armadura! Armadura não encontrada no prefab EQUIPs");
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
                gameData.helmetAtkSpeed = h.HelmetAtkSpeed;
                gameData.helmetCritical = h.HelmetCritical;
                gameData.helmetRangeAtk = h.HelmetRangeAtk;
                gameData.helmetMoveSpeed = h.HelmetMoveSpeed;
                gameData.equipedHelmetId = h.HelmetID;

                ManagerData.Save(gameData);

                return h.ImgHelmet;
            }
        }

        throw new ArgumentException("Erro ID elmo!");
    }

    public Sprite[] GetArmsToEquip()
    {
        GameData gameData = ManagerData.Load();

        foreach (var arm in arms)
        {
            if (arm.GetComponent<ArmAttributes>().ArmID == gameData.equipedArmId)
            {
                var a = arm.GetComponent<ArmAttributes>();

                gameData.armDmg = a.ArmDmg;
                gameData.armDefense = a.ArmDefense;
                gameData.armLife = a.ArmLife;
                gameData.armAtkSpeed = a.ArmAtkSpeed;
                gameData.armCritical = a.ArmCritical;
                gameData.armRangeAtk = a.ArmRangeAtk;
                gameData.armMoveSpeed = a.ArmMoveSpeed;
                gameData.equipedArmId = a.ArmID;

                ManagerData.Save(gameData);

                Sprite[] sprites = new Sprite[2];
                sprites[0] = a.ImgRightArm;
                sprites[1] = a.ImgLeftArm;
                return sprites;
            }
        }
        throw new ArgumentException("Erro ID braço!");
    }

    void CheckEquipID(GameData gameData)
    {
        if(gameData.equipedWeaponId == 0)
        {
            gameData.equipedWeaponId = 1000;
            ManagerData.Save(gameData);
        }

        if (gameData.equipedArmorId == 0)
        {
            gameData.equipedArmorId = 1000;
            ManagerData.Save(gameData);
        }

        if(gameData.equipedHelmetId == 0)
        {
            gameData.equipedHelmetId = 1000;
            ManagerData.Save(gameData);
        }

        if (gameData.equipedArmId == 0)
        {
            gameData.equipedArmId = 1000;
            ManagerData.Save(gameData);
        }
    }
}

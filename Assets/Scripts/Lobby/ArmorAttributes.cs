using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorAttributes : MonoBehaviour
{
    [Header("SET EQUIPS IMG ")]
    [SerializeField] private Sprite imgArmorIcon;
    [SerializeField] private Sprite imgBody;
    [SerializeField] private Sprite imgLeg, imgCenter, imgFoot;
    public Sprite ImgArmorIcon { get => imgArmorIcon; set => imgArmorIcon = value; }
    public Sprite ImgBody { get => imgBody; set => imgBody = value; }
    public Sprite ImgLeg { get => imgLeg; set => imgLeg = value; }
    public Sprite ImgCenter { get => imgCenter; set => imgCenter = value; }
    public Sprite ImgFoot { get => imgFoot; set => imgFoot = value; }

    [Header("SET ARMOR DATA CONTROL")]
    [SerializeField] private string armorName;
    [SerializeField] private int armorID;
    [SerializeField] private float goldCost;
    public string ArmorName { get => armorName; }
    public int ArmorID { get => armorID; }
    public float GoldCost { get => goldCost; }


    [Header("ATTRIBUTES")]
    [SerializeField] private float armorDmg;
    [SerializeField] private float armorDefense;
    [SerializeField] private float armorLife;
    [SerializeField] private float armorAtkSpeed;
    [SerializeField] private float armorCritical;
    [SerializeField] private float armorRangeAtk;
    [SerializeField] private float armorMoveSpeed;


    public float ArmorDmg { get => armorDmg; }
    public float ArmorDefense { get => armorDefense; }
    public float ArmorLife { get => armorLife; }
    public float ArmorAtkSpeed { get => armorAtkSpeed; }
    public float ArmorCritical { get => armorCritical; }
    public float ArmorRangeAtk { get => armorRangeAtk; }
    public float ArmorMoveSpeed { get => armorMoveSpeed; }



    

}

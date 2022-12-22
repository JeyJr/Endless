using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorAttributes : MonoBehaviour
{

    [SerializeField] private float armorAtk;
    [SerializeField] private float armorAtkRange;
    [SerializeField] private float armorDefense;
    [SerializeField] private float armorLife;
    [SerializeField] private float armorCritical;


    public float ArmorAtk { get => armorAtk; }
    public float ArmorAtkRange { get => armorAtkRange; }
    public float ArmorDefense { get => armorDefense; }
    public float ArmorLife { get => armorLife; }
    public float ArmorCritical { get => armorCritical; }


    //UI
    [SerializeField] private Sprite imgSetIcon;

    [SerializeField] private Sprite imgArmorBody;
    [SerializeField] private Sprite imgArmorArm;
    [SerializeField] private Sprite imgArmorFoot;

    [SerializeField] private string armorName;

    //UI
    public Sprite ImgSetIcon { get => imgSetIcon; }
    public Sprite ImgArmorBody { get => imgArmorBody; }
    public Sprite ImgArmorArm { get => imgArmorArm; }
    public Sprite ImgArmorFoot { get => imgArmorFoot; }


    public string ArmorName { get => armorName; }


    //Buy
    [SerializeField] private int armorID;
    [SerializeField] private float goldCost;

    public int ArmorID { get => armorID; }
    public float GoldCost { get => goldCost; }
}

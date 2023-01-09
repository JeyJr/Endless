using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAttributes : MonoBehaviour
{
    [Header("SET EQUIPS IMG ")]
    [SerializeField] private Sprite imgArmIcon;
    [SerializeField] private Sprite imgRightArm;
    [SerializeField] private Sprite imgLeftArm;
    public Sprite ImgArmIcon { get => imgArmIcon; set => imgArmIcon = value; }
    public Sprite ImgRightArm { get => imgRightArm; set => imgRightArm = value; }
    public Sprite ImgLeftArm { get => imgLeftArm; set => imgLeftArm = value; }


    [Header("SET ARM DATA CONTROL")]
    [SerializeField] private string armName;
    [SerializeField] private int armID;
    [SerializeField] private float goldCost;
    public string ArmName { get => armName; }
    public int ArmID { get => armID; }
    public float GoldCost { get => goldCost; }


    [Header("ATTRIBUTES")]
    [SerializeField] private float armDmg;
    [SerializeField] private float armDefense;
    [SerializeField] private float armLife;
    [SerializeField] private float armAtkSpeed;
    [SerializeField] private float armCritical;
    [SerializeField] private float armRangeAtk;
    [SerializeField] private float armMoveSpeed;


    public float ArmDmg { get => armDmg; }
    public float ArmDefense { get => armDefense; }
    public float ArmLife { get => armLife; }
    public float ArmAtkSpeed { get => armAtkSpeed; }
    public float ArmCritical { get => armCritical; }
    public float ArmRangeAtk { get => armRangeAtk; }
    public float ArmMoveSpeed { get => armMoveSpeed; }
}

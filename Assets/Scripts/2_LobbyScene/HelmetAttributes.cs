using UnityEngine;

public class HelmetAttributes : MonoBehaviour
{

    [Header("SET EQUIPS IMG ")]
    [SerializeField] private Sprite imgHelmetIcon;
    [SerializeField] private Sprite imgHelmet;
    [SerializeField] private string helmetName;
    public Sprite ImgHelmetIcon { get => imgHelmetIcon; }
    public Sprite ImgHelmet { get => imgHelmet; }
    public string HelmetName { get => helmetName; }


    [Header("SET ARMOR DATA CONTROL")]
    [SerializeField] private int helmetID;
    [SerializeField] private float goldCost;
    public int HelmetID { get => helmetID; }
    public float GoldCost { get => goldCost; }


    [Header("ATTRIBUTES")]
    [SerializeField] private float helmetDmg;       
    [SerializeField] private float helmetDefense;   
    [SerializeField] private float helmetLife;      
 
    [SerializeField] private float helmetCritical; 
    [SerializeField] private float helmetRangeAtk;
    [SerializeField] private float helmetMoveSpeed;
    public float HelmetDmg { get => helmetDmg; }
    public float HelmetDefense { get => helmetDefense; }
    public float HelmetLife { get => helmetLife; }
    public float HelmetCritical { get => helmetCritical; }
    public float HelmetRangeAtk { get => helmetRangeAtk; }
    public float HelmetMoveSpeed { get => helmetMoveSpeed; }
}

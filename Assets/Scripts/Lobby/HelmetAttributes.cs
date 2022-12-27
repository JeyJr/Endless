using UnityEngine;

public class HelmetAttributes : MonoBehaviour
{
    [SerializeField] private float helmetDmg;
    [SerializeField] private float helmetDefense;
    [SerializeField] private float helmetLife;
    [SerializeField] private float helmetSpeedAtk;
    [SerializeField] private float helmetCritical;
    [SerializeField] private float helmetRangeAtk;
    [SerializeField] private float helmetMoveSpeed;


    public float HelmetDmg { get => helmetDmg; }
    public float HelmetDefense { get => helmetDefense; }
    public float HelmetLife { get => helmetLife; }
    public float HelmetSpeedAtk { get => helmetSpeedAtk; }
    public float HelmetCritical { get => helmetCritical; }
    public float HelmetRangeAtk { get => helmetRangeAtk; }
    public float HelmetMoveSpeed { get => helmetMoveSpeed; }


    //UI
    [SerializeField] private Sprite imgSetIcon;

    [SerializeField] private Sprite imgHelmet;

    [SerializeField] private string helmetName;

    //UI
    public Sprite ImgSetIcon { get => imgSetIcon; }
    public Sprite ImgHelmet { get => imgHelmet; }


    public string HelmetName { get => helmetName; }


    //Buy
    [SerializeField] private int helmetID;
    [SerializeField] private float goldCost;

    public int HelmetID { get => helmetID; }
    public float GoldCost { get => goldCost; }
}

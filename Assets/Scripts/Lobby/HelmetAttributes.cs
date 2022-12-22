using UnityEngine;

public class HelmetAttributes : MonoBehaviour
{
    [SerializeField] private float helmetAtk;
    [SerializeField] private float helmetDefense;
    [SerializeField] private float helmetLife;
    [SerializeField] private float helmetCritical;


    public float HelmetAtk { get => helmetAtk; }
    public float HelmetDefense { get => helmetDefense; }
    public float HelmetLife { get => helmetLife; }
    public float HelmetCritical { get => helmetCritical; }


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

using UnityEngine;
using UnityEngine.UI;

public class WeaponAttributes : MonoBehaviour
{

    [SerializeField] private float weaponAtk;
    [SerializeField] private float weaponSpeedAtk;
    [SerializeField] private float weaponAtkRange;
    [SerializeField] private float weaponDefense;
    [SerializeField] private float weaponLife;
    [SerializeField] private float weaponCritical;


    public float WeaponAtk{ get => weaponAtk; }
    public float WeaponSpeedAtk { get => weaponSpeedAtk; }
    public float WeaponAtkRange { get => weaponAtkRange; }
    public float WeaponDefense { get => weaponDefense; }
    public float WeaponLife { get => weaponLife; }
    public float WeaponCritical { get => weaponCritical; }


    //UI
    [SerializeField] private Sprite imgWeapon;
    [SerializeField] private string weaponName;

    //UI
    public Sprite ImgWeapon { get => imgWeapon;}
    public string WeaponName { get => weaponName; }


    //Buy
    [SerializeField] private int weaponID;
    [SerializeField] private float goldCost;

    public int WeaponID { get => weaponID; }
    public float GoldCost { get => goldCost; }

}

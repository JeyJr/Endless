using UnityEngine;
using UnityEngine.UI;

public class WeaponAttributes : MonoBehaviour
{
    [Header("WEAPON UI ELEMENTS")]
    [SerializeField] private Sprite imgWeapon;
    [SerializeField] private string weaponName;
    public Sprite ImgWeapon { get => imgWeapon; }
    public string WeaponName { get => weaponName; }

    [Space(5)]
    [Header("WEAPON DATA CONTROL")]
    [SerializeField] private int weaponID;
    [SerializeField] private float goldCost;
    public int WeaponID { get => weaponID; }
    public float GoldCost { get => goldCost; }


    [Space(5)]
    [Header("WEAPON ATTRIBUTES")]
    [SerializeField] private float weaponDmg;
    [SerializeField] private float weaponDefense;
    [SerializeField] private float weaponLife;
    [SerializeField] private float weaponAtkSpeed;
    [SerializeField] private float weaponCritical;
    [SerializeField] private float weaponRangeAtk;
    [SerializeField] private float weaponMoveSpeed;

    public float WeaponDmg{ get => weaponDmg; }
    public float WeaponAtkSpeed { get => weaponAtkSpeed; }
    public float WeaponDefense { get => weaponDefense; }
    public float WeaponLife { get => weaponLife; }
    public float WeaponCritical { get => weaponCritical; }
    public float WeaponRangeAtk { get => weaponRangeAtk; }
    public float WeaponMoveSpeed { get => weaponMoveSpeed; }






}

using UnityEngine;
using UnityEngine.UI;

public class WeaponAttributes : MonoBehaviour
{

    [SerializeField] private float weaponAtk;
    [SerializeField] private float weaponSpeedAtk;
    private int weaponIndex;
    public float WeaponAtk{ get => weaponAtk; }
    public float WeaponSpeedAtk { get => weaponSpeedAtk; }
    public int WeaponIndex { get => weaponIndex; set => weaponIndex = value; }


    //UI
    [SerializeField] private Sprite imgWeapon;
    [SerializeField] private string weaponName;

    //UI
    public Sprite ImgWeapon { get => imgWeapon;}
    public string WeaponName { get => weaponName; }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttributes : MonoBehaviour
{
    [SerializeField] private float weaponAtk;
    [SerializeField] private float weaponSpeedAtk;
    private int weaponIndex;

    public float WeaponAtk{ get => weaponAtk; }
    public float WeaponSpeedAtk { get => weaponSpeedAtk; }
    public int WeaponIndex { get => weaponIndex; set => weaponIndex = value; }
}

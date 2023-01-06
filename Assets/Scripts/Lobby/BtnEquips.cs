using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BtnEquips : MonoBehaviour
{
    [SerializeField] private GameObject equip;
    public GameObject  Equip { get => equip; }

    [SerializeField] private bool weapon, armor, helmet;

    private void Start()
    {
        if (weapon)
            GetComponent<Image>().sprite = equip.GetComponent<WeaponAttributes>().ImgWeaponIcon;

        if (armor)
            GetComponent<Image>().sprite = equip.GetComponent<ArmorAttributes>().ImgArmorIcon;

        if (armor)
            GetComponent<Image>().sprite = equip.GetComponent<HelmetAttributes>().ImgHelmetIcon;
    }
}

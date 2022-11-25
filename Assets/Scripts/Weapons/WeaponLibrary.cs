using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLibrary : MonoBehaviour
{
    public List<GameObject> weapons;
    GameObject player;

    private void Start()
    {
        StartCoroutine(EquipWeapon());
    }

    IEnumerator EquipWeapon()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].GetComponent<WeaponAttributes>().WeaponIndex = i;
        }

        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponentInChildren<PlayerHand>().EquipWeapon(weapons[WeaponData.GetWeaponID()]);

        yield return null;
    }

    public void BtnWeaponToEquip(WeaponAttributes weaponAttributes)
    {
        foreach (var item in weapons)
        {
            if (item.GetComponent<WeaponAttributes>().WeaponIndex == weaponAttributes.WeaponIndex)
                player.GetComponentInChildren<PlayerHand>().EquipWeapon(item);
        }
    }
}

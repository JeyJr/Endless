using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponLibrary : MonoBehaviour
{
    public List<GameObject> weapons;
    GameObject player;

    private void Start()
    {
        StartCoroutine(EquipWeapon());


        if (WeaponData.GetFirstTimeWeapon())
            StartCoroutine(FirstTime());
    }

    IEnumerator FirstTime()
    {
        WeaponData.SetWeaponPurchased(weapons[0].GetComponent<WeaponAttributes>().WeaponID, 1);

        for (int i = 0; i < weapons.Count; i++)
        {
            //0: not - 1: yes
            WeaponData.SetWeaponPurchased(weapons[i].GetComponent<WeaponAttributes>().WeaponID, 0);

        }
        WeaponData.SetFirstTime();
        yield return null;
    }




    IEnumerator EquipWeapon()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].GetComponent<WeaponAttributes>().WeaponIndex = i;
        }

        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponentInChildren<PlayerHand>().EquipWeapon(weapons[WeaponData.GetWeaponIndex()]);

        yield return null;
    }

    public void BtnWeaponToEquip(int index)
    {
        player.GetComponentInChildren<PlayerHand>().EquipWeapon(weapons[index]);
    }
}

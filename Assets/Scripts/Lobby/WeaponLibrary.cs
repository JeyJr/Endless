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
    }

    IEnumerator EquipWeapon()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GameData gameData = ManagerData.Load();
        BtnWeaponToEquip(gameData.equipedWeaponId);

        yield return null;
    }

    public void BtnWeaponToEquip(int id)
    {
        foreach(var weapon in weapons)
        {
            if(weapon.GetComponent<WeaponAttributes>().WeaponID == id)
                player.GetComponentInChildren<PlayerHand>().EquipWeapon(weapon);
        }
    }
}

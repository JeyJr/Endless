using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHand : MonoBehaviour
{
    public Slider delayBar;
    public Animator anim;
    [SerializeField] private string[] animsName;
    bool isAtk;


    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isAtk)
        {
            SetIsAtk();
            StartCoroutine(StartDelay());
        }
    }

    #region EquipWeapon
    public void EquipWeapon(GameObject weapon)
    {
        GetComponent<SpriteRenderer>().sprite = weapon.GetComponent<SpriteRenderer>().sprite;

        WeaponAttributes weaponAttributes = weapon.GetComponent<WeaponAttributes>();

        WeaponData.SetWeaponDMG(weaponAttributes.WeaponAtk);
        WeaponData.SetWeaponAtkSpeed(weaponAttributes.WeaponSpeedAtk);
        WeaponData.SetWeaponIndex(weaponAttributes.WeaponIndex);
        WeaponData.SetWeaponAtkRange(weaponAttributes.WeaponAtkRange);
    }

    #endregion
    #region ATK
    IEnumerator StartDelay()
    {
        float atkSpeed = GameData.GetAtkSpeed();
        delayBar.maxValue = atkSpeed;
        delayBar.value = 0;

        anim.Play($"Base Layer.{animsName[0]}", 0); //Idle

        while (delayBar.value < delayBar.maxValue)
        {
            yield return new WaitForSeconds(.01f);
            delayBar.value += .01f;
        }
        anim.Play($"Base Layer.{animsName[1]}", 0); //MeleeAtk
    }
    public void SetIsAtk() => isAtk = !isAtk;
    #endregion
}

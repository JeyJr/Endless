using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayerHand : MonoBehaviour
{
    public List<GameObject> weapons;
    public GameObject weaponSprite;
    public Slider delayBar;
    public Animator anim;
    [SerializeField] private string[] animsName;
    bool isAtk;


    private void Start()
    {
        anim = GetComponent<Animator>();
        EquipWeapon();
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
    public void EquipWeapon()
    {
        GameData gameData = ManagerData.Load();
        //Debug.Log("Equipe weapon acionado!");
        foreach (var weapon in weapons)
        {
            
            if (weapon.GetComponent<WeaponAttributes>().WeaponID == gameData.equipedWeaponId)
            {
                //Debug.Log("Encontrou a arma pelo ID e atribuiu os status");
                var w = weapon.GetComponent<WeaponAttributes>();
                gameData.weaponDmg = w.WeaponAtk;
                gameData.weaponSpeedAtk = w.WeaponSpeedAtk;
                gameData.weaponRangeAtk = w.WeaponAtkRange;
                gameData.equipedWeaponId = w.WeaponID;
                gameData.weaponLife = w.WeaponLife;
                gameData.weaponDefense = w.WeaponDefense;
                gameData.weaponCritical = w.WeaponCritical;

                weaponSprite.GetComponent<SpriteRenderer>().sprite = w.GetComponent<SpriteRenderer>().sprite;
            }
        }

        ManagerData.Save(gameData);
    }

    public void StopDelay() {
        StopCoroutine(StartDelay());
        isAtk = false;
    }

    #endregion
    #region ATK
    IEnumerator StartDelay()
    {
        GameData gameData = ManagerData.Load();

        float atkSpeed = gameData.AtkSpeed;
        delayBar.maxValue = atkSpeed;
        delayBar.value = 0;

        anim.Play($"Base Layer.{animsName[0]}", 0); //Idle

        while (delayBar.value < delayBar.maxValue)
        {
            yield return new WaitForSeconds(.01f);
            delayBar.value += .01f;
        }

        float value = Random.Range(0, 3);

        anim.Play($"Base Layer.{animsName[1]}", 0); //MeleeAtk
    }
    public void SetIsAtk() => isAtk = !isAtk;
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

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

    public void StopDelay() {
        StopCoroutine(StartDelay());
        isAtk = false;
    }

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

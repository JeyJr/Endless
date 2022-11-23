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
        //for (float i = 0; i < atkSpeed; i+= .01f)
        //{
        //    yield return new WaitForSeconds(.01f);
        //    delayBar.value = i;
        //}

        anim.Play($"Base Layer.{animsName[1]}", 0); //MeleeAtk
    }

    public void SetIsAtk() => isAtk = !isAtk;
}

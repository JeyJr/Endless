using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHand : MonoBehaviour
{
    [SerializeField] private float delaySpeed;
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
        delayBar.maxValue = delaySpeed;
        delayBar.value = 0;

        anim.Play($"Base Layer.{animsName[0]}", 0); //Idle

        for (float i = 0; i < delaySpeed; i+= .02f)
        {
            yield return new WaitForSeconds(.02f);
            delayBar.value = i;
        }

        anim.Play($"Base Layer.{animsName[1]}", 0); //MeleeAtk
    }

    public void SetIsAtk() => isAtk = !isAtk;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PlayerHand : MonoBehaviour
{
    public Slider delayBar;
    public Animator rightArmAnim, rightHandAnim;
    bool isAtk;


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

        rightArmAnim.Play($"Base Layer.RightArm_Idle", 0);
        rightHandAnim.Play($"Base Layer.RHand_Idle", 0);

        while (delayBar.value < delayBar.maxValue)
        {
            yield return new WaitForSeconds(.01f);
            delayBar.value += .01f;
        }

        float value = Random.Range(0, 3);

        rightArmAnim.Play($"Base Layer.RightArm_Atk", 0);
        rightHandAnim.Play($"Base Layer.RHand_Atk", 0);
    }
    public void SetIsAtk() => isAtk = !isAtk;
    #endregion
}

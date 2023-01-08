using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.AdaptivePerformance.Provider.AdaptivePerformanceSubsystemDescriptor;

public class PlayerMeleeAtk : MonoBehaviour
{
    [Header("Detecting targets in atk")]
    [SerializeField] private LayerMask target;
    [SerializeField] private Transform atkPosition;

    [Header("Animations control")]
    public Slider delayBar;
    public Animator rightArmAnim, rightHandAnim;
    

    private void Start()
    {
        StartCoroutine(DelayToAtk());
    }

    #region ATK animations
    IEnumerator DelayToAtk()
    {
        while (true)
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

            rightArmAnim.Play($"Base Layer.RightArm_Atk", 0);
            rightHandAnim.Play($"Base Layer.RHand_Atk", 0);

            yield return new WaitForSeconds(rightArmAnim.speed / 2);
        }
    }


    #endregion

    #region DetectingTargetsInAtk 

    //Called in animation Base Layer.RightArm_Atk
    public void DetectingTargetsInAtk()
    {        
        GameData gameData = ManagerData.Load();

        RaycastHit2D[] hit = Physics2D.RaycastAll(atkPosition.position, atkPosition.right, gameData.RangeAtk, target);

        if (hit != null)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (Critical(gameData.CriticalDMG))
                {
                    hit[i].collider.GetComponentInChildren<EnemyStatus>().
                        LoseLife(CriticalDMG(gameData.Damage, gameData.CriticalDMG), true);
                }
                else
                {
                    hit[i].collider.GetComponentInChildren<EnemyStatus>().
                        LoseLife(SimpleDMG(gameData.Damage), false);
                }
            }
        }
    }
    bool Critical(float cri)
    {
        float value = Random.Range(0, 100);
        return value <= cri;
    }
    float CriticalDMG(float damage, float criticalDMG)
    {
        return damage + (damage * criticalDMG / 100);
    }
    float SimpleDMG(float damage)
    {
        return Random.Range(damage, damage * 1.2f);
    }
    private void OnDrawGizmos()
    {
        GameData gameData = ManagerData.Load();
        Debug.DrawRay(atkPosition.position, atkPosition.right * gameData.RangeAtk, Color.yellow);
    }

    #endregion
}

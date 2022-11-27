using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool firstTime = true;

    public float gold;

    public float atk;
    public float def;
    public float vit;
    public float agi;
    public float cri;

    public float Damage 
    {
        get => atk * 5 /* + weaponAtk */;
    }

    public float Defense
    {
        get => def / 2;
    }

    public float MaxLife
    {
        get => vit * 2;
    }
    public float AtkSpeed
    {
        //weaponAtkSpeed - atkSpeed
        get => 3.1f - (agi * .03f);
    }

    public bool CriticalDMG
    {
        get
        {
            float value = Random.Range(0, 100);
            return value <= cri;
        }
    }
}

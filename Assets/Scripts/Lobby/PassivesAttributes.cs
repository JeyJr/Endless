using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PassivesAttributes : MonoBehaviour
{
    [SerializeField] private string skillDecorativeName;
    [SerializeField] private Sprite imgWeapon;

    [SerializeField] private float skillBonus;
    [SerializeField] private string skillDesc;
    [SerializeField] private string skillShortName;

    public TextMeshProUGUI txtLevel;
    public string SkillName{ get => skillDecorativeName; }
    public Sprite ImgWeapon { get => imgWeapon; }
    public float SkillBonus { get => skillBonus; }
    public string SkillDesc { get => skillDesc; }
    public string SkillShortName { get => skillShortName; }

}

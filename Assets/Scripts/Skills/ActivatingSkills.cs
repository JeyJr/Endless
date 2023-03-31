using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum BuffType{atk, def, cri, move, range}
public class ActivatingSkills : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private int _id;
    [SerializeField] private float _buffValue;
    [SerializeField] private float _buffDurationTime;
    [SerializeField] private BuffType _buffType;

    private LevelCanvasUISkillICon _uiSkills;

    [SerializeField] private string _msg;

    private void OnEnable()
    {
        _uiSkills = GameObject.FindWithTag("MainUI").GetComponent<LevelCanvasUISkillICon>();    
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!_uiSkills.CheckID(_id))
            {
                _uiSkills.EnableBuff(_id, _sprite.sprite, _buffValue, _buffDurationTime, _buffType);
                
                var levelCanvas = GameObject.FindWithTag("MainUI").GetComponent<LevelCanvas>();
                levelCanvas.TextLevelInfo(_msg);
            }

            Destroy(this.gameObject);
        }
    }

}

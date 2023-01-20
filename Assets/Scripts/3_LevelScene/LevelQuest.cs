using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelQuest : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image iconQuestOne;
    [SerializeField] private Image iconQuestTwo;
    [SerializeField] private TextMeshProUGUI txtQuestOne;
    [SerializeField] private TextMeshProUGUI txtQuestTwo;
    [SerializeField] private Sprite imgUnchecked, imgChecked;

    public void SetQuestOne(bool completed, string text)
    {
        if (completed)
        {
            iconQuestOne.sprite = imgChecked;
            txtQuestOne.color = Color.yellow;
        }
        else
        {
            iconQuestOne.sprite = imgUnchecked;
            txtQuestOne.color = Color.white;
        }

        txtQuestOne.text = text;
    }

    public void SetQuestTwo(bool completed, string text)
    {
        if (completed)
        {
            iconQuestTwo.sprite = imgChecked;
            txtQuestTwo.color = Color.yellow;
        }
        else
        {
            iconQuestTwo.sprite = imgUnchecked;
            txtQuestTwo.color = Color.white;
        }

        txtQuestTwo.text = text;
    }
}

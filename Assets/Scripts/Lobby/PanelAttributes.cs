using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelAttributes : MonoBehaviour
{
    //0 atk, 1 def, 2 vit, 3 agi, 4 cri;
    public GameObject panelAttributes;
    string[] keys = {"atk", "def", "vit", "agi", "cri"};


    public Slider[] sliders; 
    public TextMeshProUGUI[] textAttributeAmount; 
    public TextMeshProUGUI[] textGoldAmount; 


    private void Start()
    {
        PlayerPrefs.DeleteAll();
        GameData.FirstTime();
        SliderControl();
        TextAttributeAmount();
        TextGold();
    }

    public void AddAttribute(string key)
    {
        GameData.SetAttribute(key, true);
        SliderControl();
        TextAttributeAmount();
        TextGold();
    }
    public void SubAttribute(string key)
    {
        GameData.SetAttribute(key, false);
        SliderControl();
        TextAttributeAmount();
        TextGold();
    }

    void SliderControl() 
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].maxValue = 100;
            sliders[i].value = GameData.GetAttribute(keys[i]);
        }
    }
    void TextAttributeAmount()
    {
        for (int i = 0; i < textAttributeAmount.Length; i++)
        {
            textAttributeAmount[i].text = GameData.GetAttribute(keys[i]).ToString("F0");
        }
    }
    void TextGold()
    {
        for (int i = 0; i < textGoldAmount.Length; i++)
        {
            textGoldAmount[i].text = GameData.GetGoldCost(keys[i]).ToString("F0");
        }
    }

}

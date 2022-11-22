using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelAttributes : MonoBehaviour
{
    public GameObject panelAttributes;
    [SerializeField] private float mult = 50; //100 * 50 = 5k

    [Header("Atk")]
    [Space(5)]
    public Slider atkSlider; 
    public TextMeshProUGUI atkAmount, atkGold;

    [Header("Def")]
    [Space(5)]
    public Slider defSlider;
    public TextMeshProUGUI defAmount, defGold;


    [Header("Vit")]
    [Space(5)]
    public Slider vitSlider;
    public TextMeshProUGUI vitAmount, vitGold;


    [Header("Agi")]
    [Space(5)]
    public Slider agiSlider;
    public TextMeshProUGUI agiAmount, agiGold;


    [Header("Cri")]
    [Space(5)]
    public Slider criSlider;
    public TextMeshProUGUI criAmount, criGold;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("firstTime"))
        {
            PlayerPrefs.SetInt("firstTime", 1);
            for (int i = 1; i < 5; i++)
            {
                AddStatusPoint(i);
            }
        }
    }

    private void Update()
    {
        if (panelAttributes.activeSelf)
        {
            SliderControl();
            TextControl();
        }

        if(Input.GetKey(KeyCode.Space)) PlayerPrefs.DeleteAll();
    }

    public void AddStatusPoint(int attributesNum)
    {
        switch (attributesNum)
        {
            case 1:
                if (GameData.LoadGold() > GameData.LoadAttributes("atk") * mult && GameData.LoadAttributes("atk") < 500)
                    GameData.AddAttributes("atk", mult, 5);
                break;
            case 2:
                if (GameData.LoadGold() > GameData.LoadAttributes("def") * mult && GameData.LoadAttributes("def") < 50)
                    GameData.AddAttributes("def", mult, .5f);
                break;
            case 3:
                if (GameData.LoadGold() > GameData.LoadAttributes("vit") * mult && GameData.LoadAttributes("vit") < 5000)
                    GameData.AddAttributes("vit", mult, 20);
                break;
            case 4:
                if (GameData.LoadGold() > GameData.LoadAttributes("agi") * mult && GameData.LoadAttributes("agi") < 100)
                    GameData.AddAttributes("agi", mult, .02f);
                break;
            case 5:
                if (GameData.LoadGold() > GameData.LoadAttributes("cri") * mult && GameData.LoadAttributes("cri") < 100)
                    GameData.AddAttributes("cri", mult, 1);
                break;
        }
        

        SliderControl();
        TextControl();
    }

    public void SubStatusPoint(int attributesNum)
    {
        switch (attributesNum)
        {
            case 1:
                if (GameData.LoadAttributes("atk") > 2)
                    GameData.SubAttributes("atk", mult, 2);
                break;
            case 2:
                if (GameData.LoadAttributes("def") > 0.5f)
                    GameData.SubAttributes("def", mult, .5f);
                break;
            case 3:
                if (GameData.LoadAttributes("vit") > 120)
                    GameData.SubAttributes("vit", mult, 20);
                break;
            case 4:
                if (GameData.LoadAttributes("agi") > 0.02)
                    GameData.SubAttributes("agi", mult, .02f);
                break;
            case 5:
                if (GameData.LoadAttributes("cri") > 1)
                    GameData.SubAttributes("cri", mult, 1);
                break;
        }


        SliderControl();
        TextControl();
    }

    void SliderControl() 
    {
        atkSlider.maxValue = 500;
        atkSlider.value = GameData.LoadAttributes("atk");

        defSlider.maxValue = 50;
        defSlider.value = GameData.LoadAttributes("def");

        vitSlider.maxValue = 5000;
        vitSlider.value = GameData.LoadAttributes("vit");

        agiSlider.maxValue = 100;
        agiSlider.value = GameData.LoadAttributes("agi"); 
        
        criSlider.maxValue = 100;
        criSlider.value = GameData.LoadAttributes("cri");

    }

    void TextControl()
    {
        atkAmount.text = GameData.LoadAttributes("atk").ToString("F0");
        atkGold.text = (GameData.LoadAttributes("atk") * mult).ToString("F0");

        defAmount.text = GameData.LoadAttributes("def").ToString("F2");
        defGold.text = (GameData.LoadAttributes("def") * mult).ToString("F0");

        vitAmount.text = GameData.LoadAttributes("vit").ToString("F0");
        vitGold.text = (GameData.LoadAttributes("vit") * mult).ToString("F0");

        agiAmount.text = GameData.LoadAttributes("agi").ToString("F2");
        agiGold.text = (GameData.LoadAttributes("agi") * mult).ToString("F0");

        criAmount.text = GameData.LoadAttributes("cri").ToString("F0");
        criGold.text = (GameData.LoadAttributes("cri") * mult).ToString("F0");
    }

}

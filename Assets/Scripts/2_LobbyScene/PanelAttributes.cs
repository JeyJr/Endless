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

    UIControl uiControl;
    [SerializeField] private float goldCost = 200;

    SFXControl sfxControl;

    private void Start()
    {
        GameData data = ManagerData.Load();
        uiControl = GetComponent<UIControl>();

        SliderControl(data);
        TextAttributeAmount(data);
        TextGold(data);

        sfxControl = GameObject.FindWithTag("SFX").GetComponent<SFXControl>();
    }

    public void BtnAdd(string attributeName)
    {
        sfxControl.PlayClip(SFXClip.btnStandarClick);

        GameData data = ManagerData.Load();
        attributeName = attributeName.Trim().ToLower();

        if (attributeName == "atk" && data.gold >= data.atk * goldCost && data.atk < 100)
        {
            data.gold -= data.atk * goldCost;
            data.atk++;
            Infos(0, data.atk);
        }

        if (attributeName == "def" && data.gold >= data.def * goldCost && data.def < 100)
        {
            data.gold -= data.def * goldCost;
            data.def++;
            Infos(1, data.def);
        }

        if (attributeName == "vit" && data.gold >= data.vit * goldCost && data.vit < 100)
        {
            data.gold -= data.vit * goldCost;
            data.vit++;
            Infos(2, data.vit);
        }

        if (attributeName == "agi" && data.gold >= data.agi * goldCost && data.agi < 100)
        {
            data.gold -= data.agi * goldCost;
            data.agi++;
            Infos(3, data.agi);
        }

        if (attributeName == "cri" && data.gold >= data.cri * goldCost && data.cri < 100)
        {
            data.gold -= data.cri * goldCost;
            data.cri++;
            Infos(4, data.cri);
        }

        uiControl.GoldAmount(data.gold.ToString());
        ManagerData.Save(data);
    }

    public void BtnSub(string attributeName)
    {
        sfxControl.PlayClip(SFXClip.btnStandarClick);

        GameData data = ManagerData.Load();
        attributeName = attributeName.Trim().ToLower();

        if (attributeName == "atk" && data.atk > 1)
        {
            data.atk--;
            data.gold += data.atk * goldCost;
            Infos(0, data.atk);
        }

        if(attributeName == "def" && data.def > 1)
        {
            data.def--;
            data.gold += data.def * goldCost;
            Infos(1, data.def);
        }

        if(attributeName == "vit" && data.vit > 1)
        {
            data.vit--;
            data.gold += data.vit * goldCost;
            Infos(2, data.vit);
        }

        if(attributeName == "agi" && data.agi > 1)
        {
            data.agi--;
            data.gold += data.agi * goldCost;
            Infos(3, data.agi);
        }

        if (attributeName == "cri" && data.cri > 1)
        {
            data.cri--;
            data.gold += data.cri * goldCost;
            Infos(4, data.cri);
        }

        uiControl.GoldAmount(data.gold.ToString());
        ManagerData.Save(data);
    }

    void Infos(int index, float values)
    {
        sliders[index].value = values;
        textAttributeAmount[index].text = values.ToString();
        textGoldAmount[index].text = (values * goldCost).ToString();
    }
    void SliderControl(GameData data) 
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].maxValue = 100;
        }

        sliders[0].value = data.atk;
        sliders[1].value = data.def;
        sliders[2].value = data.vit;
        sliders[3].value = data.agi;
        sliders[4].value = data.cri;
    }
    void TextAttributeAmount(GameData data)
    {
        textAttributeAmount[0].text = data.atk.ToString("F0");
        textAttributeAmount[1].text = data.def.ToString("F0");
        textAttributeAmount[2].text = data.vit.ToString("F0");
        textAttributeAmount[3].text = data.agi.ToString("F0");
        textAttributeAmount[4].text = data.cri.ToString("F0");
    }
    void TextGold(GameData data)
    {
        textGoldAmount[0].text = (data.atk * goldCost).ToString();
        textGoldAmount[1].text = (data.def * goldCost).ToString();
        textGoldAmount[2].text = (data.vit * goldCost).ToString();
        textGoldAmount[3].text = (data.agi * goldCost).ToString();
        textGoldAmount[4].text = (data.cri * goldCost).ToString();
    }



}

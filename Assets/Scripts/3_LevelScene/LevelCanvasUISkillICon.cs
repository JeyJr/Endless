using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCanvasUISkillICon : MonoBehaviour
{
    [SerializeField] private List<Sprite> spriteIcons;
    [SerializeField] private GameObject[] iconPosition;
    [SerializeField] private RectTransform root;

    private void Start()
    {

        for (int i = 0; i < root.transform.childCount; i++)
        {
            iconPosition[i] = root.transform.GetChild(i).gameObject;
            iconPosition[i].SetActive(false);
        }

        //for (int i = 0; i < 8; i++)
        //{
        //    GameObject instantiatedObject = Instantiate(icon, iconPosition[i], Quaternion.Euler(0,0,0));
        //    instantiatedObject.transform.SetParent(root);
        //}
    }

    public bool CheckIcon(Sprite sprite)
    {
        return spriteIcons.Contains(sprite);
    }

    public void AddIcon(Sprite sprite)
    {
        spriteIcons.Add(sprite);
        Organize();
    }

    public void RemoveIcon(Sprite sprite)
    {
        try
        {
            if (spriteIcons.Contains(sprite))
            {
                spriteIcons.Remove(sprite);
            }

            Organize();
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public void Organize()
    {
        for (int i = 0; i < iconPosition.Length; i++)
        {
            if (i < spriteIcons.Count)
            {
                iconPosition[i].SetActive(true);
                iconPosition[i].GetComponent<Image>().sprite = spriteIcons[i];
            }
            else
            {
                iconPosition[i].SetActive(false);
            }
        }
    }

}

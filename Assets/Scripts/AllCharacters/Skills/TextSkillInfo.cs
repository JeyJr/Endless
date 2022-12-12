using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSkillInfo : MonoBehaviour
{
    public void SetTextSkillInfo(string text)
    {
        GetComponent<TextMeshProUGUI>().text = " ";
        StopCoroutine(DisableThis());
        
        GetComponent<TextMeshProUGUI>().text = text;
        StartCoroutine(DisableThis());
    }

    IEnumerator DisableThis()
    {
        yield return new WaitForSeconds(5);
        GetComponent<TextMeshProUGUI>().text = " ";
    }
}

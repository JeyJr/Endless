using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PanelLevel : MonoBehaviour
{
    public Button[] btnLevel;

    public void EnableLevel()
    {
        GameData gameData = ManagerData.Load();

        for (int i = 0; i < btnLevel.Length; i++)
        {
            if (i <= gameData.levelUnlock)
                btnLevel[i].interactable = true;
            else
                btnLevel[i].interactable = false;
        }
    }


    public void BtnLoadLevel(GameObject go)
    {
        int num = go.name.IndexOf("l");
        SceneManager.LoadScene($"Level{go.name.Substring(num + 1)}");
    }
}

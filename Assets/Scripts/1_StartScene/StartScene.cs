using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    [SerializeField] private Button btnContinue;
    SFXControl sfxControl;

    private void Start()
    {
        Debug.Log("FirstTime check: " + ManagerData.CheckIfSavedFileExists());
        ManagerData.CaminhoDoArquivo();
        btnContinue.interactable = ManagerData.CheckIfSavedFileExists();

        sfxControl = GameObject.FindWithTag("SFX").GetComponent<SFXControl>();
    }

    public void BtnNewGame()
    {
        sfxControl.PlayClip(SFXClip.btnValidation);
        BtnDeleteSave();
        
        ManagerData.ResetSavedFile();
        SceneManager.LoadScene("Lobby");
    }

    public void BtnContinue()
    {
        sfxControl.PlayClip(SFXClip.btnValidation);
        PlayerPrefs.SetString("Scene", "Lobby");
        SceneManager.LoadScene("Loading");
    }

    public void BtnDeleteSave()
    {
        sfxControl.PlayClip(SFXClip.btnValidation);
        ManagerData.DeleteData();
        btnContinue.interactable = ManagerData.CheckIfSavedFileExists();
    }

    public void BtnInvertActive(GameObject gameObject)
    {
        sfxControl.PlayClip(SFXClip.btnStandarClick);
        gameObject.SetActive(!gameObject.activeSelf);
    }


}

//Build 1.002 - Skills UP
//endles7384JJGames
//games823HJfff - versionT


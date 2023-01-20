using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScene : MonoBehaviour
{
    [SerializeField] private Button btnContinue;

    private void Start()
    {
        Debug.Log("FirstTime check: " + ManagerData.CheckIfSavedFileExists());
        ManagerData.CaminhoDoArquivo();
        btnContinue.interactable = ManagerData.CheckIfSavedFileExists();
    }

    void FirstTime()
    {
        GameData d = new();

        d.gold = 5500;
        d.atk = 1;
        d.def = 1;
        d.vit = 1;
        d.agi = 1;
        d.cri = 1;

        d.purchasedWeaponsIds = new List<int>();
        d.purchasedWeaponsIds.Add(1000); //Pedaço de madeira
        d.equipedWeaponId = d.purchasedWeaponsIds[0];

        d.purchasedArmorIds = new List<int>();
        d.purchasedArmorIds.Add(1000); //StandardSet
        d.equipedArmorId = d.purchasedArmorIds[0];

        d.purchasedHelmetIds = new List<int>();
        d.purchasedHelmetIds.Add(1000); //StandardSet
        d.equipedHelmetId = d.purchasedHelmetIds[0];

        d.purchasedArmIds = new List<int>();
        d.purchasedArmIds.Add(1000); //StandardSet
        d.equipedArmId = d.purchasedArmIds[0];

        d.skillLevelBonusDmg = 0;
        d.skillLevelBonusDef = 0;
        d.skillLevelBonusLife = 0;
        d.skillLevelBonusAtkSpeed = 0;
        d.skillLevelBonusRange = 0;
        d.skillLevelBonusGold = 0;

        d.levelUnlock = 0;

        ManagerData.Save(d);
    }


    public void BtnNewGame()
    {
        BtnDeleteSave();
        FirstTime();
        //Animação de transição
        SceneManager.LoadScene("Lobby");
    }

    public void BtnContinue()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void BtnDeleteSave()
    {
        ManagerData.DeleteData();
        btnContinue.interactable = ManagerData.CheckIfSavedFileExists();
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ConfigVolume : MonoBehaviour
{
    public Slider sliderMusic, sliderSFX;
    public TextMeshProUGUI txtMusicVolume, txtSFXVolume;
    
    const string musicVolume = "musicVolume";
    const string sfxVolume = "sfxVolume";

    public SFXControl sfxControl;
    public MusicControl musicControl;

    private void Start()
    {

        if(sfxControl == null)
            sfxControl = GameObject.FindWithTag("SFX").GetComponent<SFXControl>();

        if (musicControl == null)
            musicControl = GameObject.FindWithTag("Music").GetComponent<MusicControl>();

        if (!PlayerPrefs.HasKey(musicVolume))
            PlayerPrefs.SetFloat(musicVolume, 0.3f);

        if (!PlayerPrefs.HasKey(sfxVolume))
            PlayerPrefs.SetFloat(sfxVolume, 0.5f);

        sliderMusic.value = PlayerPrefs.GetFloat(musicVolume);
        sliderSFX.value = PlayerPrefs.GetFloat(sfxVolume);

        txtMusicVolume.text = "Music: " + (sliderMusic.value * 100).ToString("F2") + "%";
        txtSFXVolume.text = "SFX: " + (sliderSFX.value * 100).ToString("F2") + "%";
    }

    public void SetMusicVolume()
    {
        txtMusicVolume.text = "Music: " + (sliderMusic.value * 100).ToString("F2") + "%";
        PlayerPrefs.SetFloat(musicVolume, sliderMusic.value);

        musicControl.UpdateMusicVolume(sliderMusic.value);
    }

    public void SetSFXVolume()
    {
        txtSFXVolume.text = "SFX: " + (sliderSFX.value * 100).ToString("F2") + "%";
        PlayerPrefs.SetFloat(sfxVolume, sliderSFX.value);

        sfxControl.UpdateSFXVolume(sliderSFX.value);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public enum SFXClip
{
    bossSpawned,
    hitTarget,
    meleeAtk,
    btnStandarClick,
    btnValidation,
    coin,
    getSkill,
    panels,
    ringOfFire,
    windBlade,
    victory,
    equip
}

public class SFXControl : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public AudioSource audioSource;
    private Dictionary<SFXClip, AudioClip> clips;
    private void Start()
    {
        clips = new Dictionary<SFXClip, AudioClip>
        {
            {SFXClip.bossSpawned, audioClips[0]},
            {SFXClip.hitTarget, audioClips[1]},
            {SFXClip.meleeAtk, audioClips[2]},
            {SFXClip.btnStandarClick, audioClips[3]},
            {SFXClip.btnValidation, audioClips[4]},
            {SFXClip.coin, audioClips[5]},
            {SFXClip.getSkill, audioClips[6]},
            {SFXClip.panels, audioClips[7]},
            {SFXClip.ringOfFire, audioClips[8]},
            {SFXClip.windBlade, audioClips[9]},
            {SFXClip.victory, audioClips[10]},
            {SFXClip.equip, audioClips[11]}
        };

        audioSource.volume = PlayerPrefs.GetFloat("sfxVolume");
    }

    public void PlayClip(SFXClip clip)
    {
        if (clips.ContainsKey(clip))
        {
            audioSource.PlayOneShot(clips[clip]);
        }
        else
        {
            Debug.LogError("Clip de áudio não encontrado: " + clip);
        }
    }

    public void UpdateSFXVolume(float value)
    {
        audioSource.volume = value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    public string masterSoundID;
    public string musicSoundID;
    public string sfxSoundID;
    public string sensitivityID;

    [SerializeField] private Slider _master;
    [SerializeField] private Slider _music;
    [SerializeField] private Slider _sfx;
    [SerializeField] private Slider _sensitivity;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(masterSoundID))
        {
            PlayerPrefs.SetFloat(masterSoundID, 1f);
        }

        if (!PlayerPrefs.HasKey(musicSoundID))
        {
            PlayerPrefs.SetFloat(musicSoundID, 1f);
        }

        if (!PlayerPrefs.HasKey(sfxSoundID))
        {
            PlayerPrefs.SetFloat(sfxSoundID, 1f);
        }

        if (!PlayerPrefs.HasKey(sensitivityID))
        {
            PlayerPrefs.SetFloat(sensitivityID, 1f);
        }

        _master.value = PlayerPrefs.GetFloat(masterSoundID);
        _music.value = PlayerPrefs.GetFloat(musicSoundID);
        _sfx.value = PlayerPrefs.GetFloat(sfxSoundID);
        _sensitivity.value = PlayerPrefs.GetFloat(sensitivityID);
    }

    public void OnModifyMasterSound(Slider slider)
    {
        PlayerPrefs.SetFloat(masterSoundID, slider.value);
    }

    public void OnModifyMusicSound(Slider slider)
    {
        PlayerPrefs.SetFloat(musicSoundID, slider.value);
    }

    public void OnModifySFXSound(Slider slider)
    {
        PlayerPrefs.SetFloat(sfxSoundID, slider.value);
    }

    public void OnModifyMouseSensitivity(Slider slider)
    {
        PlayerPrefs.SetFloat(sensitivityID, slider.value);
    }
}

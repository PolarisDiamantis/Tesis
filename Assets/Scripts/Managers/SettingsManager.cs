using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

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
    [SerializeField] private AudioMixer _mixer;

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

        
        if(_master != null) _master.value = PlayerPrefs.GetFloat(masterSoundID);
        if (_music != null) _music.value = PlayerPrefs.GetFloat(musicSoundID);
        if (_sfx != null) _sfx.value = PlayerPrefs.GetFloat(sfxSoundID);
        if (_sensitivity != null) _sensitivity.value = PlayerPrefs.GetFloat(sensitivityID);

        if (PlayerPrefs.GetFloat(masterSoundID) <= 0f)
        {
            _mixer.SetFloat("Master", -80f);
        }
        else
        {
            _mixer.SetFloat("Master", Mathf.Log10(PlayerPrefs.GetFloat(masterSoundID)) * 20);
        }

        //_mixer.SetFloat("Master", Mathf.Log10(PlayerPrefs.GetFloat(masterSoundID)) * 20);

        if (PlayerPrefs.GetFloat(musicSoundID) <= 0f)
        {
            _mixer.SetFloat("Music", -80f);
        }
        else
        {
            _mixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat(musicSoundID)) * 20);
        }
        //_mixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat(musicSoundID)) * 20);

        if (PlayerPrefs.GetFloat(sfxSoundID) <= 0f)
        {
            _mixer.SetFloat("SFX", -80f);
        }
        else
        {
            _mixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat(sfxSoundID)) * 20);
        }
        //_mixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat(sfxSoundID)) * 20);
    }

    public void OnModifyMasterSound(Slider slider)
    {
        PlayerPrefs.SetFloat(masterSoundID, slider.value);
        if(slider.value <= 0f)
        {
            _mixer.SetFloat("Master", -80f);
        }
        else
        {
            _mixer.SetFloat("Master", Mathf.Log10(PlayerPrefs.GetFloat(masterSoundID)) * 20);
        }
    }

    public void OnModifyMusicSound(Slider slider)
    {
        PlayerPrefs.SetFloat(musicSoundID, slider.value);

        if (slider.value <= 0f)
        {
            _mixer.SetFloat("Music", -80f);
        }
        else
        {
            _mixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat(musicSoundID)) * 20);
        }
        //_mixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat(musicSoundID)) * 20);
    }

    public void OnModifySFXSound(Slider slider)
    {
        PlayerPrefs.SetFloat(sfxSoundID, slider.value);

        if (slider.value <= 0f)
        {
            _mixer.SetFloat("SFX", -80f);
        }
        else
        {
            _mixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat(sfxSoundID)) * 20);
        }
        //_mixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat(sensitivityID)) * 20);
    }

    public void OnModifyMouseSensitivity(Slider slider)
    {
        PlayerPrefs.SetFloat(sensitivityID, slider.value);
    }
}

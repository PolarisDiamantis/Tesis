using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class InGameSettignsManager : Singleton<InGameSettignsManager>
{
    
    [SerializeField] private AudioMixer _mixer;


    public string masterSoundID;
    public string musicSoundID;
    public string sfxSoundID;
    public string sensitivityID;

    public PlayerController player;

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

        if (_master.value <= 0f)
        {
            _mixer.SetFloat("Master", -80f);
        }
        else
        {
            _mixer.SetFloat("Master", Mathf.Log10(PlayerPrefs.GetFloat(masterSoundID)) * 20);
        }

        //_mixer.SetFloat("Master", Mathf.Log10(PlayerPrefs.GetFloat(masterSoundID)) * 20);

        if (_music.value <= 0f)
        {
            _mixer.SetFloat("Music", -80f);
        }
        else
        {
            _mixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat(musicSoundID)) * 20);
        }
        //_mixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat(musicSoundID)) * 20);

        if (_sfx.value <= 0f)
        {
            _mixer.SetFloat("SFX", -80f);
        }
        else
        {
            _mixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat(sfxSoundID)) * 20);
        }
        //_mixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat(sfxSoundID)) * 20);

        player.OnResponseChange(PlayerPrefs.GetFloat(sensitivityID));
    }

    public void OnModifyMasterSound(Slider slider)
    {
        PlayerPrefs.SetFloat(masterSoundID, slider.value);
        if (slider.value <= 0f)
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
    }

    public void OnModifyMouseSensitivity(Slider slider)
    {
        PlayerPrefs.SetFloat(sensitivityID, slider.value);
        player.OnResponseChange(slider.value);
    }

    public void ReturnToGame()
    {
        GameManager.Instance.player.GetComponent<PlayerController>().lockInputs = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        GameManager.Instance.ui.pauseUI.SetActive(false);
        GameManager.Instance.time.ResumeTimer();
    }
}

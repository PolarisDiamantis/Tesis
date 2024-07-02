using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class Timer : MonoBehaviour
{
    DateTime _startingTime;
    TimeSpan _currentTime;
    [SerializeField] TextMeshProUGUI _timer;
    [SerializeField] string _currentLevelKey = "lvl1BestTime";
    bool _isActive = false;

    private void Update()
    {
        if (!_isActive) return;
        _currentTime = DateTime.Now - _startingTime;
       //Debug.Log(_currentTime.Hours.ToString("00") + " : " + _currentTime.Minutes.ToString("00") + " : " + _currentTime.Seconds.ToString("00"));
        _timer.text = _currentTime.Hours.ToString("00") + " : " + _currentTime.Minutes.ToString("00") + " : " + _currentTime.Seconds.ToString("00");
    }

    public void StartTimer()
    {
        Debug.Log("Time Started");
        _startingTime = DateTime.Now;
        _isActive = true;
    }

    public void FinishTimer()
    {
        Debug.Log("Finished");
        _isActive = false;
        CheckTimer();
        GameManager.Instance.finalTime = _currentTime;
    }

    public void CheckTimer()
    {
        if(!PlayerPrefs.HasKey(_currentLevelKey + "1"))
        {
            PlayerPrefs.SetString(_currentLevelKey + "1", _currentTime.ToString());
        }
        else
        {
            if(TimeSpan.Parse(PlayerPrefs.GetString(_currentLevelKey + "1")) > _currentTime)
            {
                PlayerPrefs.SetString(_currentLevelKey + "3", PlayerPrefs.GetString(_currentLevelKey + "2"));
                PlayerPrefs.SetString(_currentLevelKey + "2", PlayerPrefs.GetString(_currentLevelKey + "1"));
                PlayerPrefs.SetString(_currentLevelKey + "1", _currentTime.ToString());
            }
            else
            {
                if (!PlayerPrefs.HasKey(_currentLevelKey + "2"))
                {
                    PlayerPrefs.SetString(_currentLevelKey + "2", _currentTime.ToString());
                }
                else if (TimeSpan.Parse(PlayerPrefs.GetString(_currentLevelKey + "2")) > _currentTime)
                {
                    PlayerPrefs.SetString(_currentLevelKey + "3", PlayerPrefs.GetString(_currentLevelKey + "2"));
                    PlayerPrefs.SetString(_currentLevelKey + "2", _currentTime.ToString());
                }
                else
                {
                    if (!PlayerPrefs.HasKey(_currentLevelKey + "3"))
                    {
                        PlayerPrefs.SetString(_currentLevelKey + "3", _currentTime.ToString());
                    }
                    else if(TimeSpan.Parse(PlayerPrefs.GetString(_currentLevelKey + "3")) > _currentTime)
                    {
                        PlayerPrefs.SetString(_currentLevelKey + "3", _currentTime.ToString());
                    }
                }
            }
        }
        PlayerPrefs.Save();
    }
}

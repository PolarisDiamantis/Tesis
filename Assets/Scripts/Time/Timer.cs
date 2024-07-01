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
            Debug.Log("New Record");
            PlayerPrefs.SetString(_currentLevelKey + "1", _currentTime.ToString());
        }
        else
        {
            string prev = PlayerPrefs.GetString(_currentLevelKey);
            TimeSpan prevTime = TimeSpan.Parse(prev);
            if(_currentTime < prevTime)
            {
                Debug.Log("New Record");
                PlayerPrefs.SetString(_currentLevelKey, _currentTime.ToString());
            }
        }
        PlayerPrefs.Save();
    }
}

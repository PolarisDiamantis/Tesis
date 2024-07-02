using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI lvl1R;
    public TextMeshProUGUI lvl2R;
    public TextMeshProUGUI lvl3R;

    public TextMeshProUGUI firstPlace;
    public TextMeshProUGUI secondPlace;
    public TextMeshProUGUI thirdPlace;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        if (PlayerPrefs.HasKey("lvl1BestTime"))
        {
            TimeSpan time = TimeSpan.Parse(PlayerPrefs.GetString("lvl1BestTime"));
            lvl1R.text = time.Hours.ToString("00") + " : " + time.Minutes.ToString("00") + " : " + time.Seconds.ToString("00");
        }
        if (PlayerPrefs.HasKey("lvl2BestTime"))
        {
            TimeSpan time = TimeSpan.Parse(PlayerPrefs.GetString("lvl2BestTime"));
            lvl2R.text = time.Hours.ToString("00") + " : " + time.Minutes.ToString("00") + " : " + time.Seconds.ToString("00");
        }
        if (PlayerPrefs.HasKey("lvl3BestTime"))
        {
            TimeSpan time = TimeSpan.Parse(PlayerPrefs.GetString("lvl3BestTime"));
            lvl3R.text = time.Hours.ToString("00") + " : " + time.Minutes.ToString("00") + " : " + time.Seconds.ToString("00");
        }
    }


    public void LoadLvl1Board()
    {
        if(PlayerPrefs.HasKey("lvl1BestTime" + "1"))
        {
            TimeSpan time = TimeSpan.Parse(PlayerPrefs.GetString("lvl1BestTime" + "1"));
            firstPlace.text = time.Hours.ToString("00") + " : " + time.Minutes.ToString("00") + " : " + time.Seconds.ToString("00");
        }
        else
        {
            firstPlace.text = "1. None";
        }

        if (PlayerPrefs.HasKey("lvl1BestTime" + "2"))
        {
            TimeSpan time = TimeSpan.Parse(PlayerPrefs.GetString("lvl1BestTime" + "2"));
            secondPlace.text = time.Hours.ToString("00") + " : " + time.Minutes.ToString("00") + " : " + time.Seconds.ToString("00");
        }
        else
        {
            firstPlace.text = "2. None";
        }

        if (PlayerPrefs.HasKey("lvl1BestTime" + "3"))
        {
            TimeSpan time = TimeSpan.Parse(PlayerPrefs.GetString("lvl1BestTime" + "3"));
            thirdPlace.text = time.Hours.ToString("00") + " : " + time.Minutes.ToString("00") + " : " + time.Seconds.ToString("00");
        }
        else
        {
            thirdPlace.text = "3. None";
        }
    }

    public void LoadLvl2Board()
    {
        if (PlayerPrefs.HasKey("lvl2BestTime" + "1"))
        {
            TimeSpan time = TimeSpan.Parse(PlayerPrefs.GetString("lvl2BestTime" + "1"));
            firstPlace.text = time.Hours.ToString("00") + " : " + time.Minutes.ToString("00") + " : " + time.Seconds.ToString("00");
        }
        else
        {
            firstPlace.text = "1. None";
        }

        if (PlayerPrefs.HasKey("lvl2BestTime" + "2"))
        {
            TimeSpan time = TimeSpan.Parse(PlayerPrefs.GetString("lvl2BestTime" + "2"));
            secondPlace.text = time.Hours.ToString("00") + " : " + time.Minutes.ToString("00") + " : " + time.Seconds.ToString("00");
        }
        else
        {
            secondPlace.text = "2. None";
        }

        if (PlayerPrefs.HasKey("lvl2BestTime" + "3"))
        {
            TimeSpan time = TimeSpan.Parse(PlayerPrefs.GetString("lvl2BestTime" + "3"));
            thirdPlace.text = time.Hours.ToString("00") + " : " + time.Minutes.ToString("00") + " : " + time.Seconds.ToString("00");
        }
        else
        {
            thirdPlace.text = "3. None";
        }
    }

    public void LoadLvl3Board()
    {
        if (PlayerPrefs.HasKey("lvl3BestTime" + "1"))
        {
            TimeSpan time = TimeSpan.Parse(PlayerPrefs.GetString("lvl3BestTime" + "1"));
            firstPlace.text = time.Hours.ToString("00") + " : " + time.Minutes.ToString("00") + " : " + time.Seconds.ToString("00");
        }
        else
        {
            firstPlace.text = "1. None";
        }

        if (PlayerPrefs.HasKey("lvl3BestTime" + "2"))
        {
            TimeSpan time = TimeSpan.Parse(PlayerPrefs.GetString("lvl3BestTime" + "2"));
            secondPlace.text = time.Hours.ToString("00") + " : " + time.Minutes.ToString("00") + " : " + time.Seconds.ToString("00");
        }
        else
        {
            secondPlace.text = "2. None";
        }

        if (PlayerPrefs.HasKey("lvl3BestTime" + "3"))
        {
            TimeSpan time = TimeSpan.Parse(PlayerPrefs.GetString("lvl3BestTime" + "3"));
            thirdPlace.text = time.Hours.ToString("00") + " : " + time.Minutes.ToString("00") + " : " + time.Seconds.ToString("00");
        }
        else
        {
            thirdPlace.text = "3. None";
        }

    }
}

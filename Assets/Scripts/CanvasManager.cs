using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CanvasManager : MonoBehaviour
{
    public TextMeshProUGUI lvl1R;
    public TextMeshProUGUI lvl2R;

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
    }

}

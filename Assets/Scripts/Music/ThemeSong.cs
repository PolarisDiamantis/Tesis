using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeSong : MonoBehaviour
{
    public static ThemeSong Instance = null;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // When entering a level, the theme song should be destroyed.
    public static void DestroyThemeSong()
    {
        Destroy(ThemeSong.Instance.gameObject);
        ThemeSong.Instance = null;
    }
}

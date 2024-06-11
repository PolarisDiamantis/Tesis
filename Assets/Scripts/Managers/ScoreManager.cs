using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [Header("S Conditions")]
    public float sTime;
    public int sCrystal;

    [Header("A Conditions")]
    public float aTime;
    public int aCrystal;

    [Header("B Conditions")]
    public float bTime;
    public int bCrystal;

    [Header("C Conditions")]
    public float cTime;
    public int cCrystal;

    [Header("D Conditions")]
    public float dTime;
    public int dCrystal;

    [Header("E Conditions")]
    public float eTime;
    public int eCrystal;

    [Header("F Conditions")]
    public float fTime;
    public int fCrystal;

    public string DetermineFinalScore(TimeSpan time, int crystals)
    {
        // Time of 75
        // 30 crystals
        if(time > TimeSpan.FromSeconds(fTime) || fCrystal > crystals)
        {
            return "F";
        }
        if (time >= TimeSpan.FromSeconds(eTime) || eCrystal >= crystals)
        {
            return "E";
        }
        if (time >= TimeSpan.FromSeconds(dTime) || dCrystal >= crystals)
        {
            return "D";
        }
        if (time >= TimeSpan.FromSeconds(cTime) || cCrystal >= crystals)
        {
            return "C";
        }
        if (time >= TimeSpan.FromSeconds(bTime) || bCrystal >= crystals)
        {
            return "B";
        }
        if (time >= TimeSpan.FromSeconds(aTime) || aCrystal >= crystals) 
        {
            return "A";
        }
        else
        {
            return "S";
        }

    }
}

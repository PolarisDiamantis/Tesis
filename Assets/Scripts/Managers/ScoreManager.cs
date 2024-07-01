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

    #region S Score Conditions
    [Header("S Conditions")]
    public float sTime;
    public int sCrystal;
    public int sDeaths;

    public int sTimeInfluence;
    public int sCrystalInfluence;
    public int sDeathsInfluence;

    public int sScoreCondition;
    #endregion

    #region A Score Conditions
    [Header("A Conditions")]
    public float aTime;
    public int aCrystal;
    public int aDeaths;

    public int aTimeInfluence;
    public int aCrystalInfluence;
    public int aDeathsInfluence;

    public int aScoreCondition;
    #endregion

    #region B Score Conditions
    [Header("B Conditions")]
    public float bTime;
    public int bCrystal;
    public int bDeaths;

    public int bTimeInfluence;
    public int bCrystalInfluence;
    public int bDeathsInfluence;

    public int bScoreCondition;
    #endregion

    #region C Score Conditions
    [Header("C Conditions")]
    public float cTime;
    public int cCrystal;
    public int cDeaths;

    // Influence of each variable.
    public int cTimeInfluence;
    public int cCrystalInfluence;
    public int cDeathsInfluence;

    public int cScoreCondition;
    #endregion

    #region D Score Conditions
    [Header("D Conditions")]
    public float dTime;
    public int dCrystal;
    public int dDeaths;

    public int dTimeInfluence;
    public int dCrystalInfluence;
    public int dDeathsInfluence;

    public int dScoreCondition;
    #endregion

    #region E Score Conditions
    [Header("E Conditions")]
    public float eTime;
    public int eCrystal;
    public int eDeaths;

    public int eTimeInfluence;
    public int eCrystalInfluence;
    public int eDeathsInfluence;

    public int eScoreCondition;
    #endregion

    #region F Score Conditions
    [Header("F Conditions")]
    public float fTime;
    public int fCrystal;
    public int fDeaths;

    public int fTimeInfluence;
    public int fCrystalInfluence;
    public int fDeathsInfluence;

    public int fScoreCondition;
    #endregion

    private int _generalScore = 0;
    private string _timeScore = "";
    private string _crystalScore = "";
    private string _deathScore = "";
    private string _finalScore = "";

    public string[] DetermineFinalScore(TimeSpan time, int crystals, int deaths)
    {
        // Time of 75
        // 30 crystals
        _generalScore += CheckTierConditions(time, crystals, deaths, TimeSpan.FromSeconds(fTime), fCrystal, fDeaths, fTimeInfluence, fCrystalInfluence, fDeathsInfluence, "F");
        _generalScore += CheckTierConditions(time, crystals, deaths, TimeSpan.FromSeconds(eTime), eCrystal, eDeaths, eTimeInfluence, eCrystalInfluence, eDeathsInfluence, "E");
        _generalScore += CheckTierConditions(time, crystals, deaths, TimeSpan.FromSeconds(dTime), dCrystal, dDeaths, dTimeInfluence, dCrystalInfluence, dDeathsInfluence, "D");
        _generalScore += CheckTierConditions(time, crystals, deaths, TimeSpan.FromSeconds(cTime), cCrystal, cDeaths, cTimeInfluence, cCrystalInfluence, cDeathsInfluence, "C");
        _generalScore += CheckTierConditions(time, crystals, deaths, TimeSpan.FromSeconds(bTime), bCrystal, bDeaths, bTimeInfluence, bCrystalInfluence, bDeathsInfluence, "B");
        _generalScore += CheckTierConditions(time, crystals, deaths, TimeSpan.FromSeconds(aTime), aCrystal, aDeaths, aTimeInfluence, aCrystalInfluence, aDeathsInfluence, "A");
        _generalScore += CheckTierConditions(time, crystals, deaths, TimeSpan.FromSeconds(sTime), sCrystal, sDeaths, sTimeInfluence, sCrystalInfluence, sDeathsInfluence, "S");

        #region Final Score Check
        if (fScoreCondition >= _generalScore)
        {
            _finalScore = "F";
        }
        else if(eScoreCondition >= _generalScore)
        {
            _finalScore = "E";
        }
        else if (dScoreCondition >= _generalScore)
        {
            _finalScore = "D";
        }
        else if (cScoreCondition >= _generalScore)
        {
            _finalScore = "C";
        }
        else if (bScoreCondition >= _generalScore)
        {
            _finalScore = "B";
        }
        else if (aScoreCondition >= _generalScore)
        {
            _finalScore = "A";
        }
        else if (sScoreCondition >= _generalScore)
        {
            _finalScore = "S";
        }
        #endregion

        string[] r = new string[4];
        r[0] = _finalScore;
        r[1] = _timeScore;
        r[2] = _crystalScore;
        r[3] = _deathScore;
        return r;

    }

    private int CheckTierConditions(TimeSpan inputTime, int inputCrystals, int inputDeaths,TimeSpan timeT, int crystalT, int deathT, int timeS, int crystalS, int deathS, string tier)
    {
        int scoreToAdd = 0;
        if(inputTime > timeT)
        {
            if(_timeScore == "")
            {
                scoreToAdd += timeS;
                _timeScore = tier;
            }
        }
        if(crystalT > inputCrystals)
        {
            if (_crystalScore == "")
            {
                scoreToAdd += crystalS;
                _crystalScore = tier;
            }
        }
        if(inputDeaths >= deathT)
        {
            if(_deathScore == "")
            {
                scoreToAdd += deathS;
                _deathScore = tier;
            }
        }
        return scoreToAdd;
    }
}

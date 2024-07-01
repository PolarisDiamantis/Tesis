using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] public UIManager ui;
    public PlayerModel player;
    public CheckPoint lastCheckPoint;
    private int _collectedCrystals = 0;
    public TimeSpan finalTime;
    [SerializeField] int _totalDeaths = 0;

    public int Crystals
    {
        get
        {
            return _collectedCrystals;
        }
        set
        {
            _collectedCrystals = value;
            UpdateCrystalCountUI(_collectedCrystals);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void UpdateCrystalCountUI(int val)
    {
        ui.UpdateCrystalCount(val);
    }

    public void KillPlayer()
    {
        _totalDeaths++;
        player.KillPlayer(lastCheckPoint);
    }

    public void ForcePlayerBackToBounds()
    {
        _totalDeaths++;
        player.GetBackToLastCheck(lastCheckPoint);
    }

    public void FinalResults()
    {
        string[] finalScore = ScoreManager.Instance.DetermineFinalScore(finalTime, Crystals, _totalDeaths);

        ui.UpdateResults(finalTime, _totalDeaths.ToString(),finalScore[0], finalScore[1], finalScore[2], finalScore[3]);
        ui.ShowResults();

        if (!PlayerPrefs.HasKey("totalCrystals"))
        {
            PlayerPrefs.SetInt("totalCrystals", Crystals);
        }
        else
        {
            int newVal = PlayerPrefs.GetInt("totalCrystals") + Crystals;
            PlayerPrefs.SetInt("totalCrystals", newVal);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}

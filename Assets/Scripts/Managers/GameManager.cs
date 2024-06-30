using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private UIManager _ui;
    public PlayerModel player;
    public CheckPoint lastCheckPoint;
    private int _collectedCrystals = 0;
    public TimeSpan finalTime;

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
        _ui.UpdateCrystalCount(val);
    }

    public void KillPlayer()
    {
        player.KillPlayer(lastCheckPoint);
    }

    public void ForcePlayerBackToBounds()
    {
        player.GetBackToLastCheck(lastCheckPoint);
    }

    public void FinalResults()
    {
        string finalScore = ScoreManager.Instance.DetermineFinalScore(finalTime, Crystals);
        _ui.UpdateResults(finalTime, finalScore);
        _ui.ShowResults();

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

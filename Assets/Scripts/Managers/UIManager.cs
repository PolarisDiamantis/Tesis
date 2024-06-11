using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI crystalsUI;
    public TextMeshProUGUI finalCrystalsUI;
    public TextMeshProUGUI lvlUI;
    public TextMeshProUGUI finalTimeUI;
    public GameObject[] posibleScores;

    public GameObject finalResults;

    public void UpdateCrystalCount(int val)
    {
        crystalsUI.text = "" + val;
    }

    public void UpdateResults(TimeSpan time, string score)
    {
        finalTimeUI.text = "Time: " + time.Minutes.ToString("00") + " : " + time.Seconds.ToString("00");
        finalCrystalsUI.text = "Crystals: " + crystalsUI.text;
        Debug.Log(score);
        switch (score)
        {
            case "S":
                posibleScores[0].SetActive(true);
                break;
            case "A":
                posibleScores[1].SetActive(true);
                break;
            case "B":
                posibleScores[2].SetActive(true);
                break;
            case "C":
                posibleScores[3].SetActive(true);
                break;
            case "D":
                posibleScores[4].SetActive(true);
                break;
            case "E":
                posibleScores[5].SetActive(true);
                break;
            case "F":
                posibleScores[6].SetActive(true);
                break;
            default:
                break;
        }
    }

    public void ShowResults()
    {
        finalResults.SetActive(true);
    }
}

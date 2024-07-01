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
    public TextMeshProUGUI finalDeathsUI;

    public GameObject[] posibleFinalScores;
    public GameObject[] posibleTimeScores;
    public GameObject[] posibleCrystalScores;
    public GameObject[] posibleDeathScores;


    public GameObject finalResults;

    public GameObject runesUI;

    public void UpdateCrystalCount(int val)
    {
        crystalsUI.text = "" + val;
    }

    public void UpdateResults(TimeSpan time, string finalDeathC, string finalScore,string timeScore, string crystalScore, string deathScore)
    {
        finalTimeUI.text = time.Minutes.ToString("00") + " : " + time.Seconds.ToString("00");
        finalCrystalsUI.text = crystalsUI.text;
        finalDeathsUI.text = finalDeathC;
        switch (finalScore)
        {
            case "S":
                posibleFinalScores[0].SetActive(true);
                break;
            case "A":
                posibleFinalScores[1].SetActive(true);
                break;
            case "B":
                posibleFinalScores[2].SetActive(true);
                break;
            case "C":
                posibleFinalScores[3].SetActive(true);
                break;
            case "D":
                posibleFinalScores[4].SetActive(true);
                break;
            case "E":
                posibleFinalScores[5].SetActive(true);
                break;
            case "F":
                posibleFinalScores[6].SetActive(true);
                break;
            default:
                break;
        }
        switch (timeScore)
        {
            case "S":
                posibleTimeScores[0].SetActive(true);
                break;
            case "A":
                posibleTimeScores[1].SetActive(true);
                break;
            case "B":
                posibleTimeScores[2].SetActive(true);
                break;
            case "C":
                posibleTimeScores[3].SetActive(true);
                break;
            case "D":
                posibleTimeScores[4].SetActive(true);
                break;
            case "E":
                posibleTimeScores[5].SetActive(true);
                break;
            case "F":
                posibleTimeScores[6].SetActive(true);
                break;
            default:
                break;
        }
        switch (crystalScore)
        {
            case "S":
                posibleCrystalScores[0].SetActive(true);
                break;
            case "A":
                posibleCrystalScores[1].SetActive(true);
                break;
            case "B":
                posibleCrystalScores[2].SetActive(true);
                break;
            case "C":
                posibleCrystalScores[3].SetActive(true);
                break;
            case "D":
                posibleCrystalScores[4].SetActive(true);
                break;
            case "E":
                posibleCrystalScores[5].SetActive(true);
                break;
            case "F":
                posibleCrystalScores[6].SetActive(true);
                break;
            default:
                break;
        }
        switch (deathScore)
        {
            case "S":
                posibleDeathScores[0].SetActive(true);
                break;
            case "A":
                posibleDeathScores[1].SetActive(true);
                break;
            case "B":
                posibleDeathScores[2].SetActive(true);
                break;
            case "C":
                posibleDeathScores[3].SetActive(true);
                break;
            case "D":
                posibleDeathScores[4].SetActive(true);
                break;
            case "E":
                posibleDeathScores[5].SetActive(true);
                break;
            case "F":
                posibleDeathScores[6].SetActive(true);
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

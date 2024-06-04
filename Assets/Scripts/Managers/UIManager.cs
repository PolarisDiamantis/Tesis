using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI crystalsUI;

    public void UpdateCrystalCount(int val)
    {
        crystalsUI.text = "" + val;
    }
}

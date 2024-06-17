using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WitchSkin", menuName = "Skins/Create New Skin")]
public class WitchSkin : ScriptableObject
{
    public string skinKey;
    public int price;
    public Material skin;
}

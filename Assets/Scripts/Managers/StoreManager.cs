using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoreManager : MonoBehaviour
{
    public static StoreManager Instance;

    [SerializeField] private WitchSkin[] _witchSkins;
    private int step = 0;

    [Header("Base Model")]
    [SerializeField] private SkinnedMeshRenderer _witchModel;

    [Header("Buttons")]
    [SerializeField] private GameObject _equipButton;
    [SerializeField] private GameObject _buyButton;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI _priceUI;
    [SerializeField] private TextMeshProUGUI _crystalsUI;

    [Header("Skins")]
    public GameObject[] skins;

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

    private void Start()
    {
        if (!PlayerPrefs.HasKey("equippedSkinID"))
        {
            PlayerPrefs.SetString("equippedSkinID", _witchSkins[0].skinKey);
        }
        if (PlayerPrefs.HasKey("totalCrystals"))
        {
            _crystalsUI.text = PlayerPrefs.GetInt("totalCrystals").ToString();
        }
        else
        {
            _crystalsUI.text = "0";
            PlayerPrefs.SetInt("totalCrystals", 0);
        }
        LoadSkinInfo(_witchSkins[step]);
    }

    public void BuySelectedSkin()
    {
        if (!PlayerPrefs.HasKey("totalCrystals")) return;
        int currentCrystals = PlayerPrefs.GetInt("totalCrystals");
        if (currentCrystals >= _witchSkins[step].price)
        {
            PlayerPrefs.SetInt("totalCrystals", (currentCrystals - _witchSkins[step].price));
            PlayerPrefs.SetInt(_witchSkins[step].skinKey, 1);
            LoadSkinInfo(_witchSkins[step]);
        }
    }

    private void LoadSkinInfo(WitchSkin info)
    {
        _crystalsUI.text = PlayerPrefs.GetInt("totalCrystals").ToString();
        if (PlayerPrefs.GetInt(info.skinKey) != 0)
        {
            if (PlayerPrefs.GetString("equippedSkinID") != info.skinKey)
            {
                _equipButton.SetActive(true);
                _buyButton.SetActive(false);
            }
            else
            {
                _buyButton.SetActive(false);
                _equipButton.SetActive(false);
            }
        }
        else
        {
            _equipButton.SetActive(false);
            _buyButton.SetActive(true);
        }

        for (int i = 0; i < skins.Length; i++)
        {
            if (i != info.listPos)
            {
                skins[i].SetActive(false);
            }
            else
            {
                skins[i].SetActive(true);
            }
        }
        //_witchModel.material = info.skin;
        _priceUI.text = info.price.ToString();
    }

    public void ShowNext()
    {
        step = (step + 1) % _witchSkins.Length;  // loop back to first skin if at end of array
        LoadSkinInfo(_witchSkins[step]);
    }

    public void ShowPrevious()
    {
        step = (step - 1 + _witchSkins.Length) % _witchSkins.Length;  //loop back to the end if at start
        LoadSkinInfo(_witchSkins[step]);
    }

    public void EquipSkin()
    {
        PlayerPrefs.SetString("equippedSkinID", _witchSkins[step].skinKey);
        LoadSkinInfo(_witchSkins[step]);
    }
}


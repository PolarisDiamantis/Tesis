using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoreManager : MonoBehaviour // Should probably make singleton father for all Managers
{
    public static StoreManager Instance;

    [SerializeField] private WitchSkin[] _witchSkins;
    private int step = 0;

    [SerializeField] private SkinnedMeshRenderer _witchModel;
    [SerializeField] private TextMeshProUGUI _priceUI;
    [SerializeField] private GameObject _equipButton;
    [SerializeField] private GameObject _buyButton;

    //public Material mat;

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
        LoadSkinInfo(_witchSkins[step]);
    }

    public void BuySelectedSkin()
    {
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
        if(PlayerPrefs.GetInt(info.skinKey) != 0)
        {
            if(PlayerPrefs.GetString("equippedSkinID") != info.skinKey)
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

        _witchModel.material = info.skin;
        _priceUI.text = info.price.ToString();
    }

    public void ShowNext()
    {
        if (step + 1 >= _witchSkins.Length) return;
        step++;
        LoadSkinInfo(_witchSkins[step]);
    }

    public void ShowPrevious()
    {
        if (step <= 0) return;
        step--;
        LoadSkinInfo(_witchSkins[step]);
    }

    public void EquipSkin()
    {
        PlayerPrefs.SetString("equippedSkinID", _witchSkins[step].skinKey);
        LoadSkinInfo(_witchSkins[step]);
    }
}

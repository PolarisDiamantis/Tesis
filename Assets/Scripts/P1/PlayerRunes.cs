using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerRunes : MonoBehaviour
{
    private PlayerController _controller;
    private PlayerModel _model;
    [SerializeField] private UIManager _ui;

    [Header("Speed Rune")]
    [SerializeField] [Range(0, 2)] private float _speedModifer = 1f;
    [SerializeField] [Range(0, 2)] private float _shieldModifierS = 1f;
    [SerializeField] [Range(0, 2)] private float _boostModifierS = 1f;

    [Header("Power Rune")]
    [SerializeField] [Range(0, 2)] private float _shieldModifier = 1f;
    [SerializeField] [Range(0, 2)] private float _boostModifier = 1f;
    [SerializeField] [Range(0, 2)] private float _speedModiferP = 1f;

    private void Start()
    {
        _controller = GetComponent<PlayerController>();
        _model = GetComponent<PlayerModel>();
    }
    public void ChooseSpeedRune()
    {
        _controller.ModifySpeed(_speedModifer);
        _controller.ModifyPower(_boostModifierS, _shieldModifierS);
        ChoiceMade();
    }

    public void ChoosePowerRune()
    {
        _controller.ModifyPower(_boostModifier, _shieldModifier);
        _controller.ModifySpeed(_speedModiferP);
        ChoiceMade();
    }

    public void ChooseLifeRune()
    {
        _model.hasLifeRune = true;
    }

    private void ChoiceMade()
    {
        _ui.runesUI.SetActive(false);
    }
}

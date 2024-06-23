using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerView
{
    private ParticleSystem _normalSpeed, _boostSpeed, _boostForceField, _shield, _boostDecharge
        , _boostLoadUp, _boostReady, _crystalParticle;
    bool _isOnThrottleActive = false;
    bool _isOnBoostActive = false;
    bool _isShieldActive = false;

    AudioClip _pickUpSound;
    // Camera
    Animator _camAnim;

    // Skins
    private WitchSkin[] _witchSkins;
    private SkinnedMeshRenderer _witchModel;

    public PlayerView(PlayerModel c)
    {
        
        _camAnim = c.camAnim;
        _normalSpeed = c.normalSpeed;
        _boostSpeed = c.boostSpeed;
        _boostForceField = c.boostParticles;
        _shield = c.shield;
        _boostLoadUp = c.boostLoadUp;
        _boostReady = c.boostReady;
        _boostDecharge = c.boostDecharge;
        _crystalParticle = c.crystalParticle;

        // Action Assignments
        c.OnThrottle += OnThrottle;
        c.OnBoost += OnBoost;
        c.OnShield += OnShield;
        c.OnBoostLoadUp += OnBoostLoadUp;
        c.OnBoostReady += OnBoostReady;
        c.OnCrystalCollected += OnCrystalCollected;

        // Audios
        _pickUpSound = c.pickUpSound;

        // Skin Load Up

        _witchSkins = c.witchSkins;
        _witchModel = c.witchModel;

        if (PlayerPrefs.HasKey("equippedSkinID"))
        {
            string equippedID = PlayerPrefs.GetString("equippedSkinID");
            foreach(WitchSkin i in _witchSkins)
            {
                if(i.skinKey == equippedID)
                {
                    _witchModel.material = i.skin;
                    break;
                }
            }
        }
        else
        {
            _witchModel.material = _witchSkins[0].skin;
            PlayerPrefs.SetString("equippedSkinID", _witchSkins[0].skinKey);
        }
    }

    public void VirtualUpdate()
    {

    }

    private void OnThrottle()
    {
        if (!_isOnThrottleActive)
        {
            _isOnThrottleActive = true;
            _normalSpeed.Play();
        }
        else
        {
            _isOnThrottleActive = false;
            _normalSpeed.Stop();
        }
    }

    private void OnBoost()
    {
        if (!_isOnBoostActive)
        {
            _isOnBoostActive = true;
            _boostReady.Stop();
            //_boostSpeed.Play();
            _boostForceField.Play();
            _boostDecharge.Play();
            _camAnim.Play("Boost");
        }
        else
        {
            _isOnBoostActive = false;
            //_boostSpeed.Stop();
            _boostForceField.Stop();
            _camAnim.Play("Normal");
        }
    }

    private void OnBoostLoadUp()
    {
        _boostLoadUp.Play();
    }

    private void OnBoostReady()
    {
        _boostReady.Play();
    }

    private void OnShield()
    {
        if (!_isShieldActive)
        {
            _isShieldActive = true;
            _shield.Play();
        }
        else
        {
            _isShieldActive = false;
            _shield.Clear();
            _shield.Stop();
        }
    }

    private void OnCrystalCollected()
    {
        _crystalParticle.Play();
        AudioManager.instance.PlaySound(_pickUpSound);
    }
}

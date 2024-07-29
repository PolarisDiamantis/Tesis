using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.VFX;

public class PlayerView
{
    private ParticleSystem _normalSpeed, _boostSpeed, _boostForceField, _shield, _boostDecharge
        , _boostLoadUp, _boostReady, _crystalParticle;
    bool _isOnThrottleActive = false;
    bool _isOnBoostActive = false;
    bool _isShieldActive = false;

    private GameObject _shieldGO;
    bool _isShieldGO = false;

    AudioClip _pickUpSound;
    // Camera
    Animator _camAnim;
    Animator _anim;

    // Skins
    private WitchSkin[] _witchSkins;
    private GameObject _witchModel;

    private VisualEffect _tpInitial;
    private VisualEffect _tpIFinish;
    private VisualEffect _boostVFX;

    private FullScreenTestController2 _frezzeShader;

    public PlayerView(PlayerModel c)
    {
        _camAnim = c.camAnim;
        _anim = c.anim;

        _normalSpeed = c.normalSpeed;
        _boostSpeed = c.boostSpeed;
        _boostForceField = c.boostParticles;
        _shield = c.shield;
        _boostLoadUp = c.boostLoadUp;
        _boostReady = c.boostReady;
        _boostDecharge = c.boostDecharge;
        _crystalParticle = c.crystalParticle;

        _tpInitial = c.tpInitial;
        _tpIFinish = c.tpIFinish;
        _boostVFX = c.boostVFX;

        _frezzeShader = c.frezzeShader;

        if (c._isShieldGO)
        {
            _isShieldGO = true;
            _shieldGO = c.shieldToGO;
        }
        // Action Assignments
        c.OnThrottle += OnThrottle;
        c.OnBoost += OnBoost;
        c.OnShield += OnShield;
        c.OnBoostLoadUp += OnBoostLoadUp;
        c.OnBoostReady += OnBoostReady;
        c.OnCrystalCollected += OnCrystalCollected;
        c.ONTpStart += OnTPStart;
        c.OnTpArrive += OnTPArrive;
        c.ONTpBegan += OnTPBegan;

        c.OnMovement += OnMovement;
        c.OnDeath += OnDeath;
        c.OnDamage += OnDamage;

        c.OnFrozen += OnFrezzeStart;
        // Audios
        _pickUpSound = c.pickUpSound;

        // Skin Load Up

        _witchSkins = c.witchSkins;
        _witchModel = c.witchModel;

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
            _boostVFX.gameObject.SetActive(true);
            _boostDecharge.Play();
            _camAnim.Play("Boost");
        }
        else
        {
            _isOnBoostActive = false;
            //_boostSpeed.Stop();
            _boostVFX.gameObject.SetActive(false);
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
            if (_isShieldGO)
            {
                _shieldGO.SetActive(true);
            }
            else
            {
                _shield.Play();
            }
        }
        else
        {
            _isShieldActive = false;
            if (_isShieldGO)
            {
                _shieldGO.SetActive(false);
            }
            else
            {
                _shield.Clear();
                _shield.Stop();
            }
        }
    }

    private void OnCrystalCollected()
    {
        _crystalParticle.Play();
        if (AudioManager.instance == null) return;
        AudioManager.instance.PlaySound(_pickUpSound);
    }

    private void OnMovement(float x, float y)
    {
        _anim.SetFloat("x", x * 75);
        _anim.SetFloat("y", y);
    }

    private void OnDeath()
    {
        _anim.SetTrigger("Death");
    }

    private void OnDamage()
    {
        _anim.SetTrigger("Damage");
        _anim.SetInteger("DamageID", UnityEngine.Random.Range(0, 2));
    }

    private void OnTPStart()
    {
        _tpInitial.Play();
        _camAnim.Play("TP");
    }

    private void OnTPBegan()
    {
        //_camAnim.Play("TP");
        _witchModel.SetActive(false);
    }

    private void OnTPArrive()
    {
        _witchModel.SetActive(true);
        _tpIFinish.Play();
        _camAnim.Play("Normal");
    }

    private void OnFrezzeStart()
    {
        _frezzeShader.CallFrozenShader();
        // Here goes frezee sound
    }
}

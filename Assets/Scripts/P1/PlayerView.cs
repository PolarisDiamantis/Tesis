using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerView
{
    private ParticleSystem _normalSpeed, _boostSpeed, _boostForceField, _shield
        , _boostLoadUp, _boostReady;
    bool _isOnThrottleActive = false;
    bool _isOnBoostActive = false;
    bool _isShieldActive = false;

    private float _bLoadUpMultiplier = 1.0f;
    // Camera
    Animator _camAnim;

    public PlayerView(PlayerModel c)
    {
        _camAnim = c.camAnim;
        _normalSpeed = c.normalSpeed;
        _boostSpeed = c.boostSpeed;
        _boostForceField = c.boostParticles;
        _shield = c.shield;
        _boostLoadUp = c.boostLoadUp;
        _boostReady = c.boostReady;

        // Action Assignments
        c.OnThrottle += OnThrottle;
        c.OnBoost += OnBoost;
        c.OnShield += OnShield;
        c.OnBoostLoadUp += OnBoostLoadUp;
        c.OnBoostReady += OnBoostReady;
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
            _boostSpeed.Play();
            _boostForceField.Play();
            _camAnim.Play("Boost");
        }
        else
        {
            _isOnBoostActive = false;
            _boostSpeed.Stop();
            _boostForceField.Stop();
            _camAnim.Play("Normal");
        }
    }

    private void OnBoostLoadUp()
    {
        _boostReady.Stop();
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
}

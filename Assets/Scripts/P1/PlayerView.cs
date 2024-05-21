using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerView
{
    private ParticleSystem _normalSpeed, _boostSpeed, _boostForceField;
    bool _isOnThrottleActive = false;
    bool _isOnBoostActive = false;

    // Camera
    Animator _camAnim;

    public PlayerView(PlayerModel c)
    {
        _camAnim = c.camAnim;
        _normalSpeed = c.normalSpeed;
        _boostSpeed = c.boostSpeed;
        _boostForceField = c.boostForceField;

        // Action Assignments
        c.OnThrottle += OnThrottle;
        c.OnBoost += OnBoost;
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
}

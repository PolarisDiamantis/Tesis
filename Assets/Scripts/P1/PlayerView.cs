using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerView
{
    private ParticleSystem _normalSpeed, _boostSpeed;
    bool _isOnThrottleActive = false;
    bool _isOnBoostActive = false;

    // Camera
    CinemachineVirtualCamera _cam;
    float _minFOV;
    float _maxFOV;

    public PlayerView(PlayerModel c)
    {
        _cam = c.cam;
        _normalSpeed = c.normalSpeed;
        _boostSpeed = c.boostSpeed;

        // Action Assignments
        c.OnThrottle += OnThrottle;
        c.OnBoost += OnBoost;
    }

    public void VirtualUpdate()
    {
        _cam.m_Lens.FieldOfView = 120f;
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
        }
        else
        {
            _isOnBoostActive = false;
            _boostSpeed.Stop();
        }
    }
}

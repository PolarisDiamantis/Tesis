using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerModel))]
public class PlayerController : MonoBehaviour
{
    private PlayerModel _agent;

    public float throttleIncrement = 0.1f;
    public float maxThrust = 200F;
    public float responsiveness = 10f;

    private float _throttle;
    private float _roll;
    private float _pitch;
    private float _yaw;

    private bool _throttleUp = false;
    private bool _throttleDown = false;

    private float responseModifier
    {
        get
        {
            return (_agent.rb.mass / 10f) * responsiveness;
        }
    }

    private void Start()
    {
        _agent = GetComponent<PlayerModel>();
    }

    private void Update()
    {
        HandleInputs();
    }

    private void HandleInputs()
    {
        if (_throttleUp) _throttle += throttleIncrement;
        if (_throttleDown) _throttle -= throttleIncrement;
        _throttle = Mathf.Clamp(_throttle, 0f, 100f);
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        _roll = context.ReadValue<float>();

    }

    public void OnPitch(InputAction.CallbackContext context)
    {
        _pitch = context.ReadValue<float>();
    }

    public void OnYaw(InputAction.CallbackContext context)
    {
        _yaw = context.ReadValue<float>();
    }

    public void OnThrottleUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _throttleUp = true;
        }else if (context.canceled)
        {
            _throttleUp = false;
        }
    }

    public void OnThrottleDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _throttleUp = true;
        }else if (context.canceled)
        {
            _throttleDown = false;
        }
    }
}

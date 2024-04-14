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
            return (_agent._rb.mass / 10f) * responsiveness;
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

    private void FixedUpdate()
    {
        _agent.AddForce(transform.forward * maxThrust * _throttle); // Move forward.
        _agent.AddTorque(transform.up * _yaw * responseModifier); // Y rotation.
        _agent.AddTorque(transform.right * _pitch * responseModifier); // Z rotation.
        _agent.AddTorque(-transform.forward * _roll * responseModifier); // X rotation.
    }

    private void HandleInputs()
    {
        Debug.Log("Throttle Up" + _throttleUp);
        Debug.Log("Throttle Down" + _throttleDown);
        if (_throttleUp) _throttle += throttleIncrement;
        if (_throttleDown) _throttle -= throttleIncrement;
        _throttle = Mathf.Clamp(_throttle, 0f, 100f);
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        _roll = context.ReadValue<float>();

    }

    /*public void OnPitch(InputAction.CallbackContext context)
    {
        _pitch = context.ReadValue<float>();
    }

    public void OnYaw(InputAction.CallbackContext context)
    {
        _yaw = context.ReadValue<float>();
    }*/

    public void OnYawPitch(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        _yaw = dir.x;
        _pitch = -dir.y;
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
            _throttleDown = true;
        }else if (context.canceled)
        {
            _throttleDown = false;
        }
    }
}

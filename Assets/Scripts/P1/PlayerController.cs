using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerModel))]
public class PlayerController : MonoBehaviour
{
    private PlayerModel _agent;

    public float throttleIncrement = 0.1f;
    public float maxThrust = 180f;
    public float responsiveness = 120f;
    private float _responseModifier = 1f;

    public float maxRot = 45f;

    private float _throttle;
    private float _roll;
    private float _pitch;
    private float _yaw;
    private float _drift;
    [SerializeField] private float _driftModifier = 1f;

    private bool _throttleUp = false;
    private bool _throttleDown = false;

    float _horizontalInput;
    float _verticalInput;
    float _driftInput;

    [SerializeField] float _controlSnap = 1;

    private bool _lockInputs = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _agent = GetComponent<PlayerModel>();
    }

    private void Update()
    {
        if (_lockInputs) return;
        HandleInputs();
    }

    private void FixedUpdate()
    {
        if (_lockInputs) return;
        _agent.AddForce(transform.forward * maxThrust * _throttle);
        _yaw += _horizontalInput * responsiveness * _responseModifier * Time.fixedDeltaTime;
        _pitch += _verticalInput * responsiveness * _responseModifier * Time.fixedDeltaTime;
        _roll = Mathf.Lerp(0, 30, Mathf.Abs(_horizontalInput)) * -Mathf.Sign(_horizontalInput);
        _drift += _driftInput * responsiveness * _driftModifier * Time.fixedDeltaTime;
        transform.localRotation = Quaternion.Euler(Vector3.up * _yaw + Vector3.up * _drift  + Vector3.right * _pitch + Vector3.forward * _roll);
    }

    private void HandleInputs()
    {
        if (_throttleUp) _throttle += throttleIncrement;
        if (!_throttleUp && _throttle > 50f && !_throttleDown) _throttle -= throttleIncrement;
        if (_throttleDown) _throttle -= throttleIncrement * 2;
        if(_throttle >= 50f)
        {
            _throttle = Mathf.Clamp(_throttle, 50f, 100f);
        }
        else
        {
            _throttle = Mathf.Clamp(_throttle, 0f, 100f);
        }
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        //_roll = context.ReadValue<float>();

    }

    public void OnYawPitch(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        if(dir.x != 0)
        {
            dir.x = Mathf.Clamp(dir.x, -1, 1);
        }
        if (dir.y != 0)
        {
            dir.y = Mathf.Clamp(dir.y, -1, 1);
        }
        // Prevents inputs from snapping.
        _horizontalInput = Mathf.Lerp(_horizontalInput, dir.x, _controlSnap * Time.deltaTime);
        _verticalInput = Mathf.Lerp(_verticalInput, -dir.y, _controlSnap * Time.deltaTime);
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

    public void OnLockInputs(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _lockInputs = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (context.canceled)
        {
            _lockInputs = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void OnDrift(InputAction.CallbackContext context)
    {
        Debug.Log(_driftInput);
        _driftInput = context.ReadValue<float>();
    }

    public void OnResponseChange(float value)
    {
        _responseModifier = value;
    }
}

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

    public float maxRot = 45f;

    private float _throttle;
    private float _roll;
    private float _pitch;
    private float _yaw;

    private bool _throttleUp = false;
    private bool _throttleDown = false;

    float _horizontalInput;
    float _verticalInput;

    [SerializeField] float _controlSnap = 1;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _agent = GetComponent<PlayerModel>();
    }

    private void Update()
    {
        HandleInputs();
    }

    private void FixedUpdate()
    {
        _agent.AddForce(transform.forward * maxThrust * _throttle);
        _yaw += _horizontalInput * responsiveness * Time.fixedDeltaTime;
        _pitch += _verticalInput * responsiveness * Time.fixedDeltaTime;

        _roll = Mathf.Lerp(0, 30, Mathf.Abs(_horizontalInput)) * -Mathf.Sign(_horizontalInput);
        transform.localRotation = Quaternion.Euler(Vector3.up * _yaw + Vector3.right * _pitch + Vector3.forward * _roll);
        //_yaw = Mathf.Lerp(_yaw, 0, Time.fixedDeltaTime);
        //_pitch = Mathf.Lerp(_pitch, 0, Time.fixedDeltaTime);
    }

    private void HandleInputs()
    {
        Debug.Log("Throttle Up" + _throttleUp);
        Debug.Log("Throttle Down" + _throttleDown);
        if (_throttleUp) _throttle += throttleIncrement;
        if (!_throttleUp) _throttle -= throttleIncrement;
        if (_throttleDown) _throttle -= throttleIncrement * 2;
        _throttle = Mathf.Clamp(_throttle, 0f, 100f);
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
        _horizontalInput = Mathf.Lerp(_horizontalInput, dir.x, _controlSnap * Time.deltaTime);
        _verticalInput = Mathf.Lerp(_verticalInput, -dir.y, _controlSnap * Time.deltaTime);
        //_roll = dir.x;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerModel))]
public class PlayerController : MonoBehaviour
{
    private PlayerModel _agent;

    public float throttleIncrement = 0.1f;
    public float maxThrust = 180f;
    public float responsiveness = 120f;
    // Allows player to modify responsiveness.
    private float _responseModifier = 1f;

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
    [Header("Boost Settings")]
    private bool _isBoost = false;
    private bool _canBoost = true;
    [SerializeField] float _boostTime = 4f;
    [SerializeField] float _boostCoolDown = 8f;
    [SerializeField] float _boostCoolDownTimer = 0f;

    [SerializeField] Image _boostBar;

    Vector2 direction;

    public TextMeshProUGUI manaUI;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _agent = GetComponent<PlayerModel>();
    }

    private void Update()
    {
        if(!_canBoost)
        {
            _boostCoolDownTimer += Time.deltaTime;
            _boostBar.fillAmount = _boostCoolDownTimer / (_boostCoolDown + _boostTime);
        }
        if (direction.x == 0 && direction.y == 0)
        {
            //Debug.Log("B");
            _horizontalInput = Mathf.Lerp(_horizontalInput, 0, _controlSnap * Time.deltaTime * 0.7f);
            _verticalInput = Mathf.Lerp(_verticalInput, 0, _controlSnap * Time.deltaTime * 0.7f);
        }
        HandleThrottleInputs();
    }

    private void FixedUpdate()
    {
        if (_lockInputs) return;

        // Throttle
        if (_isBoost)
        {
            _agent.AddForce(transform.forward * maxThrust * 1.2f * _throttle);
        }
        else
        {
            _agent.AddForce(transform.forward * maxThrust * _throttle);

        }

        // Yaw Pitch
        _yaw += _horizontalInput * responsiveness * _responseModifier * Time.fixedDeltaTime;
        _pitch += _verticalInput * responsiveness * _responseModifier * Time.fixedDeltaTime;

        // Roll
        _roll -= _horizontalInput * 50 * Time.fixedDeltaTime;
        _roll -= _driftInput * 50 * Time.fixedDeltaTime;
        _roll = Mathf.Clamp(_roll, -30, 30);
        _roll = Mathf.Lerp(_roll, 0, Time.fixedDeltaTime * 2);

        // Drift
        _drift += _driftInput * _driftModifier * Time.fixedDeltaTime;

        // Final rotation
        transform.localRotation = Quaternion.Euler(Vector3.up * _yaw + Vector3.up * _drift  + Vector3.right * _pitch + Vector3.forward * _roll);
    }

    private void HandleThrottleInputs()
    {
        if (_lockInputs) return;
        if (_throttleUp) _throttle += throttleIncrement;
        if (!_throttleUp && !_throttleDown) _throttle -= throttleIncrement;
        if (_throttleDown) _throttle -= throttleIncrement * 2;
        _throttle = Mathf.Clamp(_throttle, 0f, 100f);
    }

    public void OnYawPitch(InputAction.CallbackContext context)
    {
        Vector2 dir = context.ReadValue<Vector2>();
        if(dir.x != 0)
        {
            dir.x = Mathf.Clamp(dir.x, -3, 3);
        }
        if (dir.y != 0)
        {
            dir.y = Mathf.Clamp(dir.y, -3, 3);
        }
        direction = dir;
        if (Vector3.Distance(dir, Vector3.zero) < 2)
        {
            _horizontalInput = Mathf.Lerp(_horizontalInput, 0, _controlSnap * Time.deltaTime);
            _verticalInput = Mathf.Lerp(_verticalInput, 0, _controlSnap * Time.deltaTime);
            return;
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
            _agent.OnThrottle();
        }else if (context.canceled)
        {
            _throttleUp = false;
            _agent.OnThrottle();
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

    public void OnDrift(InputAction.CallbackContext context)
    {
        _driftInput = context.ReadValue<float>();
    }

    public void OnBoost(InputAction.CallbackContext context)
    {
        if (context.performed && _canBoost)
        {
            StartCoroutine(BoostSequence(_boostTime, _boostCoolDown));
            //_agent.OnBoost();
        }
        /*else if (context.canceled)
        {
            _isBoost = false;
            _agent.OnBoost();
        }*/
    }

    #region Debug Methods
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

    public void OnResponseChange(float value)
    {
        _responseModifier = value;
    }
    #endregion

    public void TakeSpeedReduction(float speed)
    {
        _throttle -= speed;
    }

    IEnumerator BoostSequence(float d, float cd)
    {
        _boostCoolDownTimer = 0f;
        _isBoost = true;
        _canBoost = false;
        _agent.OnBoost();
        yield return new WaitForSeconds(d);
        _isBoost = false;
        _agent.OnBoost();
        yield return new WaitForSeconds(cd);
        _canBoost = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Catapult>() == null) return;
        if (!_isBoost) return;
        other.GetComponent<Catapult>().Die();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRainFollow : MonoBehaviour
{
    [SerializeField] Transform _target;
    public Vector3 offSet;
    public Transform defaultPoint;
    public Transform mediumSpeedPoint;
    public Transform maxSpeedPoint;
    public float maxSpeed = 10f;

    private Rigidbody _targetRigidbody;

    private void Start()
    {
        _targetRigidbody = _target.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float targetSpeed = _targetRigidbody.velocity.magnitude;

        if (targetSpeed < 200f)
        {
            transform.position = defaultPoint.position;
            transform.rotation = defaultPoint.rotation;
        }
        else if (targetSpeed < 500f)
        {
            transform.position = mediumSpeedPoint.position;
            transform.rotation = mediumSpeedPoint.rotation;
        }
        else
        {
            transform.position = maxSpeedPoint.position;
            transform.rotation = maxSpeedPoint.rotation;
        }
    }
}
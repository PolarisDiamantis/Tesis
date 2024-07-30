using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] float _force = 500f;
    [SerializeField] float _gravity = -9.8f;
    [SerializeField] private float _duration = 5f;
    [SerializeField] LayerMask _targets;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(_force * transform.forward, ForceMode.VelocityChange);
        Destroy(gameObject, _duration);
    }

    private void FixedUpdate()
    {
        _rb.AddForce(Vector3.up * _gravity, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 13)
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == _targets)
        {
            Destroy(other.gameObject);
        }
    }
}

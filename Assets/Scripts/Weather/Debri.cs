using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debri : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] float _force = 500f;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(_force * transform.forward, ForceMode.VelocityChange);
        Destroy(gameObject, 5f);
    }
}

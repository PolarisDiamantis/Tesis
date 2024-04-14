using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerModel : MonoBehaviour
{
    public Rigidbody _rb { get; private set;}

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void AddForce(Vector3 dir)
    {
        _rb.AddForce(dir);
    }

    public void AddTorque(Vector3 dir)
    {
        _rb.AddTorque(dir);
    }
}

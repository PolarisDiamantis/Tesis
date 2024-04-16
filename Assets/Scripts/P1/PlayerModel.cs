using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerModel : MonoBehaviour
{
    [SerializeField] private float _collisionSphere;
    [SerializeField] private LayerMask _collisionMask;
    [SerializeField] private float _impulseStreght = 10f;
    public Rigidbody _rb { get; private set;}

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, _collisionSphere, transform.forward, out hit, _collisionSphere, _collisionMask))
        {
            Debug.Log("HIT");
            Vector3 imp = transform.position - hit.point;
            _rb.velocity = Vector3.zero;
            AddImpulse(imp.normalized * _impulseStreght);
        }
    }

    public void AddForce(Vector3 dir)
    {
        _rb.AddForce(dir);
    }

    public void AddTorque(Vector3 dir)
    {
        _rb.AddTorque(dir);
    }

    public void AddImpulse(Vector3 dir)
    {
        _rb.AddForce(dir, ForceMode.VelocityChange);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _collisionSphere);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class SteeringAgent : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] protected float _maxVelocity;
    Rigidbody _rb;
    [SerializeField] protected float _viewRadius = 5;

    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Seek(Vector3 target) // RB Physics System with a destination and max speed.
    {
        Vector3 goal = target - transform.position;
        _rb.AddForce(_force * goal.normalized);
        if (_rb.velocity.magnitude > _maxVelocity)
        {
            _rb.velocity = _rb.velocity.normalized * _maxVelocity;
        }
    }

    public void Seek(Vector3 target, float force)
    {
        Vector3 goal = target - transform.position;
        _rb.AddForce(force * goal.normalized);
        if (_rb.velocity.magnitude > _maxVelocity)
        {
            _rb.velocity = _rb.velocity.normalized * _maxVelocity;
        }
    }

    public void SetVelocity(Vector3 target, float velocity)
    {
        Vector3 goal = target - transform.position;
        _rb.velocity = goal.normalized * velocity;
    }

    public void Arrive(Vector3 target)
    {
        float dist = Vector3.Distance(transform.position, target);
        if (dist > _viewRadius) 
        {
            Seek(target);
        }
        else
        {
            Seek(target, (_maxVelocity * (dist / _viewRadius)));
        }
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, _viewRadius);
    }
}

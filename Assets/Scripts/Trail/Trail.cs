using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Trail : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _distancePerGoal;
    [HideInInspector] public Transform[] path;
    private int step = 0;
    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!TravelPath())
        {
            Destroy(gameObject);
        }
    }

    public void Seek(Vector3 target) // RB Physics System with a destination and max speed.
    {
        Vector3 goal = target - transform.position;
        _rb.AddForce(_force * goal.normalized);
        if(_rb.velocity.magnitude > _maxVelocity)
        {
            _rb.velocity = _rb.velocity.normalized * _maxVelocity;
        }
    }

    public bool TravelPath() // Uses AStar system to follow a path towards the nearest player.
    {
        if (path == null || step >= path.Length) return false;
        Seek(path[step].position);
        if (Vector3.Distance(transform.position, path[step].position) < _distancePerGoal)
        {
            step++;
        }
        return true;
    }
}

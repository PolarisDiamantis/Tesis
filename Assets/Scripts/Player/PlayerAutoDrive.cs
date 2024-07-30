using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerModel))]
public class PlayerAutoDrive : MonoBehaviour
{
    private List<Transform> _path = null;
    private PlayerController _controller;
    private PlayerModel _agent;
    private bool _followingPath = false;
    [SerializeField] private Transform _nextPointLookAt;

    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _distancePerStep = 75f;
    [SerializeField] private float _force = 20000f;

    private void Start()
    {
        _controller = GetComponent<PlayerController>();
        _agent = GetComponent<PlayerModel>();
    }



    private void FixedUpdate()
    {
        if(IsFollowingPath())
        {
            if (FollowPath(_path))
            {
                FinishFollowPath();
            }
        }
    }

    public bool AssignNewPathAndFollow(List<Transform> path)
    {
        if (_followingPath) return false;
        if (path.Count == 0) return false;
        _path = new List<Transform>();
        foreach(var i in path)
        {
            _path.Add(i);
        }
        StartFollowPath(_path);
        return true;
    }

    private bool FollowPath(List<Transform> path)
    {
        if (path.Count <= 0) return true;
        _nextPointLookAt.LookAt(path[0], Vector3.up);
        Quaternion goalRot = Quaternion.Euler(_nextPointLookAt.rotation.eulerAngles.x, _nextPointLookAt.rotation.eulerAngles.y, 0);
        Quaternion rot = Quaternion.Lerp(_agent.transform.rotation, goalRot, Time.fixedDeltaTime * _rotationSpeed);
        _agent.transform.rotation = rot;
        _controller.ModifyRotation(rot.eulerAngles.y, rot.eulerAngles.x);
        Vector3 dir = path[0].position - _agent.transform.position;
        _agent.AddForce(dir.normalized * _force);
        if(Vector3.Distance(_agent.transform.position, path[0].position) <= _distancePerStep)
        {
            path.Remove(path[0]);
        }
        _agent.OnMovement(0, 100f);
        return false;
    }

    private void StartFollowPath(List<Transform> path)
    {
        //path.Reverse();
        _followingPath = true;
        _controller.lockInputs = true;
    }

    private void FinishFollowPath()
    {
        _controller.SetThrottle(0);
        _agent.rb.velocity = Vector3.zero;
        _path = null;
        _followingPath = false;
        _controller.lockInputs = false;
    }

    private bool IsFollowingPath()
    {
        if (_followingPath && _path != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

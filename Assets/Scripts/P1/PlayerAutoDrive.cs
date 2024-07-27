using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerModel))]
public class PlayerAutoDrive : MonoBehaviour
{
    private List<Transform> _path;
    private PlayerController _controller;
    private PlayerModel _agent;

    private void Start()
    {
        _controller = GetComponent<PlayerController>();
    }


}

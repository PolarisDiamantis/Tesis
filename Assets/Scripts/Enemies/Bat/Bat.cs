using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : SteeringAgent
{
    private bool _playerSpotted = false;
    [Header("Bat Settings")]
    [SerializeField] private float _effectArea = 10f;
    [SerializeField] private float _stopDistance = 5f;
    [SerializeField] private float _waitTimeBeforeAttack = 2f;

    private Transform _player;


    // Update: Make the bat go to player pos, once there make it show on their screen for a set amount of time and the attack.
    private void Update()
    {
        if (Vector3.Distance(_rb.position, GameManager.Instance.player.transform.position) <= _effectArea)
        {
            if (!GameManager.Instance.player.GetComponent<PlayerController>().isShield)
            {
                //GameManager.Instance.KillPlayer();
            }
            //Destroy(gameObject);
        }
        if (Vector3.Distance(_rb.position, GameManager.Instance.player.transform.position) <= _viewRadius)
        {
            _playerSpotted = true;
        }
        else
        {
            _playerSpotted = false;
        }
    }

    private void FixedUpdate()
    {
        if (!_playerSpotted) return;
        if (Vector3.Distance(_rb.position, GameManager.Instance.player.transform.position) > _stopDistance)
        {
            _player = GameManager.Instance.player.transform;
            Seek(_player.position);
            LookAtTarget(_player.position);
        }
    }

    private void LookAtTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // Ajusta la velocidad de rotación según sea necesario
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _effectArea);
    }
}

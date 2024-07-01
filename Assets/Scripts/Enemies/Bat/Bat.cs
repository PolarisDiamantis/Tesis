using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : SteeringAgent
{
    private bool _playerSpotted = false;
    [SerializeField] private float _effectArea = 10f;
    private void Update()
    {
        if (Vector3.Distance(_rb.position, GameManager.Instance.player.transform.position) <= _effectArea)
        {
            if (!GameManager.Instance.player.GetComponent<PlayerController>().isShield)
            {
                GameManager.Instance.KillPlayer();
            }
            Destroy(gameObject);
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
        Seek(GameManager.Instance.player.transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _effectArea);
    }
}

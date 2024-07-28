using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : SteeringAgent
{
    private bool _playerSpotted = false;
    private bool _isWaiting = false;
    private Transform _player;
    private Vector3 _initialPosition;

    [Header("Bat Settings")]
    [SerializeField] private float _effectArea = 10f;
    [SerializeField] private float _stopDistance = 5f;
    [SerializeField] private float _minWaitTimeBeforeAttack = 2f;
    [SerializeField] private float _maxWaitTimeBeforeAttack = 2f;
    [SerializeField] private float _forwardOffset = 2f;
    [SerializeField] private float _attackSpeed = 2f;

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, GameManager.Instance.player.transform.position);

        if (distanceToPlayer <= _effectArea)
        {
            if (!GameManager.Instance.player.GetComponent<PlayerController>().isShield)
            {
                GameManager.Instance.KillPlayer();
            }
            Destroy(gameObject);
        }

        if (distanceToPlayer <= _viewRadius)
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
        if (!_playerSpotted || _isWaiting) return;

        _player = GameManager.Instance.player.transform;
        Vector3 forwardPosition = _player.position + _player.forward * _forwardOffset;

        float distanceToPlayer = Vector3.Distance(transform.position, forwardPosition);

        if (distanceToPlayer > _stopDistance)
        {
            Seek(forwardPosition);
            LookAtTarget(forwardPosition);
        }
        else
        {
            if (!_isWaiting)
            {
                StartCoroutine(WaitAndAttack());
            }
        }
    }

    private void LookAtTarget(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private IEnumerator WaitAndAttack()
    {
        _isWaiting = true;
        _player = GameManager.Instance.player.transform;
        Vector3 offset = transform.position - _player.position;
        transform.SetParent(_player);
        _rb.isKinematic = true;

        transform.rotation = _player.rotation;

        yield return new WaitForSeconds(Random.Range(_minWaitTimeBeforeAttack, _maxWaitTimeBeforeAttack));

        Vector3 attackPosition = _player.position;

        while (Vector3.Distance(transform.position, attackPosition) > 0.1f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, _attackSpeed * Time.deltaTime);
            LookAtTarget(attackPosition);
            yield return null;
        }

        /*
        if (Vector3.Distance(transform.position, Vector3.zero) <= 1f)
        {
            if (!GameManager.Instance.player.GetComponent<PlayerController>().isShield)
            {
                GameManager.Instance.KillPlayer();
            }
            Destroy(gameObject);
        }
        */

        _isWaiting = false;
        _rb.isKinematic = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _viewRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _effectArea);
    }
}

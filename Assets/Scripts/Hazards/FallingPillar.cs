using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPillar : MonoBehaviour
{
    private bool _isTriggered = false;
    private bool _isActive = false;
    [SerializeField] private float _fallStreght;
    [SerializeField] private float _triggerRadius;
    private Quaternion originalPos;

    private void Awake()
    {
        originalPos = transform.rotation;
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, GameManager.Instance.player.transform.position) <= _triggerRadius && !_isTriggered)
        {
            _isActive = true;
        }
    }

    private void FixedUpdate()
    {
        if (!_isActive) return;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(120, transform
            .rotation.eulerAngles.y, transform.rotation.eulerAngles.z), Time.fixedDeltaTime * _fallStreght);
        if (transform.rotation.eulerAngles.x >= 85f)
        {
            if (_isActive)
            {
                _isActive = false;
                StartCoroutine(ReturnToOGPos());
            }
        }
    }

    private IEnumerator ReturnToOGPos()
    {
        yield return new WaitForSeconds(3f);
        transform.rotation = originalPos;
        _isTriggered = false;
        _isActive = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.GetComponent<PlayerModel>() && !_isTriggered)
        {
            if (!collision.transform.GetComponent<PlayerController>().isBoost)
            {
                GameManager.Instance.KillPlayer();
            }
            else
            {
                Destroy(gameObject);
            }
            _isTriggered = true;
            StartCoroutine(ReturnToOGPos());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _triggerRadius);
    }
}

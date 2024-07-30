using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debri : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] float _force = 500f;
    [SerializeField] float _mingravity = -9.8f;
    [SerializeField] float _maxgravity = -9.8f;

    [Range(0, 100)]
    [SerializeField] float _speedReduction = 90f;
    [SerializeField] private float _duration = 5f;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(_force * transform.forward, ForceMode.VelocityChange);
        Destroy(gameObject, _duration);
    }

    private void FixedUpdate()
    {
        _rb.AddForce(Vector3.up * Random.Range(_mingravity, _maxgravity), ForceMode.Acceleration);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() == null) return;
        if (other.GetComponent<PlayerController>().isShield)
        {
            Debug.Log("Triggered Shield");
            other.GetComponent<PlayerModel>().AddImpulse(750f * other.transform.forward);
        }
        else
        {
            GameManager.Instance.KillPlayer();
        }
    }
}

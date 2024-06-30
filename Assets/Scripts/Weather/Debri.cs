using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debri : MonoBehaviour
{
    Rigidbody _rb;
    [SerializeField] float _force = 500f;
    [Range(0, 100)]
    [SerializeField] float _speedReduction = 90f;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.AddForce(_force * transform.forward, ForceMode.VelocityChange);
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
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

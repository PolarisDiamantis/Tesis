using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private bool _isActive = false;
    [SerializeField] float _time = 2f;
    [SerializeField] float _destroyTime = 0.5f;
    Material _mat;
    [SerializeField] ParticleSystem _particles;
    MeshRenderer _mesh;

    private void Start()
    {
        if (GetComponent<MeshRenderer>() != null) _mesh = GetComponent<MeshRenderer>();
        Invoke("Activate", _time);
    }

    private void Activate()
    {
        _mesh.enabled = false;
        _particles.Play();
        _isActive = true;
        Destroy(gameObject, _destroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerModel>() == null || !_isActive) return;
        if (other.GetComponent<PlayerController>().isShield)
        {
            Debug.Log("Triggered Shield");
            other.GetComponent<PlayerModel>().AddImpulse(750f * other.transform.forward);
        }
        else
        {
            GameManager.Instance.ReturnToLastCheckPoint();
        }
    }
}

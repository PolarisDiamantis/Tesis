using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    private bool _isActive = false;
    [SerializeField] float _time = 2f;
    Material _mat;
    private void Start()
    {
        if (GetComponent<Material>() != null) _mat = GetComponent<Material>();
        //_mat.color = Color.white;
        Invoke("Activate", _time);
    }

    private void Activate()
    {
        Debug.Log("Activated");
        _isActive = true;
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerModel>() == null || !_isActive) return;
        // Reducir velocidad?
    }
}

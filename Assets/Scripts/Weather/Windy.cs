using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windy : WeatherZone
{
    private Vector3 _windDirection;
    public float strenght = 1500f;

    [Header("Intervals")]
    [SerializeField] float _minInterval = 1f;
    [SerializeField] float _maxInterval = 3f;

    private bool _isBusy = false;

    private void Start()
    {
        StartCoroutine(ChangeWindDirection(Random.Range(_minInterval, _maxInterval)));
    }

    private void Update()
    {
        if (_isBusy) return;
        StartCoroutine(ChangeWindDirection(Random.Range(_minInterval, _maxInterval)));
    }
    private void FixedUpdate()
    {
        foreach (PlayerModel item in _collisions)
        {
            item.AddForce(_windDirection.normalized * strenght);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerModel>() == null) return;
        _collisions.Add(other.GetComponent<PlayerModel>());

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerModel>() == null) return;
        if (_collisions.Contains(other.GetComponent<PlayerModel>()))
        {
            _collisions.Remove(other.GetComponent<PlayerModel>());
        }
    }

    IEnumerator ChangeWindDirection(float time)
    {
        _isBusy = true;
        yield return new WaitForSeconds(time);
        float x = Random.Range(-175, 175);
        float y = Random.Range(0, 360);
        _windDirection = Quaternion.Euler(x, y, 0) * Vector3.forward;
        _isBusy = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, _windDirection * 2 + transform.position);
        Gizmos.DrawWireSphere(transform.position, 0.75f);
    }
}

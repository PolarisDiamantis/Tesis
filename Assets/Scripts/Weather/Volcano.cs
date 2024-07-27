using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volcano : WeatherZone
{
    [Header("Area Of Effect")]
    [SerializeField] float _minX;
    [SerializeField] float _maxX;
    [SerializeField] float _minY;
    [SerializeField] float _maxY;

    private Vector3 _lastSP;
    [SerializeField] private float distancePerDebri = 15f;

    [Header("Instance")]
    [SerializeField] GameObject _instance;
    private bool _isBusy = false;

    [Header("Fragmentation Range")]
    [SerializeField] float _minXRot = -20f;
    [SerializeField] float _maxXRot = 20f;
    [SerializeField] float _minYRot = -10f;
    [SerializeField] float _maxYRot = 10f;

    [Header("Intervals")]
    [SerializeField] private float _minInterval = 0.5f;
    [SerializeField] private float _maxInterval = 0.75f;

    [Header("Areas of Effect")]
    [SerializeField] private float _throwRadius = 2500f;
    [SerializeField] private float _debuffRadius = 750f;

    private void Update()
    {
        if (_isBusy) return;
        StartCoroutine(ThrowDebri());
    }

    private IEnumerator ThrowDebri()
    {
        _isBusy = true;
        yield return new WaitForSeconds(Random.Range(_minInterval, _maxInterval));
        GameObject inst = Instantiate(_instance, GetRNGPosition(), transform.rotation);
        inst.transform.rotation = inst.transform.rotation * GetRNGRotation();
        _lastSP = inst.transform.position;
        _isBusy = false;
    }

    private Quaternion GetRNGRotation()
    {
        float x = Random.Range(_minXRot, _maxXRot);
        float y = Random.Range(_minYRot, _maxYRot);
        return Quaternion.Euler(x, y, 0);
    }

    private Vector3 GetRNGPosition()
    {
        Vector3 pos = transform.position + (transform.right * Random.Range(_minX, _maxX));
        pos += (transform.forward * Random.Range(_minY, _maxY));
        if (Vector3.Distance(pos, _lastSP) <= distancePerDebri) return GetRNGPosition();
        return pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _throwRadius);
        Gizmos.DrawRay(transform.position, transform.right * _maxX);
        Gizmos.DrawRay(transform.position, transform.right * _minX);
        Gizmos.DrawRay(transform.position, transform.forward * _maxY);
        Gizmos.DrawRay(transform.position, transform.forward * _minY);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _debuffRadius);
    }
}

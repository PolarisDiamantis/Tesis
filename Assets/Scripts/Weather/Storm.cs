using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storm : WeatherZone
{
    [Header("Area Of Effect")]
    [SerializeField] float _minX;
    [SerializeField] float _maxX;
    [SerializeField] float _minY;
    [SerializeField] float _maxY;

    [Header("Intervals")]
    [SerializeField] float _minInterval;
    [SerializeField] float _maxInterval;

    [Header("Instance")]
    [SerializeField] GameObject _instance;
    private bool _isBusy = false;

    private void Start()
    {
        StartCoroutine(Lightning(Random.Range(_minInterval, _maxInterval)));
        
    }

    private void Update()
    {
        if (_isBusy) return;
        StartCoroutine(Lightning(Random.Range(_minInterval, _maxInterval)));
    }

    /// <summary>
    /// Uses set min-max X and Y values to randomly return a Vector3 position beetwen those values.
    /// </summary>
    /// <returns>The final position.</returns>
    private Vector3 GetRNGPosition()
    {
        Vector3 pos = transform.position + (transform.right * Random.Range(_minX, _maxX));
        pos += (transform.forward * Random.Range(_minY, _maxY));
        return pos;
    }

    IEnumerator Lightning(float time)
    {
        _isBusy = true;
        yield return new WaitForSeconds(time);
        Instantiate(_instance, GetRNGPosition(), transform.rotation);
        _isBusy = false;
    }
}

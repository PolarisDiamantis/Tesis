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
    public bool isActive = false;

    GameManager _gm;
    [SerializeField] Vector3 offSet;
    [SerializeField] private float distancePerStrike = 15f;
    private Vector3 _lastSP;

    private void Start()
    {
        _gm = GameManager.Instance;
        //StartCoroutine(Lightning(Random.Range(_minInterval, _maxInterval)));
        
    }

    private void Update()
    {
        if (!isActive) return;
        Vector3 newPos = new Vector3(_gm.player.transform.position.x + offSet.x, offSet.y, _gm.player.transform.position.z + offSet.z);
        transform.position = newPos;
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
        if (Vector3.Distance(pos, _lastSP) <= distancePerStrike) return GetRNGPosition();
        return pos;
    }

    IEnumerator Lightning(float time)
    {
        _isBusy = true;
        yield return new WaitForSeconds(time);
        GameObject inst = Instantiate(_instance, GetRNGPosition(), transform.rotation);
        _lastSP = inst.transform.position;
        _isBusy = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 15f);
        Gizmos.DrawRay(transform.position, transform.right * _maxX);
        Gizmos.DrawRay(transform.position, transform.right * _minX);
        Gizmos.DrawRay(transform.position, transform.forward * _maxY);
        Gizmos.DrawRay(transform.position, transform.forward * _minY);
    }
}

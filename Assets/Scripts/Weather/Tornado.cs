using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : WeatherZone
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
    public float strenght = 3000f;

    [SerializeField] Transform[] _path;
    private int step = 0;
    [SerializeField] private float speed = 50;

    private void Start()
    {
        StartCoroutine(ThrowDebri(Random.Range(_minInterval, _maxInterval)));
    }

    private void FixedUpdate()
    {
        transform.position += (_path[step].position - transform.position).normalized * speed * Time.fixedDeltaTime;
        if(Vector3.Distance(transform.position, _path[step].position) < 1)
        {
            if(step + 1 >= _path.Length)
            {
                step = 0;
            }
            else
            {
                step++;
            }
        }
        foreach (PlayerModel item in _collisions)
        {
            Vector3 dir = item.transform.position - new Vector3(transform.position.x, item.transform.position.y, transform.position.z);
            dir = dir.normalized;
            item.AddForce(dir * strenght);
        }
    }

    private void Update()
    {
        if (_isBusy) return;
        StartCoroutine(ThrowDebri(Random.Range(_minInterval, _maxInterval)));
    }

    private Vector3 GetRNGPosition()
    {
        Vector3 pos = transform.position + (transform.right * Random.Range(_minX, _maxX));
        pos += (transform.up * Random.Range(_minY, _maxY));
        return pos;
    }

    IEnumerator ThrowDebri(float time)
    {
        _isBusy = true;
        yield return new WaitForSeconds(time);
        float x = Random.Range(-30, 65);
        float y = Random.Range(0, 360);
        Quaternion rot = Quaternion.Euler(x, y, 0);
        Instantiate(_instance, GetRNGPosition(), rot);
        _isBusy = false;
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
}

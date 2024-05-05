using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    [Header("Area Of Effect")]
    [SerializeField] float _minX;
    [SerializeField] float _maxX;
    [SerializeField] float _minY;
    [SerializeField] float _maxY;
    [Header("Intervals")]
    [SerializeField] float _minInterval, _maxInterval;
    [Header("Intance")]
    [SerializeField] GameObject _instance;

    private void Start()
    {
        StartCoroutine(ThrowDebri(Random.Range(_minInterval, _maxInterval)));
    }

    IEnumerator ThrowDebri(float time)
    {

        yield return new WaitForSeconds(time);
    }
}

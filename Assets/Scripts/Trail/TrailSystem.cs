using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSystem : MonoBehaviour
{
    [SerializeField] private Trail _particles;
    [SerializeField] private float _spawnRate;
    public Transform[] path;
    private bool _isBusy = false;


    private void Start()
    {
        SpawnNewParticle(_particles);
    }

    private void Update()
    {
        if (_isBusy) return;
        StartCoroutine(SpawnCycleSequence(_spawnRate, _particles));
    }
    private void SpawnNewParticle(Trail p)
    {
        Trail obj = Instantiate(p , path[0].position, transform.rotation);
        obj.path = path;
    }

    IEnumerator SpawnCycleSequence(float d, Trail p)
    {
        _isBusy = true;
        yield return new WaitForSeconds(d);
        SpawnNewParticle(p);
        _isBusy = false;
    }
}

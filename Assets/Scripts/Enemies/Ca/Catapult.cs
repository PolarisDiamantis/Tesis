using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    [SerializeField] int stonesPerThrow = 15;
    [Header("Instance")]
    [SerializeField] GameObject _instance;
    [SerializeField] float _interval = 5f;
    [SerializeField] Transform _throwPoint;
    [SerializeField] private float _range = 20f;

    private GameManager _gm;
    private bool _isBusy = false;
    private bool _isOnRange = false;
    private void Start()
    {
        if (GameManager.Instance == null) return;
        _gm = GameManager.Instance;
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position , _gm.player.transform.position) < _range)
        {
            _isOnRange = true;
        }
        else
        {
            _isOnRange = false;
        }
        if (_isBusy || !_isOnRange) return;
        StartCoroutine(TriggerCatapult(_interval));
    }

    public void Die()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }

    IEnumerator TriggerCatapult(float time)
    {
        _isBusy = true;
        yield return new WaitForSeconds(time);
        for(int i = 0; i < stonesPerThrow; i++)
        {
            float x = Random.Range(-40, 40);
            float y = Random.Range(-25, 25);
            _throwPoint.LookAt(GameManager.Instance.player.transform.position);
            Quaternion rot =_throwPoint.rotation * Quaternion.Euler(x, y, 0);
            Instantiate(_instance, _throwPoint.position, rot);
        }
        _isBusy = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}

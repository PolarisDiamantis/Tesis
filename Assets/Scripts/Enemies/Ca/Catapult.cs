using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

    // Anim
    private CatapultView _view;
    public Animator anim;
    public Action OnFire = delegate { };
    public Action OnReload = delegate { };


    private void Awake()
    {
        _view = new CatapultView(this);
    }

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
        if (_isOnRange)
        {
            transform.LookAt(GameManager.Instance.player.transform, Vector3.up);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
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
        OnReload();
        yield return new WaitForSeconds(time);
        for(int i = 0; i < stonesPerThrow; i++)
        {
            float x = UnityEngine.Random.Range(-40, 40);
            float y = UnityEngine.Random.Range(-25, 25);
            _throwPoint.LookAt(GameManager.Instance.player.transform.position);
            Quaternion rot =_throwPoint.rotation * Quaternion.Euler(x, y, 0);
            Instantiate(_instance, _throwPoint.position, rot);
        }
        OnFire();
        yield return new WaitForSeconds(time / 2);
        _isBusy = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Catapult : MonoBehaviour
{
    [Header("View Settings")]
    [SerializeField] private float _range = 20f;
    [SerializeField] float _viewAngle;
    [SerializeField] Transform _playerRot;
    [SerializeField] float _rotationSpeed = 1.5f;

    [Header("Throwable Settings")]
    [SerializeField] Transform _throwPoint;
    [SerializeField] GameObject _instance;
    [SerializeField] int stonesPerThrow = 15;

    [Header("Intervals")]
    [SerializeField] float _minInterval = 5f;
    [SerializeField] float _maxInterval = 5f;

    [Header("Audios")]
    public AudioSource onReload;
    //public AudioSource boomCatapult;

    [Header("Fragmentation Range")]
    [SerializeField] float _minXRot = -20f;
    [SerializeField] float _maxXRot = 20f;
    [SerializeField] float _minYRot = -10f;
    [SerializeField] float _maxYRot = 10f;

    [Header("Animation Settings")]
    private CatapultView _view;
    public Animator anim;
    public Action OnFire = delegate { };
    public Action OnReload = delegate { };

    // Internal
    private GameManager _gm;
    private bool _isBusy = false;
    private bool _isOnRange = false;

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
            Quaternion goalRot = Quaternion.Euler(0, _playerRot.rotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, goalRot, Time.deltaTime * _rotationSpeed);
        }
        if (_isBusy || !_isOnRange || !InFieldOfView(GameManager.Instance.player.transform.position)) return;
        StartCoroutine(TriggerCatapult(UnityEngine.Random.Range(_minInterval, _maxInterval)));
    }

    public bool InFieldOfView(Vector3 target)
    {
        Vector3 dist = new Vector3(target.x, transform.position.y, target.z) - transform.position;
        //if (dist.magnitude > _viewRadius) return false;
        //if (!InLineOfSight(target, dist, dist.magnitude)) return false;
        return Vector3.Angle(transform.forward, dist) <= _viewAngle / 2;
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
            float x = UnityEngine.Random.Range(_minXRot, _maxXRot);
            float y = UnityEngine.Random.Range(_minYRot, _maxYRot);
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
        Gizmos.DrawLine(transform.position, transform.position + GetDirFromAngle(_viewAngle / 2).normalized * _range);
        Gizmos.DrawLine(transform.position, transform.position + GetDirFromAngle(-_viewAngle / 2).normalized * _range);
    }

    public Vector3 GetDirFromAngle(float angleInDegrees)
    {
        angleInDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}

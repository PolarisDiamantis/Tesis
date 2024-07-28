using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerModel))]
public class PlayerForceField : MonoBehaviour
{
    private PlayerModel _agent;
    //private List<Debri> _collectedDebris = new List<Debri>();
    [SerializeField] private int _maxDebris = 5;
    private int _debrisCollected = 0;
    [SerializeField] private GameObject _instance;
    [SerializeField] private float _interval = 0.5f;
    private bool _isBusyFiring = false;
    private bool _isActive = false;
    [SerializeField] private float _duration = 2f;
    [SerializeField] private float _areaOfEffect = 25f;
    [SerializeField] LayerMask _collisionMask;

    private void Start()
    {
        _agent = GetComponent<PlayerModel>();
        _agent.OnForceFieldActivate += ActivateForceField;
        _agent.OnForceFieldCancel += CancelForceField;
    }

    private void FixedUpdate()
    {
        if (!_isActive) return;
        RaycastHit hit;
        if (Physics.SphereCast(_agent.rb.position, _areaOfEffect, transform.forward, out hit, _areaOfEffect, _collisionMask))
        {
            AddDebri();
            if(hit.transform.GetComponent<Debri>() != null)
            {
                Destroy(hit.transform.GetComponent<Debri>().gameObject);
            }
        }
    }

    public void ActivateForceField()
    {
        if (_isActive || _isBusyFiring) return;
        StartCoroutine(ForceFieldProccess(_duration));
    }

    public void CancelForceField()
    {
        if (_isBusyFiring) return;
        StopAllCoroutines();
        _isActive = false;
        FireAllDebris();
    }

    private void AddDebri()
    {
        if (_debrisCollected >= _maxDebris) return;
        _debrisCollected++;
    }

    private void RemoveDebri()
    {
        if (_debrisCollected <= 0) return;
        _debrisCollected--;
    }

    public void FireAllDebris()
    {
        if (_isBusyFiring) return;
        StartCoroutine(FireDebrisProccess(_debrisCollected, _interval));
        _debrisCollected = 0;
    }


    private IEnumerator ForceFieldProccess(float duration)
    {
        _isActive = true;
        yield return new WaitForSeconds(duration);
        _isActive = false;
        FireAllDebris();
    }

    private IEnumerator FireDebrisProccess(int amount, float interval)
    {
        _isBusyFiring = true;
        int a = amount;
        for(int i = 0; i <= a + 1; i++)
        {
            var inst = Instantiate(_instance, transform.position, transform.rotation);
            RemoveDebri();
            yield return new WaitForSeconds(interval);
        }
        _isBusyFiring = false;
    }
}

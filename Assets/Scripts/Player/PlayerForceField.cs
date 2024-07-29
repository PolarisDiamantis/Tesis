using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerModel))]
public class PlayerForceField : MonoBehaviour
{
    private PlayerModel _agent;
    [SerializeField] private int _maxDebris = 5;
    private int _debrisCollected = 0;
    [SerializeField] private GameObject _instance;
    [SerializeField] private float _interval = 0.5f;
    private bool _isBusyFiring = false;
    private bool _isActive = false;
    [SerializeField] private float _duration = 2f;
    [SerializeField] private float _areaOfEffect = 25f;
    [SerializeField] LayerMask _collisionMask;

    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private Transform _debriRotation;

    [SerializeField] private GameObject _debriGraphic;
    private List<GameObject> _collectedDebris = new List<GameObject>();
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
        var inst = Instantiate(_debriGraphic, _debriRotation);
        inst.transform.position = _debriRotation.position + _agent.transform.right * 3f;
        //_collectedDebris.Add(inst);
    }

    private void RemoveDebri()
    {
        Debug.Log("Fired");
        _debrisCollected--;
        Destroy(_debriRotation.GetChild(0).gameObject);
    }

    public void FireAllDebris()
    {
        if (_isBusyFiring) return;
        StartCoroutine(FireDebrisProccess(_debrisCollected, _interval));
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
        for(int i = 1; i <= a; i++)
        {
            RemoveDebri();
            var inst = Instantiate(_instance, _shootingPoint.position, _shootingPoint.rotation);
            yield return new WaitForSeconds(interval);
        }
        _isBusyFiring = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _areaOfEffect);
    }
}

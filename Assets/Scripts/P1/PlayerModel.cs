using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerModel : MonoBehaviour
{
    [Header("Impulse Settings")]
    [SerializeField] private float _collisionSphere;
    [SerializeField] private LayerMask _collisionMask;
    [SerializeField] private float _impulseStreght = 10f;

    public Rigidbody rb { get; private set;}

    public Action OnThrottle = delegate { };
    public Action OnBoost = delegate { };
    public Action OnShield = delegate { };
    public Action OnBoostLoadUp = delegate { };
    public Action OnBoostReady = delegate { };

    private PlayerView _view;

    [Header("Particles")]
    public ParticleSystem normalSpeed;
    public ParticleSystem boostSpeed;
    public ParticleSystem boostParticles;
    public ParticleSystem shield;
    public ParticleSystem boostLoadUp;
    public ParticleSystem boostReady;
    //public CinemachineVirtualCamera cam;

    [Header("Animations")]
    public Animator camAnim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        _view = new PlayerView(this);
    }

    private void Start()
    {
        GameManager.Instance.player = this;
    }

    private void FixedUpdate()
    {
        //Debug.Log(_rb.velocity.magnitude);
        // When about to collision with an obstacles, impulses the player in the opposite direction of said collision.
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, _collisionSphere, transform.forward, out hit, _collisionSphere, _collisionMask))
        {
            Debug.Log("HIT");
            Vector3 imp = transform.position - hit.point;
            rb.velocity = Vector3.zero;
            AddImpulse(imp.normalized * _impulseStreght);
        }
    }

    private void Update()
    {
        _view.VirtualUpdate();
    }

    #region Physics Methods
    public void AddForce(Vector3 dir)
    {
        rb.AddForce(dir);
    }

    public void AddTorque(Vector3 dir)
    {
        rb.AddTorque(dir);
    }

    public void AddImpulse(Vector3 dir)
    {
        rb.AddForce(dir, ForceMode.VelocityChange);
    }
    #endregion

    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _collisionSphere);
    }
    #endregion
}

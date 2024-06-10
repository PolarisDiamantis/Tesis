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
    public Action OnCrystalCollected = delegate { };

    private PlayerView _view;

    [Header("Particles")]
    public ParticleSystem normalSpeed;
    public ParticleSystem boostSpeed;
    public ParticleSystem boostParticles;
    public ParticleSystem shield;
    public ParticleSystem boostLoadUp;
    public ParticleSystem boostReady;
    public ParticleSystem boostDecharge;
    public ParticleSystem crystalParticle;
    //public CinemachineVirtualCamera cam;

    [Header("Animations")]
    public Animator camAnim;

    [Header("Audios")]
    public AudioClip pickUpSound;

    [SerializeField] private float _magnetSphere; 
    [SerializeField] private LayerMask _magnetMask;

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
            Vector3 imp = transform.position - hit.point;
            rb.velocity = Vector3.zero;
            AddImpulse(imp.normalized * _impulseStreght);
        }

        if(Physics.SphereCast(rb.position, _magnetSphere, transform.forward, out hit, _magnetSphere, _magnetMask))
        {
            Debug.Log("HIT");
            hit.transform.GetComponent<Crystal>().isPicked = true;
        }
    }

    private void Update()
    {
        _view.VirtualUpdate();
    }

    public void CrystalCollected()
    {
        OnCrystalCollected();
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _magnetSphere);
    }
    #endregion
}

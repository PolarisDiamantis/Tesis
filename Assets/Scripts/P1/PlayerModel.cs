using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerModel : MonoBehaviour
{
    [SerializeField] private float _collisionSphere;
    [SerializeField] private LayerMask _collisionMask;
    [SerializeField] private float _impulseStreght = 10f;
    public Rigidbody rb { get; private set;}

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
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

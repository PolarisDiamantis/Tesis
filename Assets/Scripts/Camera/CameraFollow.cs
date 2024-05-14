using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 locationOffset;
    public Vector3 rotationOffset;
    [SerializeField] private float _maxDistance;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + target.rotation * locationOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        /*if(Vector3.Distance(transform.position, target.position) <= _maxDistance)
        {
            transform.position = smoothedPosition;
        }*/
        transform.position = smoothedPosition;
        Quaternion desiredrotation = target.rotation * Quaternion.Euler(rotationOffset) * Quaternion.Euler(0, 0, 0);
        Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed * Time.fixedDeltaTime);
        transform.rotation = smoothedrotation;
    }

    /*private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + target.rotation * locationOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        Quaternion desiredrotation = target.rotation * Quaternion.Euler(rotationOffset) * Quaternion.Euler(0, 0, 0);
        Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed * Time.deltaTime);
        transform.rotation = smoothedrotation;
    }*/
}

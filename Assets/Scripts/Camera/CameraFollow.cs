using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 locationOffset;
    public Vector3 rotationOffset;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + target.rotation * locationOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
        //Debug.Log(target.rotation.eulerAngles.z);
        Quaternion desiredrotation = target.rotation * Quaternion.Euler(rotationOffset) * Quaternion.Euler(0, 0, -target.rotation.eulerAngles.z);
        Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed * Time.fixedDeltaTime);
        transform.rotation = smoothedrotation;
    }
}

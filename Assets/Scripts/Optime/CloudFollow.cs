using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudFollow : MonoBehaviour
{
    [SerializeField] Transform _target;

    private void Update()
    {
        Vector3 newPos = new Vector3(_target.position.x, transform.position.y, _target.position.z);
        transform.position = newPos;
    }
}

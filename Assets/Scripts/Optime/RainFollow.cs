using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainFollow : MonoBehaviour
{
    [SerializeField] Transform _target;
    public Vector3 offSet;

    private void Update()
    {
        Vector3 newPos = new Vector3(_target.position.x + offSet.x, _target.position.y + offSet.y, _target.position.z + offSet.z);
        transform.position = newPos;
    }
}

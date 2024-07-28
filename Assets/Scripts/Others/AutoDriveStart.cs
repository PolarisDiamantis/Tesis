using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDriveStart : MonoBehaviour
{
    [SerializeField] private List<Transform> path;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerAutoDrive>() != null)
        {
            other.GetComponent<PlayerAutoDrive>().AssignNewPathAndFollow(path);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Transform spawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerModel>() == null) return;
        GameManager.Instance.lastCheckPoint = this;
    }
}

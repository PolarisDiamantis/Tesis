using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    [HideInInspector] public bool collision { get; private set; } = false;

    private void Start()
    {
        if (BorderManager.Instance == null) return;
        BorderManager.Instance.borderList.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerModel>() != null)
        {
            Debug.Log("Player entered border");
            collision = true;
            BorderManager.Instance.StatusCheck();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerModel>() != null)
        {
            Debug.Log("Player left border");
            collision = false;
            BorderManager.Instance.StatusCheck();
        }
    }
}

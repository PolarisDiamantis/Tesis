using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDebuffer : MonoBehaviour
{
    [HideInInspector] public bool collision { get; private set; } = false;

    private void Start()
    {
        if (SpeedManager.Instance == null) return;
        SpeedManager.Instance.speedList.Add(this);
    }

    #region Collision Logic Methods
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerModel>() != null)
        {
            Debug.Log("Player entered fog");
            collision = true;
            SpeedManager.Instance.StatusCheck();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerModel>() != null)
        {
            Debug.Log("Player left fog");
            collision = false;
            SpeedManager.Instance.StatusCheck();
        }
    }
    #endregion
}

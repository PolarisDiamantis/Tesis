using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerModel>() != null)
        {
            GameManager.Instance.Crystals++;
            Destroy(gameObject);
        }
    }
}

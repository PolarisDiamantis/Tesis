using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{

    public GameObject Particles;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerModel>() != null)
        {
            GameManager.Instance.Crystals++;
            Instantiate(Particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

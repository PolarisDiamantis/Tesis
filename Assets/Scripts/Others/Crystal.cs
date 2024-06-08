using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{

    public GameObject Particles;
    [SerializeField] AudioClip PickupSound;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerModel>() != null)
        {
            GameManager.Instance.Crystals++;
            AudioManager.instance.PlaySound(PickupSound);
            Instantiate(Particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

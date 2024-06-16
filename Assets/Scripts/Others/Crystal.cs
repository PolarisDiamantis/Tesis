using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : SteeringAgent
{

    public GameObject Particles;
    [SerializeField] AudioClip PickupSound;
    public bool isPicked = false;
    public string isActive; 

    public Animator Anim;

    private void Start()
    {
        Anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (!isPicked) return;
        {
            SetVelocity(GameManager.Instance.player.transform.position, _maxVelocity);
            Anim.Play(isActive);
        }

      
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerModel>() != null)
        {
            GameManager.Instance.Crystals++;
            other.GetComponent<PlayerModel>().CrystalCollected();
            //Instantiate(Particles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}

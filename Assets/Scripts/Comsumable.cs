using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comsumable : MonoBehaviour
{
    public PlayerEffects myEffect;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerPickUps>() != null)
        {
            other.GetComponent<PlayerPickUps>().TriggerEffect(myEffect);
            Destroy(transform.parent.gameObject);
        }
    }
}

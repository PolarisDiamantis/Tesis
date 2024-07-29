using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceProyectile : Debri
{
    [SerializeField] private float _effectDuration = 2f;
    [SerializeField] private float _throttleModifier = -25f;

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() == null) return;
        if (other.GetComponent<PlayerController>().isShield)
        {
            Debug.Log("Triggered Shield");
            other.GetComponent<PlayerModel>().AddImpulse(750f * other.transform.forward);
        }
        else
        {
            other.GetComponent<PlayerController>().ModifyThrottle(_throttleModifier, _effectDuration, ModifyThrottleSource.frezee);
        }
    }
}

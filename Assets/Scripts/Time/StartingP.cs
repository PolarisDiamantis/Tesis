using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingP : MonoBehaviour
{
    [SerializeField] Timer _time;
    private bool _triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerModel>() == null || _triggered) return;
        _triggered = true;
        _time.StartTimer();
    }
}

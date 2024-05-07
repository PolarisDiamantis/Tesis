using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishP : MonoBehaviour
{
    [SerializeField] Timer _time;
    private bool _triggered = false;

    [SerializeField] ManagerScene _scene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerModel>() == null || _triggered) return;
        _triggered = true;
        _time.FinishTimer();
        _scene.ChangeEscene("MainMenu");
    }
}

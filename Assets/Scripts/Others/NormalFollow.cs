using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalFollow : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(GameManager.Instance.player.transform, Vector3.up);
    }
}

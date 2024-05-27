using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFollow : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(GameManager.Instance.player.transform, Vector3.up);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y - 180, 0);
    }
}

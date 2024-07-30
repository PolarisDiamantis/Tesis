using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStasrtingSpawn : MonoBehaviour
{
    private PlayerController _agent;

    private void Start()
    {
    }

    public void SetRotation()
    {
        _agent = GameManager.Instance.player.GetComponent<PlayerController>();

        _agent.ModifyRotation(transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.x);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5);
    }
}
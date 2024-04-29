using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : Weather
{
    List<PlayerModel> _collisions = new List<PlayerModel>();
    public Vector3 windDirection;
    public float strenght = 100f;

    private void FixedUpdate()
    {
        foreach(PlayerModel item in _collisions)
        {
            item.AddForce(windDirection * strenght);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerModel>() == null) return;
        _collisions.Add(other.GetComponent<PlayerModel>());

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerModel>() == null) return;
        if (_collisions.Contains(other.GetComponent<PlayerModel>()))
        {
            _collisions.Remove(other.GetComponent<PlayerModel>());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, windDirection * 2 + transform.position);
        Gizmos.DrawWireSphere(transform.position, 0.75f);
    }
}

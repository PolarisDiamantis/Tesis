using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerModel player;
    public CheckPoint lastCheckPoint;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ReturnToLastCheckPoint()
    {
        Rigidbody p = player.GetComponent<Rigidbody>();
        p.position = lastCheckPoint.spawnPoint.position;
        //player.transform.position = lastCheckPoint.spawnPoint.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    [HideInInspector] public List<SpeedDebuffer> speedList = new List<SpeedDebuffer>();
    public static SpeedManager Instance;
    [SerializeField] private PlayerController _player;

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

    public void StatusCheck()
    {
        bool status = false;
        foreach (SpeedDebuffer item in speedList)
        {
            if (item.collision)
            {
                status = true;
            }
        }
        if (status)
        {
            _player.isSlowed = true;   
        }
        if (!status)
        {
            _player.isSlowed = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class AudioCave : MonoBehaviour
{
    [SerializeField] private AudioSource audioOff;
    [SerializeField] private AudioSource AudioOn;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject original;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            if(original != null) original.SetActive(false);

            AudioOn.Play();
            if(audioOff != null) audioOff.Stop();
        }
    }
}
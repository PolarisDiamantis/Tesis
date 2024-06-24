using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCave : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private AudioSource audioSource2;
    [SerializeField] private GameObject trigger1;
    [SerializeField] private GameObject trigger2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == trigger1)
        {
            audioSource2.Play();
            audioSource1.Stop();
        }
        else if (other.gameObject == trigger2)
        {
            audioSource2.Stop();
            audioSource1.Play();
        }
    }
}
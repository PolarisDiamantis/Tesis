using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerMixer : MonoBehaviour
{
    
    public static AudioManagerMixer instance;

    private AudioSource _audioSource;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public void EjucarSonido(AudioClip sonido)
    {
        _audioSource.PlayOneShot(sonido);
    }
}

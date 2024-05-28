using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherEvent : MonoBehaviour
{
    [SerializeField] private Storm _weather;
    [SerializeField] private GameObject _weatherVisual;
    [SerializeField] private List<ParticleSystem> _particlesEffects;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerModel>() != null)
        {
            foreach(ParticleSystem par in _particlesEffects)
            {
                par.Play();
            }
            _weather.isActive = true;
            _weatherVisual.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerModel>() != null)
        {
            foreach (ParticleSystem par in _particlesEffects)
            {
                par.Stop();
            }
            _weather.isActive = false;
            _weatherVisual.SetActive(false);
        }
    }
}

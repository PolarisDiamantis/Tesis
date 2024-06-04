using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PPZone : MonoBehaviour
{
    [SerializeField] private Volume _zoneVolume;
    [SerializeField] [Range(-1, 1)] private float _volumeModifier;
    [SerializeField] private float _modificationSpeed;
    private float _goalVolume;
    private bool _isBusy = false;
    [SerializeField] private bool _canBeMultiTriggered = false;
    private bool _hasTriggered = false;

    private void Update()
    {
        
    }

    private void ApplyVolume()
    {
        if (_isBusy) return;
        _goalVolume = _volumeModifier + _zoneVolume.weight;
        _goalVolume = Mathf.Clamp(_goalVolume, 0, 1);
        StartCoroutine(SwitchVolumes(_modificationSpeed));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerModel>() != null && !_hasTriggered)
        {
            ApplyVolume();
            if (!_canBeMultiTriggered)
            {
                _hasTriggered = true;
            }
        }
    }

    private IEnumerator SwitchVolumes(float speed)
    {
        _isBusy = true;
        while (_zoneVolume.weight != _goalVolume)
        {
            if(_zoneVolume.weight > _goalVolume)
            {
                _zoneVolume.weight -= Time.deltaTime * speed;
            }
            else {
                if(_zoneVolume.weight < _goalVolume)
                {
                    _zoneVolume.weight += Time.deltaTime * speed;
                }
            }
            if(Mathf.Abs(_zoneVolume.weight - _goalVolume) <= (Time.deltaTime * speed + Time.deltaTime))
            {
                _zoneVolume.weight = _goalVolume;
            }
            yield return null;
        }
        _isBusy = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShaderExitMap : MonoBehaviour
{
    [Header("Time Stats")]
    [SerializeField] private float _hurtDisplayTime = 1.5f;
    [SerializeField] private float _hurtFadeOutTime = 0.5f;
    [SerializeField] float _cooldownReplay;

    [Header("References")]
    [SerializeField] private ScriptableRendererFeature _fullScreenExitMap;
    [SerializeField] private Material _material;


    float _timer;

    private int _fullScreenIntensity = Shader.PropertyToID("_FullScreenIntensity");

    private const float Activate_ON = 0.6f;

    private void Start()
    {
        _timer = _cooldownReplay;
        _fullScreenExitMap.SetActive(false);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        /*
        if (Input.GetKeyDown(KeyCode.T) && _timer >= _cooldownReplay)
        {
            StartCoroutine(Hurt());
            _timer = 0;
        }
        
        else if ((Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            stopShader();
        }
        */
    }

    public void CallExitMapShader()
    {
        if (_timer >= _cooldownReplay)
        {
            StartCoroutine(Hurt());
            _timer = 0;
        }
    }

    IEnumerator Hurt()
    {
        _fullScreenExitMap.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < _hurtDisplayTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedActivate = Mathf.Lerp(0f, Activate_ON, (elapsedTime / _hurtDisplayTime));
            _material.SetFloat(_fullScreenIntensity, lerpedActivate);
            yield return null;
        }

        _material.SetFloat(_fullScreenIntensity, Activate_ON);

        yield return new WaitForSeconds(_hurtDisplayTime);

        elapsedTime = 0f;
        while (elapsedTime < _hurtFadeOutTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedActivate = Mathf.Lerp(Activate_ON, 0f, (elapsedTime / _hurtFadeOutTime));
            _material.SetFloat(_fullScreenIntensity, lerpedActivate);
            yield return null;
        }

        stopShader();
    }


    public void stopShader()
    {
        _fullScreenExitMap.SetActive(false);
    }
}

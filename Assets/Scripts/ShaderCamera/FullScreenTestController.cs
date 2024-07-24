using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FullScreenTestController : MonoBehaviour
{
    [Header("Time Stats")]
    [SerializeField] private float _hurtDisplayTime = 1.5f;
    [SerializeField] private float _hurtFadeOutTime = 0.5f;
    [SerializeField] float _cooldownReplay;

    [Header("References")]
    [SerializeField] private ScriptableRendererFeature _fullScreenDamage;
    [SerializeField] private Material _material;

    float _timer;

    private int _blend = Shader.PropertyToID("_Blend");

    private const float Activate_ON = 0.1f;

    private void Start()
    {
        _timer = _cooldownReplay;
        _fullScreenDamage.SetActive(false);
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.R) && _timer >= _cooldownReplay)
        {
            StartCoroutine(Hurt());
            _timer = 0;
        }
        /*
        else if ((Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            stopShader();
        }
        */
    }

    IEnumerator Hurt()
    {
        _fullScreenDamage.SetActive(true);
        _material.SetFloat(_blend, Activate_ON);

        yield return new WaitForSeconds(_hurtDisplayTime);

        float elapsedTime = 0f;
        while (elapsedTime < _hurtFadeOutTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedActivate = Mathf.Lerp(Activate_ON, 0f, (elapsedTime / _hurtFadeOutTime));

            _material.SetFloat(_blend, lerpedActivate);

            yield return null;
        }

        stopShader();
    }

    private void stopShader()
    {
        _fullScreenDamage.SetActive(false);
    }
}
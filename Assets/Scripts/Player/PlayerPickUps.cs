using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(PlayerModel))]
public class PlayerPickUps : MonoBehaviour
{
    private bool _isMagnetActive = false;
    private bool _isInvisActive = false;
    private Action ActiveFixedEffects = delegate { };
    private Action ActiveEffects = delegate { };
    private PlayerModel _agent;

    [SerializeField] private float _pickUpRange = 40f;
    [SerializeField] LayerMask _comsumableMask;

    [Header("Magnet Settings")]
    [SerializeField] private float _magnetSphere = 100f;
    [SerializeField] private LayerMask _magnetMask;
    [SerializeField] private float _magnetDuration = 10f;
    public AudioSource getMagnet;
    public AudioSource magnet;

    [Header("Invincibility Settings")]
    [SerializeField] private float _invisDuration = 5f;

    [Header("Crystals Node Settings")]
    [SerializeField] private int _crystalsAmount = 25;
    public AudioSource crystalBoxAudio;
    public ParticleSystem crystalBoxExploscion;

    private void Start()
    {
        _agent = GetComponent<PlayerModel>();
    }

    private void Update()
    {
        ActiveEffects();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, _pickUpRange, transform.forward, out hit, _pickUpRange, _comsumableMask))
        {
            TriggerEffect(hit.transform.GetComponent<Comsumable>().myEffect);
            Destroy(hit.transform.gameObject);
        }
        ActiveFixedEffects();
    }

    #region Effects Methods

    private void MagnetEffect()
    {
        RaycastHit hit;
        if (Physics.SphereCast(_agent.rb.position, _magnetSphere, transform.forward, out hit, _magnetSphere, _magnetMask))
        {
            Debug.Log("HIT");
            hit.transform.GetComponent<Crystal>().PickUp();
        }
    }

    #endregion

    #region Trigger Effect Methods

    public void TriggerEffect(PlayerEffects effect)
    {
        switch (effect)
        {
            case PlayerEffects.Magnet:
                TriggerMagnetEffect(_magnetDuration);
                break;
            case PlayerEffects.Invincibility:
                TriggerInvincibilityEffect(_invisDuration);
                break;
            case PlayerEffects.Crystals:
                TriggerCrystalsEffect(_crystalsAmount);
                break;
        }
    }

    private void TriggerMagnetEffect(float duration)
    {
        if (_isMagnetActive) return;
        StartCoroutine(MagnetProccess(duration));
    }

    private void TriggerCrystalsEffect(int amount)
    {
        GameManager.Instance.Crystals += amount;
        crystalBoxAudio.Play();
        crystalBoxExploscion.Play();
    }

    private void TriggerInvincibilityEffect(float duration)
    {
        StartCoroutine(InvincibilityProccess(duration));
    }
    #endregion

    private IEnumerator MagnetProccess(float duration)
    {
        getMagnet.Play();
        magnet.Play();
        _isMagnetActive = true;
        _agent.magnetActive = true;
        ActiveFixedEffects += MagnetEffect;
        yield return new WaitForSeconds(duration);
        magnet.Stop();
        ActiveFixedEffects -= MagnetEffect;
        _agent.magnetActive = false;
    }

    private IEnumerator InvincibilityProccess(float duration)
    {
        _isInvisActive = true;
        _agent.isInvincible = true;
        yield return new WaitForSeconds(duration);
        _agent.isInvincible = false;
        _isInvisActive = false;
    }
}

public enum PlayerEffects
{
    Magnet,
    Invincibility,
    Crystals
}

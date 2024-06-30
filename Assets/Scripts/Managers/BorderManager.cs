using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderManager : MonoBehaviour
{
    [HideInInspector] public List<Border> borderList = new List<Border>();
    public static BorderManager Instance;
    private bool _isCounting = false;
    [SerializeField] private float _time = 5f;

    private void Awake()
    {
        if(Instance == null)
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
        foreach(Border item in borderList)
        {
            if (item.collision)
            {
                status = true;
            }
        }
        if(status && !_isCounting)
        {
            StartCoroutine(CountDown(_time));
        }
        if (!status)
        {
            StopAllCoroutines();
            _isCounting = false;
        }
    }

    IEnumerator CountDown(float t)
    {
        _isCounting = true;
        yield return new WaitForSeconds(t);
        GameManager.Instance.ForcePlayerBackToBounds();
    }
}

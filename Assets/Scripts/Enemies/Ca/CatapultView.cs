using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultView
{
    Animator _anim;
    AudioSource _onReloadInstance;
    AudioSource _onBomInstance;

    public CatapultView(Catapult a)
    {
        _anim = a.anim;
        a.OnFire += OnFire;
        a.OnReload += OnReload;
        _onReloadInstance = a._onReload;
        _onBomInstance = a._bomCatapult;
    }

    private void OnReload()
    {
        _onReloadInstance.Play();
        _anim.Play("Recarga");
    }

    private void OnFire()
    {

        _onReloadInstance.Stop();
        _anim.Play("Disparo nuevo");
    }
}

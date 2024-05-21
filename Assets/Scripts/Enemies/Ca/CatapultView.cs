using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultView
{
    Animator _anim;

    public CatapultView(Catapult a)
    {
        _anim = a.anim;
        a.OnFire += OnFire;
        a.OnReload += OnReload;
    }

    private void OnReload()
    {
        _anim.Play("Recarga");
    }

    private void OnFire()
    {
        _anim.Play("Disparo");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatRandom : MonoBehaviour
{
    private Animator anim;
    IEnumerator Start()
    {
        anim = GetComponent<Animator>();

        while (true) 
        {
            yield return new WaitForSeconds(Random.Range(3, 7));

            anim.SetInteger("Idle Index", Random.Range(0, 7));
            anim.SetTrigger("Change");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

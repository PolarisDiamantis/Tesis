using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    public float parallaxStrength = 1f;  
    private Vector3 lastMousePosition;

    void Start()
    {
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
        Vector3 parallaxOffset = new Vector3(mouseDelta.x, mouseDelta.y, 0) * parallaxStrength * Time.deltaTime;

        transform.position += parallaxOffset;

        lastMousePosition = Input.mousePosition;
    }





}

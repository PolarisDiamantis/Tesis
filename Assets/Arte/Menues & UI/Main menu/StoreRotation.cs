using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreRotation : MonoBehaviour
{
    // Adjust sensitivity to control how fast the model rotates
    public float rotateSpeed = 5f;

    private Vector3 dragOrigin;
    private bool isDragging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Capture the mouse position when the drag begins
            dragOrigin = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // Stop dragging when the mouse button is released
            isDragging = false;
        }

        if (isDragging)
        {
            // Calculate the difference in mouse position
            Vector3 mousePos = Input.mousePosition;
            float deltaX = (mousePos.x - dragOrigin.x) * rotateSpeed * Time.deltaTime;

            // Rotate the model around its own y-axis
            transform.Rotate(Vector3.up, -deltaX, Space.World);

            // Update the drag origin for the next frame
            dragOrigin = mousePos;
        }
    }
}














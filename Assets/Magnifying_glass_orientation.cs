using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyingGlassLens : MonoBehaviour
{
    public Transform vrCamera;  // Assign the VR headset (Main Camera)
    public Transform magnifyingGlassBody; // Assign the magnifying glass body
    public Camera magnificationCamera;  // Assign the Magnification Camera

    void Start()
    {
        if (vrCamera == null || magnifyingGlassBody == null || magnificationCamera == null)
        {
            Debug.LogError("Missing references on: " + gameObject.name);
            return;
        }
    }

    void Update()
    {
        if (vrCamera == null || magnifyingGlassBody == null || magnificationCamera == null)
            return;

        // Keep the magnification camera at the center of the magnifying glass lens
        magnificationCamera.transform.position = transform.position;

        // Make the magnification camera match the VR camera's orientation
        magnificationCamera.transform.rotation = Quaternion.LookRotation(vrCamera.forward, magnifyingGlassBody.up);
    }
}

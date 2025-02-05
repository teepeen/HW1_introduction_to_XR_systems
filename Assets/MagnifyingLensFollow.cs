using UnityEngine;

public class MagnifyingLensFollow : MonoBehaviour
{
    public Camera playerCamera;          // Reference to the player's camera
    public Camera magnifyingCamera;      // Reference to the magnifying camera (lens view)
    public float offsetDistance = 0.3f;  // Distance from the player camera to the magnifying camera
    public Vector3 offsetRotation = new Vector3(0, 0, 0); // Rotation offset (if needed)

    void Update()
    {
        // Update the position of the magnifying camera based on player's camera position
        magnifyingCamera.transform.position = playerCamera.transform.position + playerCamera.transform.forward * offsetDistance;

        // Optionally, apply a rotation offset to the magnifying camera (if needed)
        magnifyingCamera.transform.rotation = playerCamera.transform.rotation * Quaternion.Euler(offsetRotation);
    }
}
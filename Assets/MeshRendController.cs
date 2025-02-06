using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;  // For coroutines

public class GrabbableBodyControl : MonoBehaviour
{
    public XRGrabInteractable grabbableBody;  // Reference to the grabbable body
    public MeshRenderer lensMeshRenderer;  // Reference to the lens MeshRenderer

    public float delayBeforeTurningOff = 0.5f;  // Delay time in seconds before turning off the lens

    private bool isLensVisible = false;  // Track the visibility state of the lens

    //public void GetLocalPositionAndRotation(out Vector3 localPosition, out Quaternion localRotation);
    void Start()
    {
        // Ensure the lens's MeshRenderer is disabled at the start (invisible)
        if (lensMeshRenderer != null)
        {
            lensMeshRenderer.enabled = false; // Start with the lens invisible
            isLensVisible = false; // Initialize visibility state as false
        }

        // Register for the grabbable body events
        if (grabbableBody != null)
        {
            grabbableBody.selectEntered.AddListener(OnGrabbed);
            grabbableBody.selectExited.AddListener(OnReleased);
        }
    }

    // Called when the grabbable body is grabbed
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        // Toggle the visibility of the lens immediately
        ToggleLensVisibility(true);
    }

    // Called when the grabbable body is released
    private void OnReleased(SelectExitEventArgs args)
    {
        // Start the delay to toggle the lens off after release
        StartCoroutine(ToggleLensOffWithDelay());
    }

    // Toggle the lens visibility state (true to show, false to hide)
    private void ToggleLensVisibility(bool state)
    {
        if (lensMeshRenderer != null)
        {
            lensMeshRenderer.enabled = state; // Show or hide the lens based on the state
            isLensVisible = state; // Update the visibility state
        }
    }

    // Coroutine to wait for a specified delay before turning off the lens
    private IEnumerator ToggleLensOffWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeTurningOff);

        // Only disable if the lens is currently visible
        if (lensMeshRenderer != null && isLensVisible)
        {
            ToggleLensVisibility(false);  // Disable the lens MeshRenderer after the delay
        }
    }

    // Clean up event listeners when the object is destroyed
    private void OnDestroy()
    {
        if (grabbableBody != null)
        {
            grabbableBody.selectEntered.RemoveListener(OnGrabbed);
            grabbableBody.selectExited.RemoveListener(OnReleased);
        }
    }
}
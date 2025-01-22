using UnityEngine;
using UnityEngine.InputSystem;

public class ChangeLightColor : MonoBehaviour
{
    public Light pointLight; // Reference to the point light
    public Color[] colors; // Array of colors to cycle through
    private int currentIndex = 0; // Tracks the current color index

    public InputActionReference action; // Input Action Reference for the button press

    void Start()
    {
        // Enable the input action
        action.action.Enable();

        // Add listener for button press
        action.action.performed += OnButtonPressed;
    }

    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        // Change the light color to the next in the array
        if (colors.Length > 0 && pointLight != null)
        {
            currentIndex = (currentIndex + 1) % colors.Length;
            pointLight.color = colors[currentIndex];
        }
    }

    private void OnDestroy()
    {
        // Clean up the listener when the object is destroyed
        action.action.performed -= OnButtonPressed;
    }
}
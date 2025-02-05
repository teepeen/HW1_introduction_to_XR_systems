using UnityEngine;
using UnityEngine.InputSystem;

public class LeftControllerYButton : MonoBehaviour
{
    public InputActionReference yButtonAction; // Reference to the Y button action

    void Start()
    {
        // Enable the action
        yButtonAction.action.Enable();

        // Subscribe to the action's performed event
        yButtonAction.action.performed += OnYButtonPressed;
    }

    private void OnYButtonPressed(InputAction.CallbackContext context)
    {
        // Logic for when the Y button is pressed
        Debug.Log("Y button on the left controller was pressed!");
    }

    private void OnDestroy()
    {
        // Unsubscribe when the object is destroyed
        yButtonAction.action.performed -= OnYButtonPressed;
    }
}
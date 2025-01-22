using UnityEngine;
using UnityEngine.InputSystem;

public class LeftControllerXButton : MonoBehaviour
{
    public InputActionReference xButtonAction; // Reference to the X button action

    void Start()
    {
        // Enable the action
        xButtonAction.action.Enable();

        // Subscribe to the action's performed event
        xButtonAction.action.performed += OnXButtonPressed;
    }

    private void OnXButtonPressed(InputAction.CallbackContext context)
    {
        // Logic for when the X button is pressed
        Debug.Log("X button on the left controller was pressed!");
    }

    private void OnDestroy()
    {
        // Unsubscribe when the object is destroyed
        xButtonAction.action.performed -= OnXButtonPressed;
    }
}

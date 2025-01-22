using UnityEngine;
using UnityEngine.InputSystem;

public class Change_player_position : MonoBehaviour
{
    public Transform player; // The player's transform
    public Transform roomPosition; // Transform for the room position
    public Transform externalPosition; // Transform for the external viewing point
    public InputActionReference action; // Input Action Reference for toggling positions

    private bool isInRoom = true; // Tracks whether the player is in the room

    void Start()
    {
        // Enable the input action
        action.action.Enable();

        // Add listener for the button press
        action.action.performed += OnButtonPressed;

        // Ensure the player starts in the room
        if (player != null && roomPosition != null)
        {
            player.position = roomPosition.position;
            player.rotation = roomPosition.rotation;
        }
    }

    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        if (player == null || roomPosition == null || externalPosition == null)
        {
            Debug.LogWarning("Player, roomPosition, or externalPosition not assigned.");
            return;
        }

        // Toggle position
        if (isInRoom)
        {
            // Move to the external viewing point
            player.position = externalPosition.position;
            player.rotation = externalPosition.rotation;
        }
        else
        {
            // Move back to the room
            player.position = roomPosition.position;
            player.rotation = roomPosition.rotation;
        }

        // Flip the state
        isInRoom = !isInRoom;
    }

    private void OnDestroy()
    {
        // Clean up the listener when the object is destroyed
        action.action.performed -= OnButtonPressed;
    }
}
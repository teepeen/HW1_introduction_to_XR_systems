using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public XRController leftController; // Assign in Inspector
    public InputActionProperty moveInput; // Assign in Inspector
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector2 input = moveInput.action.ReadValue<Vector2>();
        Vector3 direction = new Vector3(input.x, 0, input.y);

        // Move relative to headset direction
        direction = Camera.main.transform.TransformDirection(direction);
        direction.y = 0; // Keep movement horizontal

        characterController.Move(direction * speed * Time.deltaTime);
    }
}

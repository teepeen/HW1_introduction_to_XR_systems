using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    // This script should be attached to both controller objects in the scene
    // Make sure to define the input in the editor (LeftHand/Grip and RightHand/Grip recommended respectively)
    CustomGrab otherHand = null;
    public List<Transform> nearObjects = new List<Transform>();
    public Transform grabbedObject = null;
    public InputActionReference action;
    bool grabbing = false;

    // Store the previous position and rotation to calculate delta
    private Vector3 previousPosition;
    private Quaternion previousRotation;

    private void Start()
    {
        action.action.Enable();

        // Find the other hand
        foreach (CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this)
                otherHand = c;
        }
    }

    void Update()
    {
        grabbing = action.action.IsPressed();
        if (grabbing)
        {
            Debug.Log("Grip button pressed!");
            Debug.Log("nearObjects count: " + nearObjects.Count); // Add this to confirm nearObjects has items.
            foreach (Transform obj in nearObjects)
            {
                Debug.Log("Object in nearObjects while pressing the grab button: " + obj.name);
            }

            // Grab nearby object or the object in the other hand
            if (!grabbedObject)
            {
                //grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand.grabbedObject;
                if (nearObjects.Count > 0)
                {
                    grabbedObject = nearObjects[0]; // Grab the first object in the list.
                    Debug.Log("Object grabbed: " + grabbedObject.name);
                }
                else if (otherHand.grabbedObject != null)
                {
                    grabbedObject = otherHand.grabbedObject; // Grab object from other hand if it's already being held there.
                    Debug.Log("Grabbed object from other hand: " + grabbedObject.name);
                }
                else
                {
                    Debug.Log("No object assigned to grabbedObject.");
                }
                if (grabbedObject != null)
                {
                    Debug.Log("Object grabbed: " + grabbedObject.name);
                }
                else
                {
                    Debug.Log("No object assigned to grabbedObject.");
                }
            }
            if (grabbedObject)
            {
                // Calculate the local position relative to the controller
                Vector3 localPosition = grabbedObject.transform.position - transform.position;

                // Calculate the delta position (movement of the controller)
                Vector3 deltaPosition = transform.position - previousPosition;

                // Calculate the delta rotation (rotation of the controller)
                Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);

                // Apply rotation only to the relative position of the object (rotate around the controller's origin)
                Vector3 rotatedLocalPosition = deltaRotation * localPosition;

                // Update the position to be the new position relative to the controller
                grabbedObject.transform.position += deltaPosition + rotatedLocalPosition - localPosition;
                grabbedObject.transform.rotation = deltaRotation * grabbedObject.transform.rotation;

                // If the other hand is also grabbing, calculate its delta position and delta rotation
                if (otherHand.grabbedObject == grabbedObject)
                {
                    Vector3 localOtherHandPosition = grabbedObject.transform.position - otherHand.transform.position;
                    Vector3 deltaOtherHandPosition = otherHand.transform.position - otherHand.previousPosition;
                    Quaternion deltaOtherHandRotation = otherHand.transform.rotation * Quaternion.Inverse(otherHand.previousRotation);

                    Vector3 rotatedOtherhandLocalPosition = deltaOtherHandRotation * localOtherHandPosition;
                
                    // Apply the combined delta to the grabbed object's position and rotation
                    grabbedObject.transform.position += deltaPosition + deltaOtherHandPosition + rotatedLocalPosition + rotatedOtherhandLocalPosition - localPosition - localOtherHandPosition;
                    grabbedObject.transform.rotation = deltaRotation * deltaOtherHandRotation * grabbedObject.transform.rotation;
                }

                
            }
            Debug.Log("nearObjects count after grabbing logic: " + nearObjects.Count); // Debugging step
        }
    
        // If let go of button, release object
        else if (grabbedObject)
            {   
            grabbedObject = null;
            }
        // Should save the current position and rotation here
        // Save the current position and rotation for the next frame
        previousPosition = transform.position;
        previousRotation = transform.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Make sure to tag grabbable objects with the "grabbable" tag
        // You also need to make sure to have colliders for the grabbable objects and the controllers
        // Make sure to set the controller colliders as triggers or they will get misplaced
        // You also need to add Rigidbody to the controllers for these functions to be triggered
        // Make sure gravity is disabled though, or your controllers will (virtually) fall to the ground

        Transform t = other.transform;
        if (t && t.tag.ToLower() == "grabbable")
        //if (t && t.CompareTag("grabbable"))
        {
            nearObjects.Add(t);
            Debug.Log("Added to nearObjects: " + t.name);
            Debug.Log("nearObjects contains " + nearObjects.Count + " objects.");
            foreach (Transform obj in nearObjects)
            {
                Debug.Log("Object in nearObjects while OnTriggerEnter: " + obj.name);
            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        Transform t = other.transform;
        if (t && t.tag.ToLower() == "grabbable")
        //if (t && t.CompareTag("grabbable"))
        {

            nearObjects.Remove(t);
            Debug.Log("Removed from nearObjects: " + t.name);

        }
    }
}
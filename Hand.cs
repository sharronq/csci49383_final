using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/*
    Class placed on hand components in VR controller to enable object interaction with Vive headset and SteamVR.
*/
public class Hand : MonoBehaviour
{
    public SteamVR_Action_Boolean grabAction = null;

    private SteamVR_Behaviour_Pose pose = null;
    private FixedJoint joint = null;

    private Interactable currentInteractable = null;
    public List<Interactable> contactInteractables = new List<Interactable>();

    void Start()
    {
        pose = GetComponent<SteamVR_Behaviour_Pose>();
        joint = GetComponent<FixedJoint>();
    }

    void Update()
    {
        // If user squeezes trigger, run Pickup method to grab object. 
        if (grabAction.GetStateDown(pose.inputSource)) 
        {
            Pickup();
        }

        // Once use releases trigger, run Drop method to drop object.
        if (grabAction.GetStateUp(pose.inputSource)) 
        {
            Drop();
        }
    }

    // If trigger squeezed while colliding with an object
    private void OnTriggerEnter(Collider other)
    {
        // Check if object is tagged as "Interactable" - cease method if not
        if (!other.gameObject.CompareTag("Interactable"))
            return;

        // If returnable, add game object script "Interactable" to list of interactables that have been contacted
        contactInteractables.Add(other.gameObject.GetComponent<Interactable>());
    }
    
    // If trigger released while colliding with an object
    private void OnTriggerExit(Collider other)
    {
        // Check if object is tagged as "Interactable" - cease method if not
        if (!other.gameObject.CompareTag("Interactable"))
            return;

        // If returnable, remove game object script "Interactable" to list of interactables that have been contacted (no longer holding)
        contactInteractables.Remove(other.gameObject.GetComponent<Interactable>());

    }

    public void Pickup()
    {
        // Get nearest interactable object
        currentInteractable = GetNearestInteractable();

        // Don't try to pick up if there is nothing there
        if (!currentInteractable) 
            return;

        // Check if an object is already being held - if yes, drop
        if (currentInteractable.activeHand)
            currentInteractable.activeHand.Drop();

        // Change position of interactable to hand position
        currentInteractable.transform.position = transform.position;

        // Connect interactable to hand
        Rigidbody target_body = currentInteractable.GetComponent<Rigidbody>();
        joint.connectedBody = target_body;

        // Set active hand in Interactable script to whichever hand is holding the object
        currentInteractable.activeHand = this; 


    }

    public void Drop()
    {
        // If nothing in hand, cease method
        if (!currentInteractable) 
            return;
    
        // Application of velocity to object drop
        Rigidbody target_body = currentInteractable.GetComponent<Rigidbody>();
        target_body.velocity = pose.GetVelocity();
        target_body.angularVelocity = pose.GetAngularVelocity();

        // Detach object from hand by removing joint
        joint.connectedBody = null;

        // Clear active hand and current interactable (no longer holding anything)
        currentInteractable.activeHand = null;
        currentInteractable = null;
    }

    private Interactable GetNearestInteractable()
    {
        Interactable nearest = null;
        float min_distance = float.MaxValue;
        float distance = 0.0f;

        // Find closest interactable by calculating distance
        foreach(Interactable interactable in contactInteractables) {
            distance = (interactable.transform.position - transform.position).sqrMagnitude; 
            if (distance < min_distance) {
                min_distance = distance;
                nearest = interactable;
            }
        }

        return nearest;
    }
}

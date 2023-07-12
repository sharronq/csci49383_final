using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRController : MonoBehaviour
{
    public float Gravity = 30.0f;
    public float RotationIncrement = 90;

    public SteamVR_Action_Boolean RotatePress = null;
    public float Sensitivity = 0.1f;
    public float MaxSpeed = 1.0f;

    public SteamVR_Action_Boolean MovePress;
    public SteamVR_Action_Vector2 MoveValue;

    private float Speed = 0.0f;
    private CharacterController CharacterController;
    private Transform CameraRig = null;
    private Transform Head = null;

    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>(); // Gets character controller component as script is being loaded
    }

    private void Start()
    {
        CameraRig = SteamVR_Render.Top().origin;
        Head = SteamVR_Render.Top().head;
    }

    // Update is called once per frame
    private void Update()
    {
        // Movement, height, and head position are calculated and updated every frame
        HandleHeight();
        CalculateMovement();
        // StartWalking(player);
        SnapRotation();
    }

    private void HandleHeight() // Resize height of character controller based on position of user headset
    {
        // Get position of user's head, capped between 1 and 2 meters
        float headHeight = Mathf.Clamp(Head.localPosition.y, 1, 2);
        CharacterController.height = headHeight; // Set character controller height to user head height

        // Find center (pivot)
        Vector3 newCenter = Vector3.zero; // Initialize
        newCenter.y = CharacterController.height / 2; // Calculate center (half of head height)
        newCenter.y += CharacterController.skinWidth; // Add skin width

        // Move character controller capsule based on position in local space
        newCenter.x = Head.localPosition.x;
        newCenter.z = Head.localPosition.z;

        // Changes center of character controller based on height
        CharacterController.center = newCenter;
    }

    private void CalculateMovement() // Detects input on controller and initiates movement
    {
        Quaternion orientation = CalculateOrientation();
        Vector3 movement = Vector3.zero;

        // When user releases touchpad (not moving) set speed to 0. Otherwise, allow movement
        if (MoveValue.axis.magnitude == 0)
        {    
            Speed = 0;
            // animator.SetBool(isWalkingHash, false);
        }

        // animator.SetBool(isWalkingHash, true);

        // Add speed to character controller and clamp value
        Speed += MoveValue.axis.magnitude * Sensitivity; // Calculates speed based on controller sensitivity
        Speed = Mathf.Clamp(Speed, -MaxSpeed, MaxSpeed); // Puts a cap on speed in both directions

        // Controls movement based on user orientation
        movement += orientation * (Speed * Vector3.forward);

        // Application of gravity (gravity is multiplied by Time.deltaTime twice as gravity is applied as an acceleration ms^2)
        movement.y -= Gravity * Time.deltaTime;

        // Application of movement to character controller
        CharacterController.Move(movement * Time.deltaTime);
    }

    private Quaternion CalculateOrientation() {
        float rotation = Mathf.Atan2(MoveValue.axis.x, MoveValue.axis.y); // Calculates rotation in radians based on finger position on touch pad
        rotation *= Mathf.Rad2Deg; // Converting rotation from radians to degrees

        // Calculate movement orientation (which direction user is facing)
        Vector3 orientationEuler = new Vector3(0, Head.eulerAngles.y + rotation, 0);
        return Quaternion.Euler(orientationEuler);
    }

    private void SnapRotation() {
        float snapValue = 0.0f;

        if (RotatePress.GetStateDown(SteamVR_Input_Sources.LeftHand))
            snapValue = -Mathf.Abs(RotationIncrement);
        if (RotatePress.GetStateDown(SteamVR_Input_Sources.RightHand))
            snapValue = Mathf.Abs(RotationIncrement);

        transform.RotateAround(Head.position, Vector3.up, snapValue);

    }

}

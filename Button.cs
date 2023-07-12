using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*
    Class to be placed on button game object that implements button animation. 
    Button functions are implemented in their respective classes.
*/
public class Button : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    AudioSource sound;
    bool isPressed;
    Vector3 buttonPos;

    void Start()
    {
        sound = GetComponent<AudioSource>(); // Grabs button press sound from audio source on object
        isPressed = false; 
        buttonPos = button.transform.localPosition;
    }

    // On button trigger when colliding with object
    private void OnTriggerEnter(Collider other) {
        if (!isPressed){ // If button is not already being pressed
            button.transform.localPosition = new Vector3(buttonPos.x, buttonPos.y - .02f, buttonPos.z); // Transform button to pressed position
            presser = other.gameObject; // Store object that interacted with button
            onPress.Invoke(); // Invoke Unity onPress method
            sound.Play(); // Play button press sound
            isPressed = true;
        }
    }

    // When button stops colliding with object
    private void OnTriggerExit(Collider other) {
        if (other.gameObject == presser) { // If the object that exits collision is the same one that entered
            button.transform.localPosition = buttonPos; // Reset button position to original position
            onRelease.Invoke(); // Invoke Unity onRelease method
            isPressed = false;
        }
    }

    // Test class that spawns spheres once button is pressed
    public void SpawnSphere() {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        sphere.transform.localPosition = new Vector3(0, 1, 2);
        sphere.AddComponent<Rigidbody>();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Script to be placed on objects to make them interactable with Vive headset and SteamVR.
*/
[RequireComponent(typeof(Rigidbody))]
public class Interactable : MonoBehaviour
{
    [HideInInspector]
    public Hand activeHand = null; // if object is being held, stores the hand it is being held in

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/*
    (INACTIVE) Class that changes animated character to walking animation when user is moving
*/
public class AnimationStateController : MonoBehaviour
{
    Animator animator;
    bool moving;
    public SteamVR_Action_Vector2 touchpadAction;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        bool isWalking = animator.GetBool("IsWalking");
        Vector2 touchpadValue = touchpadAction.GetAxis(SteamVR_Input_Sources.Any);
        moving = touchpadValue != Vector2.zero;
        if(moving && !isWalking) 
        {
            animator.SetBool("IsWalking", true);
        }
        if(!moving && isWalking) {
            animator.SetBool("IsWalking", false);
        }
    }
}

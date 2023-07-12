using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class footstepController : MonoBehaviour
{
    public SteamVR_Action_Vector2 touchpadAction;

    public AudioSource source;
    public List<AudioClip> steps = new List<AudioClip>();
    
    private enum Surface{floor};
    private Surface surface;
    private List<AudioClip> currentList;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void SelectStepList() {
        switch (surface)
        {
            case Surface.floor:
                currentList = steps;
                break;
            default:
                currentList = null;
                break;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.transform.tag == "Floor") {
            surface = Surface.floor;
        }
        SelectStepList();
    }

    // Update is called once per frame
    // void Update()
    // { 
    //     float wait_time = 4;
    //     float counter = 0;
    //     Vector2 touchpadValue = touchpadAction.GetAxis(SteamVR_Input_Sources.Any);
    //     if (touchpadValue != Vector2.zero)
    //     {
    //         while(counter < wait_time) {
    //             counter += Time.deltaTime;
    //         }
    //         PlayStep();
    //     } 
    // }
    
    public void PlayStep() 
    {
        if(currentList == null) {
            return;
        }
        AudioClip clip = steps[Random.Range(0, steps.Count)];
        source.PlayOneShot(clip); 
    }

    // private void OnTriggerEnter(Collider other){
    //     AudioClip clip = steps[Random.Range(0, steps.Count)];
    //     if(other.CompareTag(targetTag)) 
    //     {
    //         source.PlayOneShot(clip);
    //     }
    // }
}

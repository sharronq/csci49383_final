using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    Class placed on music player game object to play music and mouse animation when button is pressed.
*/
public class PlayMusic : MonoBehaviour
{
    public AudioSource source;
    public AudioClip music;
    private bool musicPlaying = false;

    Animator animator;
    public GameObject mouse;

    void Start()
    {
        // Grabs animator from mouse game object
        animator = mouse.gameObject.GetComponent<Animator>();
    }
    public void Play() 
    {
        // Random integer to randomize mouse dance
        int rand = Random.Range(1, 4);

        // Play music if music isn't already playing
        if (!musicPlaying) {
            source.PlayOneShot(music);
            source.PlayScheduled(AudioSettings.dspTime + music.length); // Loop song

            // Set variables to indicate music is playing, mouse begins dancing
            musicPlaying = true;
            animator.SetBool("MusicPlaying", true);
            animator.SetInteger("DanceNumber", rand);

        } else { // If music is playing and button is pressed
            source.Stop(); // Stop music
            musicPlaying = false;
            animator.SetBool("MusicPlaying", false); // Cease mouse animation
        }
    }

}

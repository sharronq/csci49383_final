using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    Script placed on trash can collider to implement an interactive basketball game.
*/
public class BasketballGame : MonoBehaviour
{
    public GameObject table;
    public GameObject ball;
    public int score = 0;
    private Vector3 newPosition;

    [SerializeField] private Text title;

    void Start()
    {
        newPosition = table.transform.position; // Sets respawn point of ball
    }
    
    // Text is updated every frame to reflect score count
    void Update()
    {
        SpawnText();
    }

    void OnTriggerEnter(Collider other) 
    {
        // Checks if object thrown into the trash can is interactable (can be picked up)
        if (other.gameObject.CompareTag("Interactable"))
        {  
            other.gameObject.transform.position = newPosition; // Moves ball to table
            ball.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; // Stops ball from moving once it respawns
            score += 1; // Increases score by one
        }
    }

    void SpawnText()
    {
        title.text = "Score: " + score; // Text changes to update score value every frame
    }

    public void RestartButton() 
    {
        score = 0; // Score is reset to 0 to restart game
    }

    public void RespawnBall() // Forces respawn of ball without updating score, in case ball rolls out of bounds
    {
        ball.gameObject.transform.position = newPosition; // Moves ball to table\
        ball.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; // Stops ball from moving once it respawns
    }


}

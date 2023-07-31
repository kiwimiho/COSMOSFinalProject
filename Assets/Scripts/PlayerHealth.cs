using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Components attached to gameobject we want to use a lot      
    public Vector3 spawnPoint;  
    UnityEngine.AI.NavMeshAgent agent;  

    //Making these public so we can watch them change in the designer
    public static int maxHealth = 10;                                       //Initial health of the player
    public static int health = 10;                                          //Current health of the player (when it goes to 0, respawn)

    // Start is called before the first frame update
    void Start()
    {
        //set spawnpoint
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        spawnPoint = transform.position; 

        //Fetch the PlayerClickMove component from the GameObject

        //Set health to maxHealth
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //Do nothing, it's flying with physics!
    }

    //Called upon collision with another GameObject
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.name);

        //Check to see if the player has been hit by a paint ball
        if (collision.gameObject.tag == "Enemy")
        {
            //Remove the bullet that just hit the player
            // Destroy(collision.gameObject);

            //Subtract health
            health -= 1;

            //Check to see if health dropps to 0 (or lower)
            if (health <= 0)
            {
                //Health dropped to 0, respawn the character
                Respawn();

                //Set player health back to maximum
                health = maxHealth;
            }
        }
    }

    public void Respawn()
    {
        //Send player to spawn point
        transform.position = spawnPoint;

        //End movement
        agent.ResetPath();
    }
}

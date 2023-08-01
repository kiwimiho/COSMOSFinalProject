using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    //Components attached to gameobject we want to use a lot      
    public Vector3 spawnPoint;  
    Rigidbody rigidBody;
    PlayerMovement playerMov; 
    public GameObject deathScreen;

    public GameObject pModel;
    Animator rat;

    float knockback = 20f;

    //Making these public so we can watch them change in the designer
    public static int maxHealth = 10;                                       //Initial health of the player
    public static int health = 10;                                          //Current health of the player (when it goes to 0, respawn)

    // Start is called before the first frame update
    void Start()
    {
        deathScreen.SetActive(false);

        //set spawnpoint
        spawnPoint = transform.position;
        rigidBody = GetComponent<Rigidbody>();
        playerMov = GetComponent<PlayerMovement>();
        rat = pModel.GetComponent<Animator>();

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && playerMov.controlLockTimer <= 0)
        {
            Destroy(other.gameObject);
            rigidBody.AddForce(transform.up * knockback, ForceMode.Impulse);
        }
    }
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
            rigidBody.AddForce(transform.forward * -knockback, ForceMode.Impulse);
            rigidBody.AddForce(transform.up * (knockback/2), ForceMode.Impulse);
            rat.SetTrigger("hurt");
            playerMov.controlLockTimer = 2 * Time.deltaTime;

            //Check to see if health dropps to 0 (or lower)
            if (health <= 0)
            {
                deathScreen.SetActive(true);
            }
        }
    }

    public void Respawn()
    {
        //Send player to spawn point
        transform.position = spawnPoint;
        health = maxHealth;
        deathScreen.SetActive(false);
    }
}
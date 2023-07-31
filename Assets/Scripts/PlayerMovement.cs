using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Components attached to gameobject we want to use a lot
    Rigidbody rigidBody; //Rigidbody component used for jumping (it's physics!!!)

    //Making these public so we can watch them change in the designer
    float moveSpeed = 5;        //How fast the player moves
    float rotationSpeed = 100;   //How fast the player turns left/right
    float distToGround;
    float speedCap = 30;
    float jumpForce = 600;      //How much force to use when the player jumps
    bool isJumping = false;     //True/false flag indicating if the player is already jumping
    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        rigidBody = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Check for each of the movement keys to see if the player is currently pressing them.
        //Using game standard WASD + space. Could be changed to use arrow keys (or some other combo)
        if (Input.GetKey(KeyCode.W))
        {
            //W - move forward along the player's forward direction.
            //Always multiply by deltaTime when moving as Update() runs 30+ frames per sec
            rigidBody.AddForce(transform.forward * moveSpeed, ForceMode.Acceleration);
        }   
        if (Input.GetKey(KeyCode.A))
        {
            //A - turn left
            transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.S))
        {
            //S - move backwards along the player's negative forward direction.
            rigidBody.AddForce(transform.forward * -moveSpeed, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //D - turn right by rotating the player along the Y axis in the positive direction
            transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //It can be helpful when testing to print out debug statements to the Console window.

            //No double jumps, don't allow jumping if already jumping
            if (!isJumping && IsGrounded())
            {
                //Set the jumping flag to prevent double jumps
                isJumping = true;

                //Apply a force to this Rigidbody in direction of this GameObjects up axis
                rigidBody.AddForce(transform.up * jumpForce);
            }
        }
        if (rigidBody.velocity.magnitude > speedCap) //make sure no movement is too fast
        {
            rigidBody.velocity = rigidBody.velocity.normalized * speedCap;
        }
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Check to see if the player has touched the GameOver portal
        if (collision.gameObject.name == "GameOver")
        {
            //Player touched the portal, display win msg (in future we will print to GUI)
            Debug.Log("Player wins!!");
        }

        //Collision with any object lets the player jump again.
        isJumping = false;
    }
}

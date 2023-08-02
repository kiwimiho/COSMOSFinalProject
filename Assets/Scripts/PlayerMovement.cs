using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Components attached to gameobject we want to use a lot
    Rigidbody rigidBody; //Rigidbody component used for jumping (it's physics!!!)

    //Making these public so we can watch them change in the designer
    float moveSpeed = 50;        //How fast the player moves
    float rotationSpeed = 150;   //How fast the player turns left/right
    float distToGround;
    float speedCap = 100;
    float jumpForce = 900;      //How much force to use when the player jumps
    bool isJumping = false;     //True/false flag indicating if the player is already jumping
    bool hasStomped = false;
    public float controlLockTimer = 0f;
    
    public GameObject pModel; 

    Animator rat;

    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        rigidBody = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        rat = pModel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Check for each of the movement keys to see if the player is currently pressing them.
        //Using game standard WASD + space. Could be changed to use arrow keys (or some other combo)
        if (controlLockTimer > 0)
        {
            controlLockTimer -= Time.deltaTime;
        }
        if (IsGrounded())
        {
            rigidBody.drag = 1;
            rat.ResetTrigger("jump");
        }
        else
        {
            rigidBody.drag = 0.8f;
            rat.ResetTrigger("stop");
            rat.ResetTrigger("turn");
            rat.ResetTrigger("runStart");
            rat.ResetTrigger("runBack");
            rat.SetTrigger("jump");
        }
        if (Input.GetKey(KeyCode.W) && controlLockTimer <= 0)
        {
            //W - move forward along the player's forward direction.
            //Always multiply by deltaTime when moving as Update() runs 30+ frames per sec
            rigidBody.AddForce(transform.forward * moveSpeed, ForceMode.Acceleration);
            rat.ResetTrigger("stop");
            rat.SetTrigger("runStart");
        }   
        if (Input.GetKey(KeyCode.A) && controlLockTimer <= 0)
        {
            //A - turn left
            transform.Rotate(new Vector3(0, -rotationSpeed * Time.deltaTime, 0));
            rat.ResetTrigger("stop");
            rat.SetTrigger("turn");
        }
        if (Input.GetKey(KeyCode.S) && controlLockTimer <= 0)
        {
            //S - move backwards along the player's negative forward direction.
            rigidBody.AddForce(transform.forward * -moveSpeed, ForceMode.Acceleration);
            rat.ResetTrigger("stop");
            rat.SetTrigger("runBack");
        }
        if (Input.GetKey(KeyCode.D) && controlLockTimer <= 0)
        {
            //D - turn right by rotating the player along the Y axis in the positive direction
            transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
            rat.ResetTrigger("stop");
            rat.SetTrigger("turn");
        }
        if (rigidBody.velocity.magnitude > speedCap) //make sure no movement is too fast
        {
            rigidBody.velocity = rigidBody.velocity.normalized * speedCap;
        }

        if (IsGrounded() && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D))
        {
            rat.SetTrigger("stop");
        }
    }

    void Update()
    {
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
            else if (!hasStomped && !IsGrounded()) //double jump
            {
                hasStomped = true;
                rigidBody.AddForce(transform.up * (jumpForce*1/60) - rigidBody.velocity, ForceMode.VelocityChange);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!hasStomped && !IsGrounded()) //stompdive for jump correction and stuff
            {
                hasStomped = true;
                rigidBody.AddForce(transform.forward * (moveSpeed*3/4), ForceMode.Impulse);
                rigidBody.AddForce(transform.up * -(moveSpeed*1/3), ForceMode.Impulse);
            }
        }
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.5f);
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
        hasStomped = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;// The speed that the player will move at.
    public float rotSpeed = 2f;
    public float jumpForce = 10f;
	public float jumpForceMax = 100f;
	public float jumpForceMin = 10f;
	public float jumpBuildRate = 10f;
	public Vector3 jumpAngle = new Vector3(1,1,0);
	



    bool grounded;
    float distToGround;
    Vector3 movement;                   // The vector to store the direction of the player's movement.
    //Animator anim;                      // Reference to the animator component.
    Rigidbody playerRigidbody;          // Reference to the player's rigidbody.



    void Awake()
    {
        // Create a layer mask for the floor layer.


        // Set up references.
        //anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        distToGround = .5f;
    }


    void Update()
    {
        // Store the input axes.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Move the player around the scene.
        Move(h, v);
        //Animating(h, v);
        // Turn the player to face the mouse cursor.
        /**
		if (!grounded && GetComponent<Rigidbody>().velocity.y == 0)
        {
            grounded = true;
        }
		**/
		
		//Hopefully the following is what enables the player to jump.
		if (IsGrounded() && Input.GetButton("Jump"))
		{
			Jump();
		}
		if (Input.GetButtonUp("Jump"))
		{
			playerRigidbody.velocity = jumpAngle * jumpForce;
			jumpForce = jumpForceMin;
		}
			
			

        // Animate the player.

    }

    void FixedUpdate()
    {

    }

    void Move(float h, float v)
    {
        // Set the movement vector based on the axis input.

        if (v > .1)
        {
            playerRigidbody.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
/**
        if (Input.GetButtonDown("Jump") && grounded)
        {
            playerRigidbody.AddForce(0, jumpForce, 0);
            grounded = false;
        }
		**/
        //movement.Set (0f, 0f, v);

        // Normalise the movement vector and make it proportional to the speed per second.
        //movement = Vector3.forward * speed * Time.deltaTime;

        // Move the player to it's current position plus the movement.
        //playerRigidbody.MovePosition (transform.position + movement);
        //Vector3 rot =( h *rotSpeed * Time.deltaTime);
        playerRigidbody.transform.Rotate(0f, h * rotSpeed * Time.deltaTime, 0f);

        Debug.DrawRay(transform.position, -Vector3.up, Color.red);

    }


/**
    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        //anim.SetBool("IsWalking", walking);
    }
**/
    bool IsGrounded()
    {
        return Physics.CheckCapsule(GetComponent<Collider>().bounds.center, new Vector3(GetComponent<Collider>().bounds.center.x, GetComponent<Collider>().bounds.min.y - 0.1f, GetComponent<Collider>().bounds.center.z), 1.3f);
    }
	
	//Jump class using slingshot jump physics. Increase power while button is pressed. jump using power after released
	void Jump()
	{
		if(Input.GetButton("Jump") && jumpForce < jumpForceMax && IsGrounded()) 
		{
			//Rigidbody.GetComponent<Rigidbody>.speed = 0;
			jumpForce += Time.deltaTime * jumpBuildRate;
		}
		
		//playerRigidbody.velocity = jumpAngle * jumpForce;
		//jumpForce = jumpForceMin;
		
	}
			
}
	


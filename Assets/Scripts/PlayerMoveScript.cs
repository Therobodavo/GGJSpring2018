using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour {

    //public variables
    public float accel;
    public float airAccel;
    public float decel;
    public float velocity;
    public float maxSpeed;
    public float jumpForce;
    public float maxLandDistance;
    public Rigidbody2D body;
    public Collider playerCollider;

    public bool isOnFloor;
    private int xChange;

	// Use this for initialization
	void Start () {
        xChange = 0;
        isOnFloor = false;

        body.freezeRotation = true;
	}
	
	// Update is called once per frame
	void Update () {
        float dT = Time.deltaTime;

        CheckColliders();

        GetInput();

        Accelerate(dT);

        Move(dT);
	}

    private void CheckColliders()
    {
        if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - .01f), -Vector3.up, maxLandDistance))
        {
            if (!isOnFloor)
            {
                isOnFloor = true;
            }
        }
        else
        {
            isOnFloor = false;
        }
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            xChange = -1;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            xChange = 1;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.D))
            {
                xChange = 1;
            }
            else
            {
                xChange = 0;
            }

        }
        else if (Input.GetKeyUp(KeyCode.D))
        {

            if (Input.GetKey(KeyCode.A))
            {
                xChange = -1;
            }
            else
            {
                xChange = 0;
            }

        }
        


        if(body.velocity.y <= 0 && isOnFloor && Input.GetKey(KeyCode.W))
        {
            body.AddForce(new Vector2(0, jumpForce));
            body.velocity = new Vector2(body.velocity.x, .01f);
        }
    }

    private void Accelerate(float dT)
    {
        //use floor acceleration if on floor
        if (isOnFloor)
        {
            if (xChange != velocity / Math.Abs(velocity))
            {
                velocity = velocity * decel;
            }

            if (xChange != 0)
            {
                velocity += xChange * accel * dT;
            }

        }
        else //otherwise use air acceleration
        {
            velocity += xChange * airAccel * dT;
        }
        if(Math.Abs(velocity) > maxSpeed)
        {
            velocity = maxSpeed * velocity / Math.Abs(velocity);
        }
    }

    private void Move(float dT)
    {
        transform.Translate(velocity * dT, 0, 0);
    }
}

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
    public float maxMoveDistance;
    public Rigidbody2D body;
    public Collider playerCollider;

    public bool isOnFloor;
    private int xChange;

    //left and right checkers
    private bool leftBlock;
    private bool rightBlock;

    public SpriteRenderer sprite;
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
        if (Physics2D.Raycast(new Vector2(transform.position.x + .5f * sprite.bounds.size.x - .05f, transform.position.y - .05f), -Vector3.up, maxLandDistance) || 
            Physics2D.Raycast(new Vector2(transform.position.x - .5f * sprite.bounds.size.x + .05f, transform.position.y - .05f), -Vector3.up, maxLandDistance) ||
            Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - .01f), -Vector3.up, maxLandDistance))
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

        if (Physics2D.Raycast(new Vector2(transform.position.x - .5f * sprite.bounds.size.x - .01f, transform.position.y + .1f), -Vector3.right, maxMoveDistance) ||
            Physics2D.Raycast(new Vector2(transform.position.x - .5f * sprite.bounds.size.x - .01f, transform.position.y + .5f * sprite.bounds.size.y), -Vector3.right, maxMoveDistance) ||
            Physics2D.Raycast(new Vector2(transform.position.x - .5f * sprite.bounds.size.x - .01f, transform.position.y + sprite.bounds.size.y - .1f), -Vector3.right, maxMoveDistance))
        {
            if (!leftBlock)
            {
                leftBlock = true;
            }
        }
        else
        {
            leftBlock = false;
        }
        print(sprite.bounds.size.x);
        if (Physics2D.Raycast(new Vector2(transform.position.x + .5f * sprite.bounds.size.x + .01f, transform.position.y + .1f), Vector3.right, maxMoveDistance) ||
            Physics2D.Raycast(new Vector2(transform.position.x + .5f * sprite.bounds.size.x + .01f, transform.position.y + .5f * sprite.bounds.size.y), Vector3.right, maxMoveDistance) ||
            Physics2D.Raycast(new Vector2(transform.position.x + .5f * sprite.bounds.size.x + .01f, transform.position.y + sprite.bounds.size.y - .1f), Vector3.right, maxMoveDistance))
        {
            if (!rightBlock)
            {
                rightBlock = true;
            }
        }
        else
        {
            rightBlock = false;
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
        if(xChange == -1 && leftBlock && velocity < 0)
        {
            velocity = 0;
            return;
        }
        if(xChange == 1 && rightBlock && velocity > 0)
        {
            velocity = 0;
            return;
        }

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

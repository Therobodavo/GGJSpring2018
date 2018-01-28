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
    public Collider2D playerCollider;

    public LayerMask layer;


    bool directionLeft;

    //playeer start position
    public Vector3 sPos;

    public bool isOnFloor;
    private int xChange;

    public bool isLaser;
    public bool isFired;
    public int canLaser = 1;

    public SpriteRenderer laserBall;
    public GameObject laser;

    public Vector2 savedVelocity;

    //left and right checkers
    private bool leftBlock;
    private bool rightBlock;

    public GameObject[] mirrors;
    public SpriteRenderer[] mirrorSprites;

    public SpriteRenderer sprite;
    public SpriteRenderer death;
    public SpriteRenderer jumpSprite;
    public SpriteRenderer walkingSprite;
    public SpriteRenderer transSprite;
    public Animate tranAnimate;
    public Animate deathAnimate;

    public bool transforming;


    public AudioSource laserSound;

    // Use this for initialization
    void Start()
    {
        xChange = 0;
        isOnFloor = false;

        body.freezeRotation = true;

        laser.SetActive(false);

        sPos = transform.position;
        isLaser = false;
        isFired = false;
        
        mirrors = new GameObject[0];
        mirrorSprites = new SpriteRenderer[0];
        GetAllMirrors();
        jumpSprite.enabled = false;
        transSprite.enabled = false;
        death.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            Die();
        }

        GetInput();


        if (!isLaser)
        {
            float dT = Time.deltaTime;

            CheckColliders();

            Accelerate(dT);

            Move(dT);

            laserBall.enabled = false;
        }
        else
        {
            if (!isFired && Input.GetKeyUp(KeyCode.Space))
            {
                EndLaser(false);
            }
        }
    }

    public void GetAllMirrors()
    {
        mirrors = GameObject.FindGameObjectsWithTag("Mirror");
        mirrorSprites = new SpriteRenderer[mirrors.Length];

        for (int i = 0; i < mirrors.Length; i++)
        {
            mirrorSprites[i] = mirrors[i].GetComponent<SpriteRenderer>();
        }
    }

    public void EndLaser(bool wall)
    {
        isLaser = false;
        isFired = false;
        transforming = false;
        body.gravityScale = 1.5f;
        if (!wall) body.velocity = savedVelocity;
        else
        {
            body.velocity = new Vector2(0, 0);
            velocity = 0;
        }
        sprite.enabled = true;
        playerCollider.enabled = true;
        laser.SetActive(false);

        if (laserSound.isPlaying)
        {
            laserSound.Stop();
        }
    }

    private void CheckColliders()
    {
        print(playerCollider.bounds.size.x);
        if (Physics2D.Raycast(new Vector2(transform.position.x + .5f * playerCollider.bounds.size.x - .05f, transform.position.y), -Vector3.up, maxLandDistance, layer) ||
            Physics2D.Raycast(new Vector2(transform.position.x - .5f * playerCollider.bounds.size.x + .05f, transform.position.y), -Vector3.up, maxLandDistance, layer) ||
            Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), -Vector3.up, maxLandDistance, layer))
        {
            print("hi");
            if (!isOnFloor)
            {
                isOnFloor = true;
            }
            if (canLaser != 0)
            {
                canLaser--;
            }
        }
        else
        {
            isOnFloor = false;
        }
        if (Physics2D.Raycast(new Vector2(transform.position.x - .5f * playerCollider.bounds.size.x - .01f, transform.position.y + .2f), -Vector3.right, maxMoveDistance, layer) ||
            Physics2D.Raycast(new Vector2(transform.position.x - .5f * playerCollider.bounds.size.x - .01f, transform.position.y + .5f * playerCollider.bounds.size.y), -Vector3.right, maxMoveDistance, layer) ||
            Physics2D.Raycast(new Vector2(transform.position.x - .5f * playerCollider.bounds.size.x - .01f, transform.position.y + playerCollider.bounds.size.y - .2f), -Vector3.right, maxMoveDistance, layer))
        {
            print("left");
            if (!leftBlock)
            {
                leftBlock = true;
            }
        }
        else
        {
            leftBlock = false;
        }
        //print(playerCollider.bounds.size.x);
        if (Physics2D.Raycast(new Vector2(transform.position.x + .5f * playerCollider.bounds.size.x, transform.position.y + .2f), Vector3.right, maxMoveDistance, layer) ||
            Physics2D.Raycast(new Vector2(transform.position.x + .5f * playerCollider.bounds.size.x, transform.position.y + .5f * playerCollider.bounds.size.y), Vector3.right, maxMoveDistance, layer) ||
            Physics2D.Raycast(new Vector2(transform.position.x + .5f * playerCollider.bounds.size.x, transform.position.y + playerCollider.bounds.size.y - .2f), Vector3.right, maxMoveDistance, layer))
        {
            print("right");
            if (!rightBlock)
            {
                rightBlock = true;
            }
        }
        else
        {
            rightBlock = false;
        }

        for (int i = 0; i < mirrors.Length; i++)
        {
            switch ((int)mirrors[i].transform.eulerAngles.z)
            {
                case 0:
                    if (mirrorSprites[i].bounds.min.x > playerCollider.bounds.min.x && mirrorSprites[i].bounds.min.x < playerCollider.bounds.max.x)
                    {
                        if (mirrorSprites[i].bounds.max.y < playerCollider.bounds.min.y && mirrorSprites[i].bounds.max.y + 10 * maxLandDistance > playerCollider.bounds.min.y)
                        {
                            if (!isOnFloor)
                            {
                                isOnFloor = true;
                            }
                            if (canLaser != 0)
                            {
                                canLaser--;
                            }
                        }
                    }
                    break;
                case 90:
                    if (mirrorSprites[i].bounds.max.x > playerCollider.bounds.min.x && mirrorSprites[i].bounds.max.x < playerCollider.bounds.max.x)
                    {
                        if (mirrorSprites[i].bounds.max.y < playerCollider.bounds.min.y && mirrorSprites[i].bounds.max.y + 10 * maxLandDistance > playerCollider.bounds.min.y)
                        {
                            if (!isOnFloor)
                            {
                                isOnFloor = true;
                            }
                            if (canLaser != 0)
                            {
                                canLaser--;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }


        }
    }

    private void GetInput()
    {

        if (!deathAnimate.deathTrans && !tranAnimate.transformer)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                xChange = -1;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                xChange = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Space) && !isLaser)
            {
                transforming = true;

                sprite.enabled = false;
                walkingSprite.enabled = false;
                jumpSprite.enabled = false;
                transSprite.enabled = true;
                tranAnimate.transformer = true;
                tranAnimate.scr = this;
                tranAnimate.Timer = 0;
                body.gravityScale = 0;
                velocity = 0;
                xChange = 0;
                savedVelocity = body.velocity;
                body.velocity = Vector2.zero;
                playerCollider.enabled = false;




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
        }

        if (deathAnimate.deathTrans)
        {
            xChange = 0;
        }


        if (!transforming && !deathAnimate.deathTrans)
        {
            if (isOnFloor && !isLaser  && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            {
                sprite.enabled = false;
                walkingSprite.enabled = true;

                if (xChange == 1 && walkingSprite.flipX && sprite.flipX)
                {
                    walkingSprite.flipX = !walkingSprite.flipX;
                    sprite.flipX = !sprite.flipX;
                    jumpSprite.flipX = !jumpSprite.flipX;
                    transSprite.flipX = !transSprite.flipX;
                }
                else if (xChange == -1 && !walkingSprite.flipX && !sprite.flipX)
                {
                    walkingSprite.flipX = !walkingSprite.flipX;
                    sprite.flipX = !sprite.flipX;
                    jumpSprite.flipX = !jumpSprite.flipX;
                    transSprite.flipX = !transSprite.flipX;

                }
            }
            else
            {
                if (!isLaser)
                {
                    sprite.enabled = true;
                }
                walkingSprite.enabled = false;
            }
            if (isLaser)
            {
                jumpSprite.enabled = false;
            }

            if (!isLaser)
            {

                if (body.velocity.y <= 0 && isOnFloor && Input.GetKey(KeyCode.W))
                {
                    body.AddForce(new Vector2(0, jumpForce));
                    body.velocity = new Vector2(body.velocity.x, .01f);
                }


                if (!isOnFloor)
                {
                    sprite.enabled = false;
                    jumpSprite.enabled = true;
                }
                else
                {
                    jumpSprite.enabled = false;
                }
            }
        }


    }

    public void TurnLaser()
    {
        transSprite.enabled = false;
        transforming = false;
        isLaser = true;
        canLaser = 5;
      
        laser.SetActive(true);
        if (!laserSound.isPlaying)
        {
            laserSound.Play();
        }

    }



    private void Accelerate(float dT)
    {
        if (xChange == -1 && leftBlock && velocity < 0)
        {
            velocity = 0;
            return;
        }
        if (xChange == 1 && rightBlock && velocity > 0)
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
        if (Math.Abs(velocity) > maxSpeed)
        {
            velocity = maxSpeed * velocity / Math.Abs(velocity);
        }
    }

    private void Move(float dT)
    {
        transform.Translate(velocity * dT, 0, 0);
    }



public void Die()
    {
        // Death animation
        if (isLaser) EndLaser(true);
        death.enabled = true;
        walkingSprite.enabled = false;
        sprite.enabled = false;
        jumpSprite.enabled = false;
        deathAnimate.Timer = 0;
        deathAnimate.scr = this;
        velocity = 0;
        body.velocity = new Vector2(0, 0);
        if (deathAnimate.AniTime >= 8)
        {

            deathAnimate.deathTrans = true;

        }
      
    }

    public void restart()
    {
        transform.position = sPos;

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("PushableBlock"))
        {
            PushableBlockScript script = obj.GetComponent<PushableBlockScript>();
            script.Reset();
        }

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Lantern"))
        {
            Lantern script = obj.GetComponent<Lantern>();
            script.Reset();
        }

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Switch"))
        {
            Switch script = obj.GetComponent<Switch>();
            script.Reset();
        }


        death.enabled = false;
    }

}


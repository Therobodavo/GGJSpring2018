using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
    public PlayerMoveScript script;
    public Animate animatiated;
    public SpriteRenderer lightBall;
    public SpriteRenderer lightBeam;


    public GameObject player;
    public float laserSpeed;

    private float xChange;
    private float yChange;
    public AudioSource hitMirror;

    // Use this for initialization
    void Start()
    {
        lightBall.enabled = false;
        lightBeam.enabled = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!script.isLaser)
        {
            lightBall.enabled = false;
        }
        if (script.isLaser)
        {
            float dT = Time.deltaTime;

            if (!script.isFired)
            {
                lightBall.enabled = true;
                lightBeam.enabled = false;

                if (Input.GetKeyDown(KeyCode.A))
                {
                    SetFired();
                    xChange = -1f;
                    yChange = 0;

                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    SetFired();
                    xChange = 1f;
                    yChange = 0;

                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    SetFired();
                    xChange = 0;
                    yChange = 1f;

                    transform.eulerAngles = new Vector3(0, 0, 90);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    SetFired();
                    xChange = 0;
                    yChange = -1f;

                    transform.eulerAngles = new Vector3(0, 0, 90);
                }
            }
            else
            {
                player.transform.position = new Vector2(player.transform.position.x + xChange * laserSpeed * dT, player.transform.position.y + yChange * laserSpeed * dT);
                lightBall.enabled = false;
            }
        }
        else
        {
            lightBeam.enabled = true;
            lightBall.enabled = false;
        }
    }

    private void SetFired()
    {
        lightBall.enabled = false;
        lightBeam.enabled = true;
        script.isFired = true;

        if (script.laserSound.isPlaying)
        {
            script.laserSound.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Mirror")
        {
            hitMirror.mute = false;
            hitMirror.Play();
            int rotation = (int)other.transform.eulerAngles.z;
            switch (rotation)
            {
                case 0:
                    if (xChange == -1)
                    {
                        xChange = 0;
                        yChange = 1;
                        transform.eulerAngles = new Vector3(0, 0, 90);
                        return;
                    }
                    if (yChange == -1)
                    {
                        xChange = 1;
                        yChange = 0;
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        return;
                    }
                    break;
                case 90:
                    if (xChange == 1)
                    {
                        xChange = 0;
                        yChange = 1;
                        transform.eulerAngles = new Vector3(0, 0, 90);
                        return;
                    }
                    if (yChange == -1)
                    {
                        xChange = -1;
                        yChange = 0;
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        return;
                    }
                    break;
                case 180:
                    if (xChange == 1)
                    {
                        xChange = 0;
                        yChange = -1;
                        transform.eulerAngles = new Vector3(0, 0, 90);
                        return;
                    }
                    if (yChange == 1)
                    {
                        xChange = -1;
                        yChange = 0;
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        return;
                    }
                    break;
                case 270:
                    if (xChange == -1)
                    {
                        xChange = 0;
                        yChange = -1;
                        transform.eulerAngles = new Vector3(0, 0, 90);
                        return;
                    }
                    if (yChange == 1)
                    {
                        xChange = 1;
                        yChange = 0;
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        return;
                    }
                    break;
            }
        }
        if (other.tag == "OneWayGlass")
        {
            switch ((int)other.transform.eulerAngles.z)
            {
                case 0:
                    if (xChange == -1)
                    {
                        return;
                    }

                    break;
                case 90:
                    if (yChange == -1)
                    {
                        return;
                    }

                    break;
                case 180:
                    if (xChange == 1)
                    {
                        return;
                    }

                    break;
                case 270:
                    if (yChange == 1)
                    {
                        return;
                    }

                    break;

                default:
                    break;
            }
        }
        if (other.tag != "Transparent" && other.tag != "Lantern" && other.tag != "Switch")
        {
            script.EndLaser(true);
            float dimx = player.GetComponent<Collider2D>().bounds.size.x / 2.0f;
            print(script.sprite.bounds.size.x);
            float dimy = player.GetComponent<Collider2D>().bounds.size.y / 2.0f;
            print(dimy);
            player.transform.position = new Vector2(player.transform.position.x - dimx * (float)xChange, player.transform.position.y - dimy * (float)yChange);
            if(other.tag == "Spike")
            script.Die();
        }
    }
}
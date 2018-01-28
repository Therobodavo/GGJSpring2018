using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public PlayerMoveScript script;
    public GameObject player;
    public float laserSpeed;

    private float xChange;
    private float yChange;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
        if (script.isLaser)
        {
            float dT = Time.deltaTime;

            if (!script.isFired)
            {
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
            }
        }
	}

    private void SetFired()
    {
        GetComponent<SpriteRenderer>().sprite = script.laser2;
        script.isFired = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Mirror"){
            int rotation = (int) other.transform.eulerAngles.z;
            switch (rotation)
            {
                case 0:
                    if(xChange == -1)
                    {
                        xChange = 0;
                        yChange = 1;
                        transform.eulerAngles = new Vector3(0, 0, 90);
                        return;
                    }
                    if(yChange == -1)
                    {
                        xChange = 1;
                        yChange = 0;
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        return;
                    }
                    break;
                case 90:
                    if(xChange == 1)
                    {
                        xChange = 0;
                        yChange = 1;
                        transform.eulerAngles = new Vector3(0, 0, 90);
                        return;
                    }
                    if(yChange == -1)
                    {
                        xChange = 1;
                        yChange = 0;
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        return;
                    }
                    break;
                case 180:
                    if(xChange == 1)
                    {
                        xChange = 0;
                        yChange = -1;
                        transform.eulerAngles = new Vector3(0, 0, 90);
                        return;
                    }
                    if(yChange == 1)
                    {
                        xChange = -1;
                        yChange = 0;
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        return;
                    }
                    break;
                case 270:
                    if(xChange == -1)
                    {
                        xChange = 0;
                        yChange = -1;
                        transform.eulerAngles = new Vector3(0, 0, 90);
                        return;
                    }
                    if(yChange == 1)
                    {
                        xChange = 1;
                        yChange = 0;
                        transform.eulerAngles = new Vector3(0, 0, 0);
                        return;
                    }
                    break;
            }
        }
        if(other.tag == "OneWayGlass")
        {
            switch((int)other.transform.eulerAngles.z)
            {
                case 0:
                    if (xChange == -1)
                    {
                        return;
                    }

                    break;
                case 90:
                    if(yChange == -1)
                    {
                        return;
                    }

                    break;
                case 180:
                    if(xChange == 1)
                    {
                        return;
                    }

                    break;
                case 270:
                    if(yChange == 1)
                    {
                        return;
                    }

                    break;

                default:
                    break;
            }
        }
        if(other.tag != "Transparent")
        {
<<<<<<< HEAD
            float dimx = script.gameObject.GetComponent<Collider2D>().bounds.size.x / 2.0f;
            float dimy = script.gameObject.GetComponent<Collider2D>().bounds.size.y / 2.0f;
            player.transform.position = new Vector2(player.transform.position.x - dimx * xChange, player.transform.position.y - dimy * yChange);
            script.EndLaser(true);
=======
            script.EndLaser(true);
            float dimx = player.GetComponent<Collider2D>().bounds.size.x / 2.0f;
            print(script.sprite.bounds.size.x);
            float dimy = player.GetComponent<Collider2D>().bounds.size.y / 2.0f;
            print(dimy);
            player.transform.position = new Vector2(player.transform.position.x - dimx * (float) xChange, player.transform.position.y - dimy * (float) yChange);

>>>>>>> 6e13457d4a30ee7efa8bc07238a9360da944a7a4
        }
    }
}

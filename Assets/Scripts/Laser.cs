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
        script.isFired = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "Transparent")
        {
            print(other.name);
            script.EndLaser(true);
        }
    }
}

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
                    script.isFired = true;
                    xChange = -1f;
                    yChange = 0;

                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    script.isFired = true;
                    xChange = 1f;
                    yChange = 0;

                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    script.isFired = true;
                    xChange = 0;
                    yChange = 1f;

                    transform.eulerAngles = new Vector3(0, 0, 90);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    script.isFired = true;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("yo");
        script.EndLaser();
    }
}

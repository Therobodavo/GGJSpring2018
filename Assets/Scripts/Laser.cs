using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public PlayerMoveScript script;

    private float xChange;
    private float yChange;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
        if (script.isLaser)
        {
            if (!script.isFired)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    script.isFired = true;
                    xChange = -0.5f;
                    yChange = 0;
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    script.isFired = true;
                    xChange = 0.5f;
                    yChange = 0;
                }
                else if (Input.GetKeyDown(KeyCode.W))
                {
                    script.isFired = true;
                    xChange = 0;
                    yChange = 0.5f;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    script.isFired = true;
                    xChange = 0;
                    yChange = -0.5f;
                }
            }
            else
            {
                transform.position = new Vector2(transform.position.x + xChange, transform.position.y + yChange);
            }
        }
	}
}

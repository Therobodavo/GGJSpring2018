using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Spikes class
//Used to detect player when touching a spike
//Causes death

public class Spikes : MonoBehaviour {

    // player object
    public GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player"); //Reference to player
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //When player is touching a spike
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Player")
        {
            PlayerMoveScript pScript = (PlayerMoveScript)player.GetComponent("PlayerMoveScript");

            if (!pScript.isLaser)
            {
                pScript.Die();
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        //Do whatever you want
    }
}

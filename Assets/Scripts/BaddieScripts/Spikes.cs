using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    // player object
    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerMoveScript pScript = (PlayerMoveScript)player.GetComponent("PlayerMoveScript");

        if (!pScript.isLaser)
        {
            pScript.Die();
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        //Do whatever you want
    }
}

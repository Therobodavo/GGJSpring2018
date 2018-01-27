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
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMoveScript pScript = (PlayerMoveScript)player.GetComponent("PlayerMoveScript");
        pScript.transform.position = pScript.sPos;
        Debug.Log("WOOOT");
    }
    private void OnTriggerExit(Collider other)
    {
        //Do whatever you want
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour {

    // player object
    public GameObject player;

    //player collider
    Collider2D playerCollide;

    //start position
    public Vector3 sPos;

    //Box collider for hunting player
    public BoxCollider2D huntBox;

    // Use this for initialization
    void Start () {
        sPos = transform.position;
       // playerCollide = 
	}
	
	// Update is called once per frame
	void Update () {
		//if()
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMoveScript pScript = (PlayerMoveScript)player.GetComponent("PlayerMoveScript");
        pScript.transform.position = pScript.sPos;
        transform.position = sPos;

    }

 

    
}

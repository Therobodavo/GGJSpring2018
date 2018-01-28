using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    public GameObject trapdoor;

    public bool on;

    public AudioSource switchSound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            on = !on;

            bool tState = trapdoor.GetComponent<Collider2D>().enabled;

            GetComponent<SpriteRenderer>().flipX = on;
<<<<<<< HEAD
            trapdoor.GetComponent<Collider2D>().enabled = !tState;
            trapdoor.GetComponent<SpriteRenderer>().enabled = !tState;
=======
            trapdoor.GetComponent<Collider2D>().enabled = !on;
            trapdoor.GetComponent<SpriteRenderer>().enabled = !on;

            switchSound.Play();
>>>>>>> 2e6f37607a7434ddc8163c4f29a80c22e0ab263f
        }
    }
}

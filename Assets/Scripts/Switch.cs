using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Switch class
//Used for activating/deactivating switches

public class Switch : MonoBehaviour {

    //Variables used
    public GameObject trapdoor;
    public bool on;

    //Sound file for activating switch
    public AudioSource switchSound;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }


    public void OnTriggerStay2D(Collider2D other)
    {
        //When player is pressing E and next to switch
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            on = !on;

            bool tState = trapdoor.GetComponent<Collider2D>().enabled;
            trapdoor.GetComponent<Collider2D>().enabled = !tState;
            trapdoor.GetComponent<SpriteRenderer>().enabled = !tState;
            GetComponent<SpriteRenderer>().flipX = on;
            
            switchSound.Play();
        }
    }

    //Resets switch
    public void Reset()
    {
        if (on)
        {
            bool tState = trapdoor.GetComponent<Collider2D>().enabled;
            trapdoor.GetComponent<Collider2D>().enabled = !tState;
            trapdoor.GetComponent<SpriteRenderer>().enabled = !tState;
            GetComponent<SpriteRenderer>().flipX = on;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    public GameObject trapdoor;

    public bool on;

    public AudioSource switchSound;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void OnTriggerStay2D(Collider2D other)
    {
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Lantern class
//Sets everything up for Lantern

public class Lantern : MonoBehaviour {
    
    //References
    public PlayerMoveScript script;
    public Sprite lit;
    public Sprite unlit;
    public GameObject door;
    public AudioSource lightSound;

    //Variables
    public bool isLit;

	void Start () {
        door = GameObject.FindGameObjectWithTag("Door");
        door.GetComponent<TriggerDoor>().UncountLantern();
        isLit = false;

        script = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMoveScript>();
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //If player hits lantern when a laser
        if(other.tag == "Laser")
        {
            if (script.isLaser && !isLit)
            {
                Light();
            }
        }
    }

    //Activate Lantern
    void Light()
    {
        isLit = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = lit;
        TriggerDoor td = door.GetComponent<TriggerDoor>();
        td.CountLantern();

        lightSound.Play();
    }

    //Reset Lantern
    public void Reset()
    {
        TriggerDoor td = door.GetComponent<TriggerDoor>();
        if (isLit) td.UncountLantern();
        isLit = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = unlit;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour {

    public PlayerMoveScript script;

    public Sprite lit;

    public bool isLit;

    public GameObject door;

	// Use this for initialization
	void Start () {
        door = GameObject.FindGameObjectWithTag("Door");
	}
	
	// Update is called once per frame
	void Update () {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        print("z");
        if(other.tag == "Laser")
        {
            print("hi");
            if (script.isLaser)
            {
                Light();
            }
        }
    }

    void Light()
    {
        isLit = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = lit;
        TriggerDoor td = door.GetComponent<TriggerDoor>();
        td.CountLantern();
    }

    private void Reset()
    {
        isLit = false;
    }
}

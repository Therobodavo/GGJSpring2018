using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDoor : MonoBehaviour {

    public string newScene;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.gameObject.tag == "player")
        {
            SceneManager.LoadScene(newScene);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Do whatever you want
    }
}

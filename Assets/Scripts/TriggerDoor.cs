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
    private void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(newScene);
    }
    private void OnTriggerExit(Collider other)
    {
        //Do whatever you want
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDoor : MonoBehaviour {

    public string newScene;

    public int lanternCount;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.gameObject.tag == "Player" && lanternCount == 0)
        {
            SceneManager.LoadScene(newScene);
            other.collider.gameObject.GetComponent<PlayerMoveScript>().GetAllMirrors();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Do whatever you want
    }

    public void CountLantern()
    {
        lanternCount--;
        //play sound
        if(lanternCount == 0)
        {
            //play sound
            //set image to unlocked
        }
    }
}

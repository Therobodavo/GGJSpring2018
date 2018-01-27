using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = new Vector3(0f,0f,0f);
        Debug.Log("ds");
    }
    private void OnTriggerExit(Collider other)
    {
        //Do whatever you want
    }
}

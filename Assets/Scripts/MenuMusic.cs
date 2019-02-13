using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Menu Music Script
//Used for singleton music class (For MENU ONLY)

public class MenuMusic : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}
    void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("GameMusic").Length > 0)
        {
            Destroy(GameObject.Find("GameMusic"));
        }  
        if(GameObject.FindGameObjectsWithTag("MenuMusic").Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}

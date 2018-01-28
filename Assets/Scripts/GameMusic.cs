using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Awake()
    {
        if(GameObject.FindGameObjectsWithTag("MenuMusic").Length > 0)
        {
            Destroy(GameObject.Find("MenuMusic"));
        }  
        if(GameObject.FindGameObjectsWithTag("GameMusic").Length > 1)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}

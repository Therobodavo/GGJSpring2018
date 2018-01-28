using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
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

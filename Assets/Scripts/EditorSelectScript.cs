using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorSelectScript : MonoBehaviour {

    public LevelEditorScript levelScript;
    public GameObject prefab;
    public Sprite sprite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            levelScript.sprite.sprite = sprite;
            levelScript.gameObject.transform.localScale = prefab.transform.localScale;
            levelScript.currentPrefab = prefab;
        }
    }
}

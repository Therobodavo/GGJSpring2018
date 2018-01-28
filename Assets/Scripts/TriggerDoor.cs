using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerDoor : MonoBehaviour {

    public string newScene;

    public Sprite open;
    public Sprite closed;

    public bool restart;

    public int lanternCount;

    public AudioSource doorUnlock;
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
            if (!other.collider.GetComponent<PlayerMoveScript>().isLaser)
            {
                if (restart)
                {
                    other.collider.transform.position = other.collider.GetComponent<PlayerMoveScript>().sPos;
                    return;
                }

                SceneManager.LoadScene(newScene);
                other.collider.gameObject.GetComponent<PlayerMoveScript>().GetAllMirrors();
            }
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
            doorUnlock.mute = false;
            doorUnlock.Play();
            GetComponent<SpriteRenderer>().sprite = open;
        }
    }

    public void UncountLantern()
    {
        lanternCount++;
        //play sound
        if (lanternCount != 0)
        {
            GetComponent<SpriteRenderer>().sprite = closed;
        }
    }
}

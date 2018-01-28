using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEditorScript : MonoBehaviour {

    public SpriteRenderer sprite;
    public GameObject currentPrefab;

    public GameObject player;
    public GameObject door;

    public GameObject currentTrapDoor;

    public List<GameObject> objects;
    public List<Vector3> objectLocs;

    public GameObject music;

    public Camera mainCam;
    private float camWidth;
    private float camHeight;

    public List<GameObject> possiblePrefabs;
    private List<Sprite> possiblePrefabSprites;

    private List<List<GameObject>> menuObjects;

    private bool menu;

    private bool playing;

    private KeyCode[] numKeyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };

    // Use this for initialization
    void Start () {
        player.GetComponent<Rigidbody2D>().isKinematic = true;

        menu = false;
        playing = false;

        menuObjects = new List<List<GameObject>>();
        camHeight = 2f * mainCam.orthographicSize;
        camWidth = camHeight * mainCam.aspect;

        objects = new List<GameObject>();
        objectLocs = new List<Vector3>();

        possiblePrefabSprites = new List<Sprite>();

        foreach (var item in possiblePrefabs)
        {
            possiblePrefabSprites.Add(item.GetComponent<SpriteRenderer>().sprite);
        }

        float objSep = (camWidth - 4f) / 6f;
        List<GameObject> currentMenu = new List<GameObject>();
        for (int i = 0; i < possiblePrefabs.Count; i++)
        {
            if (i % 6 == 0)
            {
                currentMenu = new List<GameObject>();
                menuObjects.Add(currentMenu);
            }

            GameObject newObj = new GameObject();

            newObj.transform.localScale = possiblePrefabs[i].transform.localScale * 2;
            newObj.transform.position = new Vector3(mainCam.transform.localPosition.x - camWidth / 2 + 5 + objSep * (i % 6), mainCam.transform.localPosition.y - camHeight/ 2 + 2,-6);

            SpriteRenderer newRender = newObj.AddComponent<SpriteRenderer>();
            newRender.sprite = possiblePrefabSprites[i];

            if(i == 1)
            {
                newRender.sprite = possiblePrefabSprites[0];
            }

            EditorSelectScript newScript = newObj.AddComponent<EditorSelectScript>();
            newScript.sprite = possiblePrefabSprites[i];
            newScript.prefab = possiblePrefabs[i];
            newScript.levelScript = this;

            newObj.AddComponent<BoxCollider2D>();

            newObj.SetActive(false);

            currentMenu.Add(newObj);
        }

        
	}
	
	// Update is called once per frame
	void Update () {
        if (!playing)
        {
            for (int i = 1; i <= numKeyCodes.Length; i++)
            {
                if (menuObjects.Count >= i && Input.GetKeyDown(numKeyCodes[i - 1]))
                {
                    for (int j = 0; j < menuObjects.Count; j++)
                    {
                        for (int k = 0; k < menuObjects[j].Count; k++)
                        {
                            menuObjects[j][k].SetActive(false);
                        }
                    }

                    for (int j = 0; j < menuObjects[i - 1].Count; j++)
                    {
                        menuObjects[i - 1][j].SetActive(true);
                    }

                    menu = true;
                }
            }

            Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePos.x, mousePos.y, -5);

            if (Input.GetMouseButtonDown(0))
            {
                if (menu)
                {
                    menu = false;

                    for (int j = 0; j < menuObjects.Count; j++)
                    {
                        for (int k = 0; k < menuObjects[j].Count; k++)
                        {
                            menuObjects[j][k].SetActive(false);
                        }
                    }

                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                else if (currentPrefab != null)
                {
                    if (currentPrefab.tag == "Player")
                    {
                        player.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
                        player.GetComponent<PlayerMoveScript>().sPos = player.transform.position;

                        currentPrefab = null;
                        sprite.sprite = null;
                    }
                    else if(currentPrefab.tag == "Door")
                    {
                        door.transform.position = new Vector3(mousePos.x, mousePos.y, 0);

                        currentPrefab = null;
                        sprite.sprite = null;
                    }
                    else if (!(currentPrefab.tag == "Switch" && currentTrapDoor == null))
                    {

                        GameObject newObj = Instantiate(currentPrefab);

                        newObj.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
                        newObj.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

                        if (newObj.tag == "Switch")
                        {
                            newObj.GetComponent<Switch>().trapdoor = currentTrapDoor;
                        }

                        if(newObj.tag == "PushableBlock")
                        {
                            newObj.GetComponent<Rigidbody2D>().gravityScale = 0;
                        }

                        objects.Add(newObj);
                        objectLocs.Add(newObj.transform.position);
                    }


                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (currentTrapDoor != null)
                {
                    currentTrapDoor.GetComponent<SpriteRenderer>().color = Color.white;
                    currentTrapDoor = null;
                }

                sprite.sprite = null;
                currentPrefab = null;

                RaycastHit2D hit = new RaycastHit2D();
                if ((hit = Physics2D.Raycast(mousePos, Vector2.up, 0)).collider != null)
                {
                    if(hit.collider.gameObject.tag == "Trapdoor")
                    {
                        currentTrapDoor = hit.collider.gameObject;
                        currentTrapDoor.GetComponent<SpriteRenderer>().color = Color.gray;
                    }

                    if(hit.collider.gameObject.tag == "Door")
                    {
                        transform.localScale = hit.collider.gameObject.transform.localScale;
                        sprite.sprite = hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite;
                        currentPrefab = hit.collider.gameObject;
                    }
                }
                else
                {
                    Collider2D playerCollider = player.GetComponent<Collider2D>();
                    if (mousePos.x > playerCollider.bounds.min.x && mousePos.x < playerCollider.bounds.max.x && mousePos.y > playerCollider.bounds.min.y && mousePos.y < playerCollider.bounds.max.y)
                    {
                        transform.localScale = player.transform.localScale;
                        sprite.sprite = player.GetComponent<SpriteRenderer>().sprite;
                        currentPrefab = player.gameObject;
                    }
                }


            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, (transform.eulerAngles.z + 90) % 360);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (menu)
                {
                    menu = false;

                    for (int j = 0; j < menuObjects.Count; j++)
                    {
                        for (int k = 0; k < menuObjects[j].Count; k++)
                        {
                            menuObjects[j][k].SetActive(false);
                        }
                    }
                }


                if (currentTrapDoor != null)
                {
                    currentTrapDoor.GetComponent<SpriteRenderer>().color = Color.white;
                    currentTrapDoor = null;
                }

                playing = true;

                sprite.sprite = null;
                currentPrefab = null;
                player.GetComponent<Rigidbody2D>().gravityScale = 1.5f;
                player.GetComponent<Rigidbody2D>().isKinematic = false;
                player.GetComponent<PlayerMoveScript>().enabled = true;
                door.GetComponent<TriggerDoor>().enabled = true;

                for (int i = 0; i < objects.Count; i++)
                {
                    if (objects[i].tag == "PushableBlock")
                    {
                        objects[i].GetComponent<Rigidbody2D>().gravityScale = 1.5f;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                RaycastHit2D hit = new RaycastHit2D();
                if ((hit = Physics2D.Raycast(mousePos, Vector2.up, 0)).collider != null)
                {
                    if (!(hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "Door"))
                    {
                        GameObject temp = hit.collider.gameObject;

                        if (hit.collider.gameObject.tag == "Trapdoor")
                        {


                            for (int i = 0; i < objects.Count; i++)
                            {
                                if(objects[i].tag == "Switch" && objects[i].GetComponent<Switch>().trapdoor == hit.collider.gameObject)
                                {
                                    temp = objects[i];
                                    objects.Remove(temp);
                                    objectLocs.Remove(temp.transform.position);
                                    GameObject.Destroy(temp);
                                }
                            }
                        }


                        temp = hit.collider.gameObject;
                        objects.Remove(temp);
                        objectLocs.Remove(temp.transform.position);
                        GameObject.Destroy(temp);
                    }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Reset();

                playing = false;

                player.GetComponent<Rigidbody2D>().gravityScale = 0f;
                player.GetComponent<Rigidbody2D>().isKinematic = true;
                player.GetComponent<PlayerMoveScript>().enabled = false;
                door.GetComponent<TriggerDoor>().enabled = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(music);

            SceneManager.LoadScene("Menu");
        }
	}

    private void Reset()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            objects[i].transform.position = objectLocs[i];

            if (objects[i].tag == "PushableBlock")
            {
                objects[i].GetComponent<Rigidbody2D>().gravityScale = 0f;
            }
        }

        player.transform.position = player.GetComponent<PlayerMoveScript>().sPos;
    }
}

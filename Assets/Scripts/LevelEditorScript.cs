using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorScript : MonoBehaviour {

    public SpriteRenderer sprite;
    public GameObject currentPrefab;

    public GameObject player;
    public GameObject door;

    public GameObject currentTrapDoor;

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
        menu = false;
        playing = false;

        menuObjects = new List<List<GameObject>>();
        camHeight = 2f * mainCam.orthographicSize;
        camWidth = camHeight * mainCam.aspect;

        possiblePrefabSprites = new List<Sprite>();

        foreach (var item in possiblePrefabs)
        {
            print(item.GetComponent<SpriteRenderer>().sprite);
            possiblePrefabSprites.Add(item.GetComponent<SpriteRenderer>().sprite);
        }

        float objSep = (camWidth - 4f) / 6f;
        print(objSep);
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
            print(mainCam.transform.localPosition.x - camWidth / 2 + 2 + objSep * i % 6);
            newObj.transform.position = new Vector3(mainCam.transform.localPosition.x - camWidth / 2 + 5 + objSep * (i % 6), mainCam.transform.localPosition.y - camHeight/ 2 + 2,-6);

            SpriteRenderer newRender = newObj.AddComponent<SpriteRenderer>();
            newRender.sprite = possiblePrefabSprites[i];

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

                    print(menuObjects[i - 1].Count);
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
                    GameObject newObj = Instantiate(currentPrefab);

                    newObj.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
                    newObj.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

                    if (newObj.tag == "Switch")
                    {
                        newObj.GetComponent<Switch>().trapdoor = currentTrapDoor;
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

                RaycastHit2D hit = new RaycastHit2D();
                if ((hit = Physics2D.Raycast(mousePos, Vector2.up, 0)).collider != null)
                {
                    print("hi");
                    if(hit.collider.gameObject.tag == "Trapdoor")
                    {
                        currentTrapDoor = hit.collider.gameObject;
                        currentTrapDoor.GetComponent<SpriteRenderer>().color = Color.gray;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, (transform.eulerAngles.z + 90) % 360);
            }
            if (Input.GetKeyDown(KeyCode.Space))
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
                else if (currentPrefab != null)
                {
                    GameObject newObj = Instantiate(currentPrefab);

                    newObj.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
                }

                playing = true;

                sprite = null;
                currentPrefab = null;
                player.GetComponent<Rigidbody2D>().gravityScale = 1.5f;
                player.GetComponent<PlayerMoveScript>().enabled = true;
                door.GetComponent<TriggerDoor>().enabled = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                player.GetComponent<PlayerMoveScript>().Die();

                player.GetComponent<Rigidbody2D>().gravityScale = 0f;
                player.GetComponent<PlayerMoveScript>().enabled = false;
                door.GetComponent<TriggerDoor>().enabled = false;
            }
        }
	}
}

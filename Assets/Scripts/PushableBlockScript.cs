using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlockScript : MonoBehaviour {

    public Collider2D playerCollider;
    public SpriteRenderer blockSprite;
    public float pushSpeed;
    public float pushStartTime;
    public float maxMoveDistance;

    private float sincePush;
    public bool pushed;
    public int xChange;

    private bool leftBlock;
    private bool rightBlock;

    public Vector3 sPos;

    // Use this for initialization
    void Start () {
        leftBlock = false;
        rightBlock = false;
        pushed = false;
        sincePush = 0;
        sPos = transform.position;
        playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics2D.Raycast(new Vector2(transform.position.x - .5f * blockSprite.bounds.size.x - .01f, transform.position.y + .1f), -Vector3.right, maxMoveDistance) ||
    Physics2D.Raycast(new Vector2(transform.position.x - .5f * blockSprite.bounds.size.x - .01f, transform.position.y + .5f * blockSprite.bounds.size.y), -Vector3.right, maxMoveDistance) ||
    Physics2D.Raycast(new Vector2(transform.position.x - .5f * blockSprite.bounds.size.x - .01f, transform.position.y + blockSprite.bounds.size.y - .1f), -Vector3.right, maxMoveDistance))
        {
            if (!leftBlock)
            {
                leftBlock = true;
            }
        }
        else
        {
            leftBlock = false;
        }
        //print(blockSprite.bounds.size.x);
        if (Physics2D.Raycast(new Vector2(transform.position.x + .5f * blockSprite.bounds.size.x + .01f, transform.position.y + .1f), Vector3.right, maxMoveDistance) ||
            Physics2D.Raycast(new Vector2(transform.position.x + .5f * blockSprite.bounds.size.x + .01f, transform.position.y + .5f * blockSprite.bounds.size.y), Vector3.right, maxMoveDistance) ||
            Physics2D.Raycast(new Vector2(transform.position.x + .5f * blockSprite.bounds.size.x + .01f, transform.position.y + blockSprite.bounds.size.y - .1f), Vector3.right, maxMoveDistance))
        {
            if (!rightBlock)
            {
                rightBlock = true;
            }
        }
        else
        {
            rightBlock = false;
        }

        xChange = 0;

        if (playerCollider.bounds.min.y < blockSprite.bounds.max.y && playerCollider.bounds.max.y > blockSprite.bounds.min.y)
        {

            if (playerCollider.bounds.min.x >= blockSprite.bounds.max.x && playerCollider.bounds.min.x - .05f < blockSprite.bounds.max.x && Input.GetKey(KeyCode.A))
            {
                if (pushed)
                {
                    xChange = -1;
                    sincePush = 0;
                }
                else
                {
                    sincePush += Time.deltaTime;

                    if (sincePush > pushStartTime)
                    {
                        sincePush = 0;
                        pushed = true;
                    }
                }
            }
            else if (playerCollider.bounds.max.x <= blockSprite.bounds.min.x && playerCollider.bounds.max.x + .05f > blockSprite.bounds.min.x && Input.GetKey(KeyCode.D))
            {
                if (pushed)
                {
                    xChange = 1;
                    sincePush = 0;
                }
                else
                {
                    //print("hello");
                    sincePush += Time.deltaTime;

                    if (sincePush > pushStartTime)
                    {
                        sincePush = 0;
                        pushed = true;
                    }
                }
            }
            else
            {
                if (pushed)
                {
                    sincePush += Time.deltaTime;

                    if (sincePush >= .2)
                    {
                        pushed = false;
                        sincePush = 0;
                    }
                }
            }
        }
        else
        {
            pushed = false;
            sincePush = 0;
        }

        //print(rightBlock);
        if(!(leftBlock && xChange == -1) && !(rightBlock && xChange == 1))
        {
            transform.Translate(xChange * pushSpeed * Time.deltaTime, 0, 0);
        }
    }

    public void Reset()
    {
        transform.position = sPos;
    }
}

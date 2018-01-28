using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour {

    //attributes
    SpriteRenderer Rend; //render
    public Sprite[] Animations; // holds each sprite
    public float Timer; // timer to change sprite
    public int AniTime; //specfic timer that controls frames

    // Use this for initialization
    void Start()
    {

        Timer = 0;
        AniTime = 0;
        Rend = (SpriteRenderer)GetComponent("SpriteRenderer");
    }

    // Update is called once per frame
    void Update()
    {

        Timer += 0.2f;

        AniTime = (int)Mathf.Round(Timer);
        if (AniTime >= Animations.Length - 1)
        {
            Timer = 0;
        }

        Rend.sprite = Animations[AniTime];
    }
}
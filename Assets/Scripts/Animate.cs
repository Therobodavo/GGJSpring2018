using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour {

    //attributes
    SpriteRenderer Rend; //render
    public Sprite[] Animations; // holds each sprite
    public float Timer; // timer to change sprite
    public int AniTime; //specfic timer that controls frames

    public bool repeat;
    public float length;

    public bool transformer;
    public bool deathTrans;

    public PlayerMoveScript scr;
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
        length = Animations.Length;
        if (AniTime < Animations.Length)
        {
            Rend.sprite = Animations[AniTime];
            Timer += 0.2f;
        }
        else if (transformer)
        {
            Timer += 0.2f;
            if (AniTime == Animations.Length + 1)
            {
                Timer = 0;
                scr.TurnLaser();
                transformer = false;
            }

        }
        else if (deathTrans)
        {
            Timer += 0.2f;
            if (AniTime == Animations.Length + 1)
            {
                Timer = 0;
                scr.restart();
               deathTrans = false;
            }
        }
        

        AniTime = (int)Mathf.Round(Timer);
        if (AniTime >= Animations.Length - 1)
        {
            if (repeat)
                Timer = 0;

        }


    }
}
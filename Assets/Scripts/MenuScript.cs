using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Utility Script for menu functions

public class MenuScript : MonoBehaviour {

    public void GoToScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
    public void Exit()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject start;
    public GameObject quit;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void changescene(string scenename)
    {
        Application.LoadLevel(scenename);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}

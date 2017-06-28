using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Coordinates : MonoBehaviour
{
    Text txt;
    public GameObject gameobject;
    public GameObject cam;
	// Use this for initialization
	void Start ()
    {
        txt = gameobject.GetComponent<Text>();
        txt.text = "<" + (int)cam.transform.position.x + ", " + (int)cam.transform.position.y + ", " + (int)cam.transform.position.z + " >";
    }
	
	// Update is called once per frame
	void Update ()
    {
        txt.text = "<" + (int)cam.transform.position.x + ", " + (int)cam.transform.position.y + ", " + (int)cam.transform.position.z + " >";
    }
}

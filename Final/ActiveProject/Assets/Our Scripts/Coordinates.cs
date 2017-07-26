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
	// instantiates the coordinates with your initial position
	void Start ()
    {
        txt = gameobject.GetComponent<Text>();
        txt.text = "<" + (int)cam.transform.position.x + ", " + (int)cam.transform.position.y 
            + ", " + (int)cam.transform.position.z + " >";
    }
	
	// Updates the coordinates as you move
	void Update ()
    {
        txt.text = "<" +  (int)cam.transform.position.x + ", " + (int)cam.transform.position.y 
            + ", " + (int)cam.transform.position.z + " >";
    }
}

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
<<<<<<< HEAD


    // instantiates the coordinates with your initial position
    void Start()
=======
	// instantiates the coordinates with your initial position
	void Start ()
>>>>>>> 710c6d937430837c776e15455509c65cc6306a86
    {
 
        txt = gameobject.GetComponent<Text>();
<<<<<<< HEAD
        txt.text = "<" + (int)cam.transform.position.x + ", " +  (int)cam.transform.position.y
            + ", " +  (int)cam.transform.position.z + " >";
    }

    // Updates the coordinates as you move
    void Update()
    {
        txt.text = "<" + (int)cam.transform.position.x + ", " + (int)cam.transform.position.y
=======
        txt.text = "<" + (int)cam.transform.position.x + ", " + (int)cam.transform.position.y 
            + ", " + (int)cam.transform.position.z + " >";
    }
	
	// Updates the coordinates as you move
	void Update ()
    {
        txt.text = "<" + (int)cam.transform.position.x + ", " + (int)cam.transform.position.y 
>>>>>>> 710c6d937430837c776e15455509c65cc6306a86
            + ", " + (int)cam.transform.position.z + " >";
    }
}
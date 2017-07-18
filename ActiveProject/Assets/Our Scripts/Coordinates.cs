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
<<<<<<< HEAD


    // instantiates the coordinates with your initial position
    void Start()
=======
=======
>>>>>>> 710c6d937430837c776e15455509c65cc6306a86
	// instantiates the coordinates with your initial position
	void Start ()
>>>>>>> 710c6d937430837c776e15455509c65cc6306a86
    {
 
        txt = gameobject.GetComponent<Text>();
<<<<<<< HEAD
<<<<<<< HEAD
        txt.text = "<" + (int)cam.transform.position.x + ", " +  (int)cam.transform.position.y
            + ", " +  (int)cam.transform.position.z + " >";
    }

    // Updates the coordinates as you move
    void Update()
    {
        txt.text = "<" + (int)cam.transform.position.x + ", " + (int)cam.transform.position.y
=======
=======
>>>>>>> 710c6d937430837c776e15455509c65cc6306a86
        txt.text = "<" + (int)cam.transform.position.x + ", " + (int)cam.transform.position.y 
            + ", " + (int)cam.transform.position.z + " >";
    }
	
	// Updates the coordinates as you move
	void Update ()
    {
        txt.text = "<" + (int)cam.transform.position.x + ", " + (int)cam.transform.position.y 
<<<<<<< HEAD
>>>>>>> 710c6d937430837c776e15455509c65cc6306a86
=======
>>>>>>> 710c6d937430837c776e15455509c65cc6306a86
            + ", " + (int)cam.transform.position.z + " >";
    }
}
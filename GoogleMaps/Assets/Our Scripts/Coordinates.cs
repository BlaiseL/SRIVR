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
    public GameObject planeT;
    private double ratioLatitudeZ;
    private double ratioLongitudeX;

    // instantiates the coordinates with your initial position
    void Start()
    {
        //Finds the ratio between the plane size and the area of the Hoboken map
        ratioLongitudeX = Math.Round(((-74.008198 + 74.02442) / (planeT.transform.localScale.x*10.0)), 8);
        ratioLatitudeZ = Math.Round(((40.720721 - 40.752849) / (planeT.transform.localScale.z * 10.0)), 8);

        txt = gameobject.GetComponent<Text>();
        txt.text = "<" + (((-74.008198 + -74.02442)/2) + ratioLongitudeX * (int)cam.transform.position.x) + ", " +  (int)cam.transform.position.y
            + ", " + (((40.720721 + 40.752849)/2)+ ratioLatitudeZ * (int)cam.transform.position.z) + " >";
    }

    // Updates the coordinates as you move
    void Update()
    {
        txt.text = "<" + (((-74.008198 + -74.02442) / 2) + ratioLongitudeX * (int)cam.transform.position.x) + ", " +  (int)cam.transform.position.y
            + ", " + (((40.720721 + 40.752849) / 2) + ratioLatitudeZ * (int)cam.transform.position.z) + " >";
    }
}
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
    public double latitude1, latitude2;
    public double longitude1, longitude2;
    private double ratioLatitudeZ;
    private double ratioLongitudeX;

    // instantiates the coordinates with your initial position
    void Start()
    {
        //Finds the ratio between the plane size and the area of the Hoboken map
        ratioLongitudeX = Math.Round((Math.Abs(longitude2 - longitude1) / (planeT.transform.localScale.x*10.0)), 8);
        ratioLatitudeZ = Math.Round((Math.Abs(latitude2 - latitude1) / (planeT.transform.localScale.z * 10.0)), 8);

        txt = gameobject.GetComponent<Text>();
        txt.text = "<" + (((latitude1 + latitude2) / 2) + ratioLatitudeZ * (int)cam.transform.position.z) + ", " +  (int)cam.transform.position.y
            + ',' + (((longitude1 + longitude2) / 2) + ratioLongitudeX * (int)cam.transform.position.x)+ " >";
    }

    // Updates the coordinates as you move
    void Update()
    {
        txt.text = "<" + ((latitude1) + ratioLatitudeZ * (int)cam.transform.position.z) + ", " + (int)cam.transform.position.y
            + ',' + ((longitude1) + ratioLongitudeX * (int)cam.transform.position.x) + " >";
    }
}
<<<<<<< HEAD
﻿using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEditor;

public class screen : MonoBehaviour
{
    public int resolution = 3; // 1= default, 2= 2x default, etc.
    public string imageName = "Screenshot_";
    public string customPath = ""; // leave blank for project file location
    public bool resetIndex = false;
    private int index = 0;
    public GameObject canv;

    void Awake()
    {
        if (resetIndex) PlayerPrefs.SetInt("ScreenshotIndex", 0); //reset index if player checks pictures
        if (customPath != "") //if there is a path save it there
        {
            if (!System.IO.Directory.Exists(customPath))
            {
                System.IO.Directory.CreateDirectory(customPath);
            }
        }
        index = PlayerPrefs.GetInt("ScreenshotIndex") != 0 ? PlayerPrefs.GetInt("ScreenshotIndex") : 1;
    }



    //take screenshot and add to custom path
    public void takepic()
    {
        canv.SetActive(false);
        Application.CaptureScreenshot(customPath + imageName + index + ".png", resolution);
        Debug.Log("Screenshot saved: " + customPath + " --- " + imageName + index);
        index++;
    }
    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("ScreenshotIndex", (index)); //make sure to set index for next pic
    }
=======
﻿using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEditor;

public class screen : MonoBehaviour
{
    public int resolution = 3; // 1= default, 2= 2x default, etc.
    public string imageName = "Screenshot_";
    public string customPath = ""; // leave blank for project file location
    public bool resetIndex = false;
    private int index = 0;
    public GameObject canv;

    void Awake()
    {
        if (resetIndex) PlayerPrefs.SetInt("ScreenshotIndex", 0); //reset index if player checks pictures
        if (customPath != "") //if there is a path save it there
        {
            if (!System.IO.Directory.Exists(customPath))
            {
                System.IO.Directory.CreateDirectory(customPath);
            }
        }
        index = PlayerPrefs.GetInt("ScreenshotIndex") != 0 ? PlayerPrefs.GetInt("ScreenshotIndex") : 1;
    }



    //take screenshot and add to custom path
    public void takepic()
    {
        canv.SetActive(false);
        Application.CaptureScreenshot(customPath + imageName + index + ".png", resolution);
        Debug.Log("Screenshot saved: " + customPath + " --- " + imageName + index);
        index++;
    }
    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("ScreenshotIndex", (index)); //make sure to set index for next pic
    }
>>>>>>> 710c6d937430837c776e15455509c65cc6306a86
}
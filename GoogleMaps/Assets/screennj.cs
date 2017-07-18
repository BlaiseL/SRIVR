using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEditor;

public class screennj : MonoBehaviour
{
    public int resolution = 3; // 1= default, 2= 2x default, etc.
    public string imageName = "Screenshot_";
    public string customPath = ""; // leave blank for project file location
    public bool resetIndex = false;
    private int index = 0;
    public GameObject canv;

    void Awake()
    {
        if (resetIndex) PlayerPrefs.SetInt("ScreenshotIndex", 0);
        if (customPath != "")
        {
            if (!System.IO.Directory.Exists(customPath))
            {
                System.IO.Directory.CreateDirectory(customPath);
            }
        }
        index = PlayerPrefs.GetInt("ScreenshotIndex") != 0 ? PlayerPrefs.GetInt("ScreenshotIndex") : 1;
    }




    public void takepic()
    {
        canv.SetActive(false);
        Application.CaptureScreenshot(customPath + imageName + index + ".png", resolution);
        Debug.LogWarning("Screenshot saved: " + customPath + " --- " + imageName + index);
        index++;
        //canv.SetActive(true);
    }
    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("ScreenshotIndex", (index));
    }
}
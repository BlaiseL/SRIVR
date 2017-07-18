using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolTIP : MonoBehaviour
{

    public SteamVR_TrackedObject trackedObj, otherTrackedObj;
    private bool menuUp;
<<<<<<< HEAD
    public GameObject leftMenu, rightMenu, coordinates;
=======
    public GameObject leftMenu, rightMenu;
<<<<<<< HEAD
>>>>>>> 710c6d937430837c776e15455509c65cc6306a86
=======
>>>>>>> 710c6d937430837c776e15455509c65cc6306a86
    // Initializes controller as tracked object
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        otherTrackedObj = GetComponent<SteamVR_TrackedObject>();
        leftMenu.GetComponent<Renderer>().enabled = false;
        rightMenu.GetComponent<Renderer>().enabled = false;
    }


    void FixedUpdate()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu) && !menuUp)
        {
            leftMenu.GetComponent<Renderer>().enabled = true;
            rightMenu.GetComponent<Renderer>().enabled = true;
            menuUp = true;
        }
        else if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu) && menuUp)
        {
            leftMenu.GetComponent<Renderer>().enabled = false;
            rightMenu.GetComponent<Renderer>().enabled = false;
            menuUp = false;
        }
    }
}
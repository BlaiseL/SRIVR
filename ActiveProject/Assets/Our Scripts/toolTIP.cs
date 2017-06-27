using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolTIP : MonoBehaviour
{

    public SteamVR_TrackedObject trackedObj, otherTrackedObj;
    GameObject attachedObject;
    private bool menuUp, datMenuUp;
    public GameObject tip, top, datMenu;
    // Initializes controller as tracked object
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        otherTrackedObj = GetComponent<SteamVR_TrackedObject>();
        tip.GetComponent<Renderer>().enabled = false;
        top.GetComponent<Renderer>().enabled = false;
        datMenu.GetComponent<Renderer>().enabled = false;
    }


    void FixedUpdate()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu) && !menuUp)
        {
            Debug.Log("WE SQUADD BOIES");
            tip.GetComponent<Renderer>().enabled = true;
            top.GetComponent<Renderer>().enabled = true;
            datMenu.GetComponent<Renderer>().enabled = false;
            menuUp = true;
        }
        else if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu) && menuUp)
        {
            Debug.Log("WE SQUADD BOIES");
            tip.GetComponent<Renderer>().enabled = false;
            top.GetComponent<Renderer>().enabled = false;
            menuUp = false;
        }
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu) && !menuUp)
        {
            Debug.Log("Just push it to git hub");
            datMenu.GetComponent<Renderer>().enabled = true;
            datMenuUp = true;
        }
        else if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu) && menuUp)
        {
            Debug.Log("Just push it to git hub");
            datMenu.GetComponent<Renderer>().enabled = false;
            datMenuUp = false;
        }
    }
}
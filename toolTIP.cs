using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolTIP : MonoBehaviour
{

    SteamVR_TrackedObject trackedObj;
    GameObject attachedObject;
    private bool menuUp;
    public GameObject tip, top;
    // Initializes controller as tracked object
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
        tip.GetComponent<Renderer>().enabled = false;
        top.GetComponent<Renderer>().enabled = false;
    }


    void FixedUpdate()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        
        if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu) && !menuUp)
        {
            Debug.Log("WE SQUADD BOIES");
            tip.GetComponent<Renderer>().enabled = true;
            top.GetComponent<Renderer>().enabled = true;
            menuUp = true;
        }
        else if (device.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu) && menuUp)
        {
            Debug.Log("WE SQUADD BOIES");
            tip.GetComponent<Renderer>().enabled = false;
            top.GetComponent<Renderer>().enabled = false;
            menuUp = false;
        }

    }
}
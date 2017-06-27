using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
/*
 * Generate cylinder to teleport to
 * 
 * */
public class GenerateCapsules1 : MonoBehaviour {
    public SteamVR_TrackedObject leftCtrl;
    public SteamVR_TrackedObject rightCtrl;
    ArrayList list=new ArrayList();
    public Dropdown dropdown;
    public GameObject c;
    int counter = -1;
    public GameObject cam;
    int lcount = -1;
    Color[] label = {Color.blue, Color.green, Color.magenta, Color.yellow, Color.red, Color.white };
    int deletespot=-1;
    int num=0;
    bool menuUp = false; 

    //intitialize menu as inactive and  pointer as inactive
    private void Start()
    {
        c.SetActive(false);
        dropdown.Hide();
    }

    //function to add to the menu
    public void Dropdown_Add(int y)
    {
        dropdown.options.Add(new Dropdown.OptionData("Teleport Pad " + y));
    }
    //Remove from the menu
    public void Dropdown_Remove(int x)
    {
        dropdown.options.RemoveAt(x);
        dropdown.RefreshShownValue();
    }

    private void Update()
    {
        int size=list.Count;
        var deviceLeft = SteamVR_Controller.Input((int)leftCtrl.index);
        var deviceRight = SteamVR_Controller.Input((int)rightCtrl.index);
        Vector2 touchpad = deviceLeft.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
        //touchpad up generates a capsule and stores it in arraylist
        if (touchpad.y > .7f && deviceLeft.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //components of capsule 
            GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cylinder.AddComponent< Rigidbody > ();
            Rigidbody cyl = cylinder.GetComponent<Rigidbody>();
            cyl.isKinematic = true;
            cyl.useGravity = false;
            cylinder.transform.localScale = new Vector3(1 , 0.01f , 1);
            cylinder.transform.position = cam.transform.position;
            cylinder.GetComponent<Renderer>().material.color= Color.blue;
            list.Add(cylinder);
            num++;
            Dropdown_Add(num);
        }

        // touchpad click right cycles and teleports
        if (touchpad.x > .7f && deviceLeft.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            counter++;
            counter %= size;
            deletespot=counter;
            cam.transform.position = ((GameObject)(list[counter])).transform.position;
        }
        //touchpad click down destroys the pad your at
        if (touchpad.y < -.7f && deviceLeft.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //destroy the last touchpad if youre on it
            if(cam.transform.position == ((GameObject)(list[size - 1])).transform.position)
            {
                Destroy((GameObject)(list[list.Count-1]));
                list.RemoveAt(size-1); //remove from list
                Dropdown_Remove(size); //remove from menu
            }
            //destroy the touchpad you teleport to
            else if (counter > -1)
            {
                Destroy((GameObject)(list[counter]));
                deletespot=counter;
                list.RemoveAt(counter);
                Dropdown_Remove(deletespot+1);
                counter--;
            }
            if (list.Count!=0)
                counter %= size;
        }

        //change the color of the padd
        if (touchpad.x < -.7f && deviceLeft.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            lcount++;
            lcount %= 6;
            //change the color of the pad youre on
            if (cam.transform.position == ((GameObject)(list[list.Count - 1])).transform.position)
            {
                ((GameObject)(list[list.Count - 1])).GetComponent<Renderer>().material.color=(Color)(label[lcount]);
            }
            //change the color of the pad you teleported to
            else if (counter>-1)
            {
                ((GameObject)(list[counter])).GetComponent<Renderer>().material.color = (Color)(label[lcount]);
            }
        }

        /*
		Toggle Menu

        */
        //if the menu is not up clicking will start it 
        if (deviceRight.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu) && !menuUp)
        {
            c.SetActive(true);
            dropdown.Show();
            menuUp = true;
            cam.GetComponent<Zoom>().enabled = false;
        }
        //if the menu is up disavle it on click
        else if (deviceRight.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu) && menuUp)
        {
            c.SetActive(false);
            menuUp = false;
            cam.GetComponent<Zoom>().enabled = true;
        }
        
    }
    //teleport to pad function for changeonvalue in unity
    public void teleportMenu()
    {
        int g = dropdown.value;
        counter = g - 1;
        if (g != 0)
        {
            cam.transform.position = ((GameObject)(list[g - 1])).transform.position;
        }
    }
}

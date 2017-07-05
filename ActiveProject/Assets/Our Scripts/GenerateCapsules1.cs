using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using System.IO;
/*
 * Generate cylinder to teleport to and add it to the dropdown menu
 * Delete created cylinders from the world and dropdown 
 * Save your current status and Pads you created to a text file 
 * */
public class GenerateCapsules1 : MonoBehaviour
{
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
    int num=1;
    bool menuUp = false;
    public GameObject text;
    public String fname;
    System.IO.StreamWriter wfile;
    System.IO.StreamReader reader;
    ArrayList deletedLines = new ArrayList();
    

    //intitialize menu as inactive and  pointer as inactive
    private void Start()
    {

        var fs = File.Open(fname, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        wfile = new System.IO.StreamWriter(fs);
        c.SetActive(false);
        dropdown.Hide();
        reader = new System.IO.StreamReader(fs);
        string line;
        Debug.Log("HERE");
        while (( line=reader.ReadLine() ) != null)
        {
            Debug.Log(line);
            Debug.Log("Should be adding pads");
            string[] arr = line.Split(',');
            //This should only print lines we didnt delete
            if (arr.Length==4)
            {
                num = Int16.Parse(arr[0]);
                add(new Vector3(float.Parse(arr[1]), float.Parse(arr[2]), float.Parse(arr[3])), num);
            }
        }
    }

    //function to add to the menu
    public void Dropdown_Add(int y)
    {
        dropdown.Show();
        dropdown.options.Add(new Dropdown.OptionData("Teleport Pad " + y));
        dropdown.RefreshShownValue();
    }
    //Remove from the menu
    public void Dropdown_Remove(int x)
    {
        List<Dropdown.OptionData> menuOptions = dropdown.GetComponent<Dropdown>().options;
        string value = menuOptions[x].text;
        string[] arr = value.Split(' ');
        int r= Int16.Parse(arr[2]);
        Debug.Log(r);
        dropdown.options.RemoveAt(x);
        dropdown.RefreshShownValue();
        deletedLines.Add(r);
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
            wfile.WriteLine(num + "," + cam.transform.position.x + "," + cam.transform.position.y +
             "," + cam.transform.position.z);
            add(cam.transform.position, num);
        

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
    void add(Vector3 add, int n)
    {
        //components of capsule 
        GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        cylinder.AddComponent<Rigidbody>();
        Rigidbody cyl = cylinder.GetComponent<Rigidbody>();
        cyl.isKinematic = true;
        cyl.useGravity = false;
        cylinder.transform.localScale = new Vector3(1, 0.01f, 1);
        cylinder.transform.position = add;
        cylinder.GetComponent<Renderer>().material.color = Color.blue;
        list.Add(cylinder);

        //create numbers for pads
        GameObject label = Instantiate(text);
        label.GetComponentInChildren<Text>().text = n + " ";
        label.transform.position = new Vector3(add.x, add.y + .1f, add.z);
        label.transform.Rotate(0, 90, 0);
        label.transform.SetParent(cylinder.transform);
        Dropdown_Add(n);
        num++;
    }
    private void OnApplicationQuit()
    {
        deletedLines.Sort();
        wfile.Close();
        reader.Close();
        var fs = File.Open(fname, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        wfile = new System.IO.StreamWriter(fs);
        reader = new System.IO.StreamReader(fs);
        string line;
        int check = 1;
        int ctr = 0;
        while ((line = reader.ReadLine()) != null)
        {
            Debug.Log(check);
            if (ctr != deletedLines.Count && check == (int)(deletedLines[ctr]))
            {
                Debug.Log((int)deletedLines[ctr] + " delete this line");
                wfile.WriteLine(line + ", dinosaur");
                ctr++;
            }
            else
            {
                wfile.WriteLine(line);
            }
            check++;
        }
        wfile.Close();
        reader.Close();
    }
}

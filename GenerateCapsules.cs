using UnityEngine;
using System.Collections;
/*
 * Generate cylinder to teleport to
 * 
 * */
public class GenerateCapsules : MonoBehaviour {
    public SteamVR_TrackedObject leftCtrl;
    ArrayList list=new ArrayList();
    int counter = -1;
    public GameObject cam;
    int lcount = -1;
    Colors[] label = {Colors.blue, Colors.green, Colors.magenta, Colors.yellow, Colors.orange };

    private void Update()
    {
        var deviceLeft = SteamVR_Controller.Input((int)leftCtrl.index);
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
        }
        // touchpad click right cycles and teleports
        if (touchpad.x > .7f && deviceLeft.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            counter++;
            counter %= list.Count;
            cam.transform.position = ((GameObject)(list[counter])).transform.position;
            Debug.Log("Size: " + list.Count);
            Debug.Log(" Counter: " + counter);
        }
        //touchpad click down destroys the pad your at
        if (touchpad.y < -.7f && deviceLeft.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            //destroy the touchpad your on?
            if(cam.transform.position == ((GameObject)(list[list.Count - 1])).transform.position){
                Destroy((GameObject)(list[list.Count-1]));
                list.RemoveAt(list.Count-1);
            }
            //destroy the touchpad you teleporte to
            else if (counter > -1)
            {
                Debug.Log("trying to destroy and counter:" + counter);
                Destroy((GameObject)(list[counter]));
                list.RemoveAt(counter);
            }
            counter %= list.Count;
        }

        //change the color of the padd
        if (touchpad.x < -.7f && deviceLeft.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad))
        {
            lcount++;
            lcount %= 6;
            //change the color of the pad youre on
            if (cam.transform.position == ((GameObject)(list[list.Count - 1])).transform.position)
            {
                ((GameObject)(list[list.Count - 1])).GetComponent<Renderer>().material.color=(Color)(labels[lcount]);
            }
            //change the color of the pad you teleported to
            else if (counter>-1)
            {
                ((GameObject)(list[counter])).GetComponent<Renderer>().material.color = (Color)(labels[lcount]);
            }
        }
    }
    
}

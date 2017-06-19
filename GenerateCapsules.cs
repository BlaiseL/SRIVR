using UnityEngine;
using System.Collections;
/*
 * Generate cylinder to teleport to
 * 
 * */
public class GenerateCapsules : MonoBehaviour {
    public SteamVR_TrackedObject leftCtrl;
    ArrayList list=new ArrayList();
    int counter = 0;
    public GameObject cam;


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
            counter %= list.Count;
            cam.transform.position = ((GameObject)(list[counter])).transform.position;
            counter++;
            Debug.Log("Size: " + list.Count);
            Debug.Log(" Counter: " + counter);
        }
        //touchpad click down destroys the pad your at
        if (touchpad.y < -.7f && deviceLeft.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) && counter>0)
        {
            counter--;
            Debug.Log("trying to destroy and counter:" + counter);
            Destroy((GameObject)(list[counter]));
            list.RemoveAt(counter);
        }
    }
    
}

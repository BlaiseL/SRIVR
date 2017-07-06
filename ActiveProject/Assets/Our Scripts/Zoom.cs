using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * The script to zoom
 * Actually scales the world and the surroundings 
 * 
 * */
public class Zoom : MonoBehaviour
{
    //initialize our vars for the code
    public GameObject c1, c2; //controllers 
    public SteamVR_TrackedObject leftCtrl, rightCtrl; //controllers as tracked objects 
    Vector3 lastPositionl;
    Vector3 lastPositionr;
    public GameObject World;
    public GameObject cube;
    float x = 1;
    float y = 1;
    float z = 1;
    float xc = 0.5f;
    public MeshRenderer currentRenderer;
    public Texture2D small;
    public Texture2D medium;
    public Texture2D large;
    public Texture2D extraLarge;


    private void Start()
    {
        currentRenderer = cube.GetComponent<MeshRenderer>();
    }
    //update the code works by frame 
    private void Update()
    {
        lastPositionl = c1.transform.position; //updates the position of the left controller
        lastPositionr = c2.transform.position; //updates the position of the right controller
    }
    /*
    * Code that compares the new position to the old position and zooms
    */
    private void FixedUpdate()
    {
        cube.GetComponent<Renderer>().material.color = new Color(1, 1, 1, .5f);// makes cube transparent
        cube.GetComponent<Renderer>().enabled = false;
        var deviceLeft = SteamVR_Controller.Input((int)leftCtrl.index);
        var deviceRight = SteamVR_Controller.Input((int)rightCtrl.index);
        /*
         * Debug logs testing various grip presses
         * 
         */
        if (deviceLeft.GetPressDown(SteamVR_Controller.ButtonMask.ApplicationMenu))
        {
            Debug.Log("You pushed the menu button");
        }

        if (deviceLeft.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log("You squeeeeezed ME Left");
        }
        if (deviceRight.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log("You squeeeeezed ME Right");
        }
        if (deviceRight.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You reset the scale");
            xc = 0.5f;
            x = 1;
            y = 1;
            z = 1; 
            cube.transform.localScale = new Vector3(xc, xc, xc);
            World.transform.localScale = new Vector3(1, 1, 1);
        }
        /*
         * Activates on pressing of both grips
         * Compares initial distance between the controllers to the updated distance and zooms in or out
         */
        if (deviceRight.GetPress(SteamVR_Controller.ButtonMask.Grip) && deviceLeft.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
            cube.GetComponent<Renderer>().enabled = true;
            Debug.Log("You squeezed both");
            float lastdist = Vector3.Distance(lastPositionl, lastPositionr);
            float dist = Vector3.Distance(c1.transform.position, c2.transform.position);
            float diff = dist - lastdist;
            //Debug.Log(lastdist + "  " + dist);
            //Zoom out
            if (diff > 0.02 && deviceRight.GetPress(SteamVR_Controller.ButtonMask.Grip) && deviceLeft.GetPress(SteamVR_Controller.ButtonMask.Grip))
            {
                if (xc < 9f)
                {
                    Debug.Log("You are zooming out");
                    //Change the SCALE 
                    World.transform.localScale = new Vector3(x += .1f, y += .1f, z += .1f);
                    xc += .05f;
                    cube.transform.localScale = new Vector3(xc, xc, xc);
                }
            }
            //Zoom in
            if (diff < -0.02 && deviceRight.GetPress(SteamVR_Controller.ButtonMask.Grip) && deviceLeft.GetPress(SteamVR_Controller.ButtonMask.Grip))
            {
                if (x > .1f && xc >.5f)
                {
                    //Change the Scale smaller
                    Debug.Log("You are zooming in");
                    World.transform.localScale = new Vector3(x -= .1f, y -= .1f, z -= .1f);
                    xc -= .05f;
                    cube.transform.localScale = new Vector3(xc, xc, xc);
                }
            }
        }
        if (cube.transform.localScale.x <= 2 )
        {
            currentRenderer.material.SetTexture("_MainTex", small);
        }
        else if (cube.transform.localScale.x > 2 && cube.transform.localScale.x <= 5)
        {
            currentRenderer.material.SetTexture("_MainTex", medium);
        }
        else if (cube.transform.localScale.x > 5 && cube.transform.localScale.x <= 8)
        {
            currentRenderer.material.SetTexture("_MainTex", large);
        }
        else if (cube.transform.localScale.x > 8)
        {
            currentRenderer.material.SetTexture("_MainTex", extraLarge);
        }
    }
    //2, 5, 8 are the three points of change
}

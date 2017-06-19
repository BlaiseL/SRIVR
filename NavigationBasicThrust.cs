using UnityEngine;
using System.Collections;
/*
 * Script for flight, using touchpad
 * Modified from the NavigationBasicThrust asset in the asset store
 */
[RequireComponent(typeof(SteamVR_TrackedObject))]
public class NavigationBasicThrust : MonoBehaviour
{
    public Rigidbody NaviBase;
    public Vector3 ThrustDirection;
    public float ThrustForce;
    public bool ShowTrustMockup = true;
    public GameObject ThrustMockup;

    SteamVR_TrackedObject trackedObj;
    FixedJoint joint;
    GameObject attachedObject;
    Vector3 tempVector;
    // Initializes controller as tracked object
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void FixedUpdate()
    {
        var device = SteamVR_Controller.Input((int)trackedObj.index);

        // add force to user in direction of controller
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Vector2 touchpad = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
            if (touchpad.y > .7f)
            {
                Debug.Log("Touchpad pressed moving up");
                tempVector = Quaternion.Euler(ThrustDirection) * Vector3.forward;
                NaviBase.AddForce(transform.rotation * tempVector * ThrustForce);
                NaviBase.maxAngularVelocity = 2f;
            }
            //If touching the bottom half of the touchpad adds force to the user in the opposite direction of the controller
            else if (touchpad.y < -.7f)
            {
                Debug.Log("Touchpad pressed moving up");
                tempVector = Quaternion.Euler(ThrustDirection) * new Vector3(0, 0, -1);
                NaviBase.AddForce(transform.rotation * tempVector * ThrustForce);
                NaviBase.maxAngularVelocity = 2f;
            }
            //If touching the right half of the touchpad adds force on the user to the right
            else if (touchpad.x > .7f)
            {
                Debug.Log("Touchpad pressed moving right");
                tempVector = Quaternion.Euler(ThrustDirection) * new Vector3(1, 0, 0);
                NaviBase.AddForce(transform.rotation * tempVector * ThrustForce);
                NaviBase.maxAngularVelocity = 2f;
            }
            //If touching the left half of the touchpad adds force on the user towards the left
            else if (touchpad.x < -.7f)
            {
                Debug.Log("Touchpad pressed moving left");
                tempVector = Quaternion.Euler(ThrustDirection) * new Vector3(-1, 0, 0);
                NaviBase.AddForce(transform.rotation * tempVector * ThrustForce);
                NaviBase.maxAngularVelocity = 2f;
            }
        }

        //stop moving
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Debug.Log("Released the trackpad");
            NaviBase.velocity = Vector3.zero;
            NaviBase.angularVelocity = Vector3.zero;
        }
    }
}
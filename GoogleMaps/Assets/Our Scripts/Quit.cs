using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Quit : MonoBehaviour
{
    //Allows the user to quit the application
    public void quit()
    {
        EditorApplication.isPlaying = false;
    }
    
}

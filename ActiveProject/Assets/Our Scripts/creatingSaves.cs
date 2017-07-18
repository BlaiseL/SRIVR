using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class creatingSaves : MonoBehaviour {
    public string saveName;
	// Use this for initialization
	void Awake () {
        string createText = "Hello and Welcome" + Environment.NewLine;
        File.AppendAllText(@"C:\Users\MSC\Desktop\SaveFiles\WriteLines.txt", createText);
        // Open the file to read from.
        string readText = File.ReadAllText(@"C:\Users\MSC\Desktop\SaveFiles\save.txt");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

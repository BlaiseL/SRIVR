using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class ShipInputs : MonoBehaviour
{
    public String datapath;
    StreamReader reader;
    Dictionary<string, string> Ships;

	// Use this for initialization
	void Start ()
    {
        Ships = new Dictionary<string, string>();
    }

    // Update is called once per frame
    void Update()
    {
        var orgfile = File.Open(datapath, FileMode.Open, FileAccess.Read);
        reader = new StreamReader(orgfile);
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] arr = line.Split(',');
            if (Ships.ContainsKey((arr[2]))== false)
            {
                Debug.Log("Adding Into Dictionary");
                Debug.Log(arr[2]);
                Ships.Add(arr[2], arr[1] + "," + arr[0] + "," + arr[3]);
                addShips(arr[2], arr[1], arr[0], arr[3]);
            }
            else
            {
                Ships[arr[2]] = arr[1] + "," + arr[0] + "," + arr[3];
                moveShips(arr[2], arr[1], arr[0]);
            }
        }
    }
    void addShips(string key, string lng, string lat, string type)
    {
        GameObject ship = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        ship.name = key;
        ship.tag = key;
        ship.AddComponent<Rigidbody>();
        Rigidbody cyl = ship.GetComponent<Rigidbody>();
        cyl.isKinematic = true;
        cyl.useGravity = false;
        ship.transform.localScale = new Vector3(1, 0.01f, 1);
        ship.transform.position = new Vector3(float.Parse(lat), 0, float.Parse(lng));
    }
    void moveShips(string key, string lng, string lat)
    {
        (GameObject.Find(key)).transform.position = new Vector3(float.Parse(lat), 0, float.Parse(lng));
        (GameObject.FindWithTag(key)).transform.position = new Vector3(float.Parse(lat), 0, float.Parse(lng));
    }
}

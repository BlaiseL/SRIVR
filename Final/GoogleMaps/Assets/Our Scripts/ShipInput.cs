using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;


public class ShipInput : MonoBehaviour
{
    public GameObject planeT;
    public GameObject cruiser, tug, carrier, crew, weap;
    Dictionary<string, string> Ships;
    public double latitude1, latitude2;
    public double longitude1, longitude2;
    private double ratioLatitudeZ;
    private double ratioLongitudeX;
    UdpClient client;
    IPEndPoint RemoteIpEndPoint;

    // Use this for initialization
    void Start()
    {
        Renderer[] rs = cruiser.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
            r.enabled = false;
        Renderer[] cs = crew.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in cs)
            r.enabled = false;
        Renderer[] ds = carrier.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in ds)
            r.enabled = false;
        Renderer[] gs = tug.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in gs)
            r.enabled = false;
        Renderer[] ls = weap.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in ls)
            r.enabled = false;
        Ships = new Dictionary<string, string>();
        ratioLongitudeX = Math.Round((Math.Abs(longitude2 - longitude1) / (planeT.transform.localScale.x * 10.0)), 12);
        ratioLatitudeZ = Math.Round((Math.Abs(latitude2 - latitude1) / (planeT.transform.localScale.z * 10.0)), 12);
        //Setting up UDP socket to recieve AIS feed and run in a background thread
        client = new UdpClient(5003);
        client.Client.Blocking = false;
        RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        //Creating Ships and moving them in the map, keeping track with a dictionary
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        while (true)
        {
            byte[] recieveBytes = client.Receive(ref RemoteIpEndPoint);
            string returndata = Encoding.ASCII.GetString(recieveBytes);
            string[] arr = returndata.Split(',');
            if (Ships.ContainsKey((arr[2])) == false)
            {
                Debug.Log("Adding Into Dictionary");
                Debug.Log(arr[2]);
                Ships.Add(arr[2], arr[1] + "," + arr[0] + "," + arr[3]);
                addShips(arr[2], arr[1], arr[0], arr[3]);
            }
            else
            {
                Debug.Log("Moving1");
                Ships[arr[2]] = arr[1] + "," + arr[0] + "," + arr[3];
                moveShips(arr[2], arr[1], arr[0]);
            }
        }
    }

    void addShips(string key, string lng, string lat, string type)
    {
        Debug.Log("Making Ships");
        GameObject ship = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        ship.name = key;
        ship.AddComponent<Rigidbody>();
        Rigidbody cyl = ship.GetComponent<Rigidbody>();
        cyl.isKinematic = true;
        cyl.useGravity = false;
        ship.transform.localScale = new Vector3(1, 1f, 1);
        ship.transform.position = new Vector3((float.Parse(lng) / (float)ratioLatitudeZ)-((float)((latitude1) / (float)ratioLatitudeZ)), 0, ((float.Parse(lat) / (float)ratioLongitudeX) - (float)((longitude1))/ (float)ratioLongitudeX));
        if (float.Parse(type) > 30 && float.Parse(type) < 40 )
        {
            GameObject repre = Instantiate(tug);
            Renderer[] rs = repre.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rs)
                r.enabled = true;
            repre.transform.position = new Vector3(ship.transform.position.x, ship.transform.position.y + .1f, ship.transform.position.z);
            repre.transform.Rotate(0, 0, 0);
            repre.transform.SetParent(ship.transform);
        }
        else if (float.Parse(type) >39 && float.Parse(type) <50)
        {
            GameObject repre = Instantiate(weap);
            Renderer[] rs = repre.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rs)
                r.enabled = true;
            repre.transform.position = new Vector3(ship.transform.position.x, ship.transform.position.y + .1f, ship.transform.position.z);
            repre.transform.Rotate(0, 0, 0);
            repre.transform.SetParent(ship.transform);
        }
        else if (float.Parse(type) > 49 && float.Parse(type) < 70)
        {
            GameObject repre = Instantiate(cruiser);
            Renderer[] rs = repre.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rs)
                r.enabled = true;
            repre.transform.position = new Vector3(ship.transform.position.x, ship.transform.position.y + .1f, ship.transform.position.z+10f);
            repre.transform.Rotate(0, 0, 0);
            repre.transform.SetParent(ship.transform);
        }
        else if (float.Parse(type) > 69 && float.Parse(type) < 90)
        {
            GameObject repre = Instantiate(carrier);
            Renderer[] rs = repre.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rs)
                r.enabled = true;
            repre.transform.position = new Vector3(ship.transform.position.x, ship.transform.position.y + .1f, ship.transform.position.z);
            repre.transform.Rotate(0, 0, 0);
            repre.transform.SetParent(ship.transform);
        }
        else if (float.Parse(type) > 89)
        {
            GameObject repre = Instantiate(crew);
            Renderer[] rs = repre.GetComponentsInChildren<Renderer>();
            foreach (Renderer r in rs)
                r.enabled = true;
            repre.transform.position = new Vector3(ship.transform.position.x, ship.transform.position.y + 4f, ship.transform.position.z);
            repre.transform.Rotate(0, 0, 0);
            repre.transform.SetParent(ship.transform);
        }
        else
        {
            GameObject repre = Instantiate(crew);
            Renderer[] rs = repre.GetComponentsInChildren<Renderer>();
            foreach(Renderer r in rs)
            r.enabled = true;
            repre.transform.position = new Vector3(ship.transform.position.x, ship.transform.position.y + 4f, ship.transform.position.z);
            repre.transform.Rotate(0, 0, 0);
            repre.transform.SetParent(ship.transform);
        }
    }

    void moveShips(string key, string lng, string lat)
    {
        (GameObject.Find(key)).transform.position = new Vector3((float.Parse(lng) / (float)ratioLatitudeZ) - ((float)((latitude1) / (float)ratioLatitudeZ)), 0, ((float.Parse(lat) / (float)ratioLongitudeX) - (float)((longitude1)) / (float)ratioLongitudeX));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using UnityEngine.UI;
using System.Text;
using System.Threading;


public class udp : MonoBehaviour
{
    UdpClient client;
    Thread recieveThread;
    IPEndPoint RemoteIpEndPoint;

    // Use this for initialization
    void Start()
    {
        client = new UdpClient(5003);
        RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
        initthread();
    }

    private void initthread()
    {
        recieveThread = new Thread(new ThreadStart(RecieveData));
        recieveThread.IsBackground = true;
        recieveThread.Start();
        Debug.Log("Start");
    }

    private void RecieveData()
    {
        while (true)
        {
            byte[] recieveBytes = client.Receive(ref RemoteIpEndPoint);
            string returndata = Encoding.ASCII.GetString(recieveBytes);
            Debug.Log(returndata);
        }
    }

    private void OnApplicationQuit()
    {
        recieveThread.Abort();
    }
    // Update is called once per frame
}

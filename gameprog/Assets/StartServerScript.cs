using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;

public class StartServerScript : MonoBehaviour
{
    private IPAddress ipAddress;
    public static TcpListener listener;
    private string ip = "192.168.1.198";
    private ChatScript chatScript;


    private void Start()
    {
        chatScript = FindAnyObjectByType<ChatScript>();
        if (PlayerPrefs.GetString("IsServerStart") == "true")
        {
            StartServer();
        }
    }
    public async void StartServer()
    {
        ipAddress = IPAddress.Parse(ip);
        int port = 8080;
        listener = new TcpListener(ipAddress, port);
        listener.Start();
        print($"Serveur démarré sur {ipAddress}:{port}");
        await chatScript.ClientConnect(listener);
    }
}

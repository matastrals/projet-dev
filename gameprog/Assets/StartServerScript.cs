using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

public class StartServerScript : MonoBehaviour
{
    private IPAddress ipAddress;
    public static TcpListener listener;
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
        string serverIpandPort = PlayerPrefs.GetString("Ip server");
        Match regex = Regex.Match(serverIpandPort, @"(?<ip>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}):(?<port>\d+)");
        string serverIP = regex.Groups["ip"].Value;
        string port = regex.Groups["port"].Value;

        ipAddress = IPAddress.Parse(serverIP);
        listener = new TcpListener(ipAddress, int.Parse(port));
        listener.Start();
        print($"Serveur démarré sur {ipAddress}:{int.Parse(port)}");
        await chatScript.ClientConnect(listener);
    }
}

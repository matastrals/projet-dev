using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using System.Net;
using System;
using UnityEngine.Rendering;
using UnityEditor.Experimental.GraphView;

public class TestClient : MonoBehaviour
{
    private bool socketReady;
    private TcpClient socket;
    private NetworkStream stream;
    private StreamWriter writer;
    private StreamReader reader;
    private ChatManager chatManager;
    private List<string> allMessage;
    private string username;
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        chatManager = FindAnyObjectByType<ChatManager>();
        allMessage = new List<string>();
        username = PlayerPrefs.GetString("PlayerName");
    }

    public void ConnectToServer()
    {
       if(socketReady) return;

        string host = PlayerPrefs.GetString("Ip");
        int port;
        int.TryParse(PlayerPrefs.GetString("Port"), out port);

        try
        {
            socket = new TcpClient(host, port);
            stream = socket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
            socketReady = true;
            Send(username);
        }
        catch (Exception e)
        {
            Debug.Log("Socket error : " + e.Message);
        }
    }

    private void Update()
    {
        if (socketReady)
        {
            if (stream.DataAvailable)
            {
                string data = reader.ReadLine();
                if (data != null)
                {
                    OnIncomingData(data);
                }
            }
        }
    }

    private void OnIncomingData(string data)
    {
        allMessage.Add(data);
    }

    public void Send(string data)
    {
        if (!socketReady) return;

        data = $"{username} : {data}";
        writer.WriteLine(data);
        writer.Flush();
    }

    public List<string> GetAllMessage()
    {
        return(allMessage);
    }
}

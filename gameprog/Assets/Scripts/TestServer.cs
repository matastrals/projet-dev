using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Sockets;
using UnityEngine;
using System;
using System.Net;
using System.IO;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;
using TMPro;

public class TestServer : MonoBehaviour
{
    private List<ServerClient> clients;
    private List<ServerClient> disconnectList;

    private int port;
    private TcpListener server;
    private bool serverStarted;
    public GameObject ErrorBeginNoIpOrPortParent;
    private TMP_Text ErrorBeginNoIpOrPort;


    private void Start()
    {
        ErrorBeginNoIpOrPort = ErrorBeginNoIpOrPortParent.GetComponentInChildren<TMP_Text>();
        ErrorBeginNoIpOrPort.gameObject.SetActive(false);
    }
    public void StartServer()
    {
        DontDestroyOnLoad(this.gameObject);
        string ip = PlayerPrefs.GetString("Ip");
        int.TryParse(PlayerPrefs.GetString("Port"), out port);
        clients = new List<ServerClient>();
        disconnectList = new List<ServerClient>();
        if (ip == null || port == 0)
        {
            ErrorBeginNoIpOrPort.text = "You need to put your ip and a port for begin to host";
            ErrorBeginNoIpOrPort.color = Color.red;
            ErrorBeginNoIpOrPort.gameObject.SetActive(true);
            StartCoroutine(ErrorBeginIpOrPort());
            return;
        }
        try
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();

            StartListening();
            serverStarted = true;
            Debug.Log("Server has been started on port " + port.ToString());
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e.Message);
        }
    }

    IEnumerator ErrorBeginIpOrPort()
    {
        yield return new WaitForSeconds(3);
        ErrorBeginNoIpOrPort.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!serverStarted)
        {
            return;
        }

        foreach (ServerClient c in clients) 
        {
            if (!IsConnected(c.tcp))
            {
                c.tcp.Close();
                disconnectList.Add(c);
                continue;
            } else
            {
                NetworkStream s = c.tcp.GetStream();
                if(s.DataAvailable)
                {
                    StreamReader reader = new StreamReader(s, true);
                    string data = reader.ReadLine();

                    if (data != null)
                    {
                        OnIncomingData(c, data);
                    }
                }
            }
        }
    }

    private void StartListening()
    {
        server.BeginAcceptTcpClient(AcceotTcoCkient, server);
    }

    private bool IsConnected(TcpClient c)
    {
        try
        {
            if (c != null && c.Client != null && c.Client.Connected)
            {
                if (c.Client.Poll(0, SelectMode.SelectRead))
                {
                    return !(c.Client.Receive(new byte[1], SocketFlags.Peek)== 0);
                }
                
                return true;
            } else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    private void AcceotTcoCkient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;

        clients.Add(new ServerClient(listener.EndAcceptTcpClient(ar)));
        StartListening();

        Broadcast(clients[clients.Count- 1].clientName + " has connected", clients);
    }

    private void OnIncomingData(ServerClient c, string data)
    {
        Broadcast(data, clients);
    }

    private void Broadcast(string data, List<ServerClient> cl )
    {
        foreach (ServerClient c in cl)
        {
            try
            {
                StreamWriter writer = new StreamWriter(c.tcp.GetStream());
                writer.WriteLine(data);
                writer.Flush();
            }
            catch (Exception e)
            {
                Debug.Log("Write error : " + e.Message + " to client " + c.clientName);
            }
        }
    }
}

public class ServerClient
{
    public TcpClient tcp;
    public string clientName;

    public ServerClient(TcpClient clientSocket)
    {
        clientName = "Guest";
        tcp = clientSocket;
    }
}

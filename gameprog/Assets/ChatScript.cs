using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ChatScript : MonoBehaviour
{
    public TMP_InputField inputChat;
    public TMP_Text textChatLocal;
    private Dictionary<string, TcpClient> CLIENT;
    private PlayerMovement playerMovement;
    private MenuScript menuScript;
    private InventoryScript inventoryScript;
    private List<string> listChat;
    private int tailleList = 10;
    private StartServerScript serverScript;
    private TcpClient client;
    private IPAddress clientAddress;
    private string textOnline;
    private TcpClient server;

    private void Start()
    {
        serverScript = FindAnyObjectByType<StartServerScript>();
        menuScript = FindObjectOfType<MenuScript>();
        inventoryScript = FindObjectOfType<InventoryScript>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.isInMenu = false;
        inputChat.gameObject.SetActive(false);
        listChat = new List<string>();
        CLIENT = new Dictionary<string, TcpClient>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
           if (inputChat.gameObject.activeSelf)
           {
                playerMovement.isInMenu = false;
                menuScript.enabled = true;
                inventoryScript.enabled = true;
                inputChat.gameObject.SetActive(false);
                inputChat.text = "";
           } else
           {
                menuScript.enabled = false;
                inventoryScript.enabled = false;
                playerMovement.isInMenu = true;
                inputChat.gameObject.SetActive(true);
           }
        }
    }

    public async void ChatLocal(string text)
    {
        
        if (PlayerPrefs.GetString("IsServerStart") == "true" & CLIENT.Count != 0)
        {
            NetworkStream stream = client.GetStream();
            byte[] dataToSend = Encoding.ASCII.GetBytes(text);
            await stream.WriteAsync(dataToSend, 0, dataToSend.Length);
        }

        PrintOnChat(text);
    }

    public async Task ClientConnect(TcpListener listener)
    {
        while (true)
        {
            try
            {
                print("En attente d'un client");
                client = await listener.AcceptTcpClientAsync();
                clientAddress = ((IPEndPoint)client.Client.RemoteEndPoint).Address;
                CLIENT.Add(clientAddress.ToString(), client);
                print($"Client {clientAddress} connecté !");
                Task.Run(async () => await ChatOnline(client));
            }
            catch(Exception ex)
            {
                print(ex.ToString());
            }
        }
    }

    public async Task ChatOnline(TcpClient client)
    {
        NetworkStream stream = client.GetStream();

        byte[] buffer = new byte[1024];
        int bytesRead;

        while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
        {
            textOnline = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            print($"Message reçu du client ({((IPEndPoint)client.Client.RemoteEndPoint).Address}): {textOnline}");

            byte[] dataToSend = Encoding.ASCII.GetBytes($"Message reçu : {textOnline}");
            await stream.WriteAsync(dataToSend, 0, dataToSend.Length);
        }
        print($"Message du client (pour etre sur) {textOnline}");
        PrintOnChat(textOnline);
    }

    private void PrintOnChat(string text)
    {
        if (Regex.IsMatch(text, @"^\s*$"))
        {
            return;
        }
        if (text.Length >= 50)
        {
            return;
        }
        listChat.Insert(listChat.Count, text);
        if (listChat.Count > tailleList)
        {
            listChat.RemoveAt(0);
        }
        string stringChat = string.Join("\n", listChat);
        textChatLocal.text = stringChat;
    }

    public void ConnectToServer()
    {
        try
        {
            string serverIP = "10.33.72.75";
            int port = 8080; 

            server = new TcpClient(serverIP, port);

            print("Connexion établie");
        }
        catch (Exception e)
        {
            print("Erreur: " + e.Message);
        }
    }
}

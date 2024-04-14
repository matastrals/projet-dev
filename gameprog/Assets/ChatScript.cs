using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.PackageManager;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private TcpClient server;
    private string isServerStart;
    private ScoreManager scoreManagerScript;

    private void Start()
    {
        scoreManagerScript = FindAnyObjectByType<ScoreManager>();
        serverScript = FindAnyObjectByType<StartServerScript>();
        menuScript = FindObjectOfType<MenuScript>();
        inventoryScript = FindObjectOfType<InventoryScript>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.isInMenu = false;
        inputChat.gameObject.SetActive(false);
        listChat = new List<string>();
        CLIENT = new Dictionary<string, TcpClient>();
        isServerStart = PlayerPrefs.GetString("IsServerStart");
        SceneManager.sceneUnloaded += OnSceneUnloaded;

        if (isServerStart == "false" & PlayerPrefs.GetString("Ip server") != "")
        {
            ConnectToServer();
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (inputChat.gameObject.activeSelf)
            {
                scoreManagerScript.enabled= true;
                menuScript.enabled = true;
                inventoryScript.enabled = true;
                playerMovement.isInMenu = false;
                inputChat.gameObject.SetActive(false);
                inputChat.text = "";
            }
            else
            {
                scoreManagerScript.enabled = false;
                menuScript.enabled = false;
                inventoryScript.enabled = false;
                playerMovement.isInMenu = true;
                inputChat.gameObject.SetActive(true);
            }
        }
    }


    // CODE SERVEUR

    // Code connect client
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
                ServerSendMessageAll(clientAddress.ToString(), true);
                Task.Run(async () => await ServerReceiveMessage());
            }
            catch (Exception ex)
            {
                print(ex.ToString());
            }
        }
    }


    // Code receive message
    public async Task ServerReceiveMessage()
    {
        
        while (true)
        {
            try
            { 
                NetworkStream stream = client.GetStream();
                string clientIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();

                byte[] buffer = new byte[1024];
                int bytesRead;
                string textToReceive = "";

                if (isServerStart == "true" & CLIENT.Count != 0)
                {
                    while (true)
                    {
                        bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                        textToReceive = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        print($"Message reçu du client ({((IPEndPoint)client.Client.RemoteEndPoint).Address}): {textToReceive}");
                        break;
                    }
                }

                if (textToReceive == "")
                {
                    textToReceive = ($"{((IPEndPoint)client.Client.RemoteEndPoint).Address} c'est déconnecté");
                    CLIENT.Remove(clientIP);
                    ServerSendMessage(textToReceive, client, true);
                    stream.Close();
                    client.Close();
                    break;
                }
                ServerSendMessage(textToReceive, client);     
            }
            catch (Exception e)
            {
                print(e.Message);
            }
        }
        
    }

    // Code send message at all client
    public async void ServerSendMessageAll(string text, bool isNewClient = false)
    {
        if (client == null | text.Length >= 50)
        {
            return;
        }

        NetworkStream stream = client.GetStream();

        string textToSend = text;

        if (isNewClient)
        {
            textToSend = $"{text} est maintenant connecté !";
        } else
        {
            textToSend = $"{((IPEndPoint)client.Client.RemoteEndPoint).Address} " + text;
        }

        if (isServerStart == "true" & CLIENT.Count != 0)
        {

            foreach (var clients in CLIENT)
            {
                byte[] dataToSend = Encoding.ASCII.GetBytes(textToSend);
                await stream.WriteAsync(dataToSend, 0, dataToSend.Length);
            }
        }
        UnityMainThreadDispatcher.Enqueue(() => {
            PrintOnChat(textToSend);
        });
    }


    // Code send message at all client without sender
    public async void ServerSendMessage(string text, TcpClient sender, bool isClientDeco = false)
    {
        if (client == null && !isClientDeco)
        {
            return;
        }

        string textToSend = $"{((IPEndPoint)client.Client.RemoteEndPoint).Address} " + text;;
        if (isServerStart == "true" & CLIENT.Count != 0)
        {
            foreach (var clients in CLIENT)
            {
                TcpClient eachClient = CLIENT[clients.Key];
                NetworkStream stream = eachClient.GetStream();

                if (eachClient == sender)
                {
                    continue;
                }


                byte[] dataToSend = Encoding.ASCII.GetBytes(textToSend);
                await stream.WriteAsync(dataToSend, 0, dataToSend.Length);
            }
        }
        UnityMainThreadDispatcher.Enqueue(() => {
            PrintOnChat(textToSend);
        });
    }


    // CODE CLIENT

    // Code connection serveur
    public void ConnectToServer()
    {
        try
        {
            string serverIpandPort = PlayerPrefs.GetString("Ip server");
            Match regex = Regex.Match(serverIpandPort, @"(?<ip>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}):(?<port>\d+)");

            string serverIP = regex.Groups["ip"].Value;
            string port = regex.Groups["port"].Value;

            server = new TcpClient(serverIP, int.Parse(port));
            print("Connexion établie");
            ClientReceiveMessage();
        }
        catch (Exception e)
        {
            print("Erreur: " + e.Message);
        }
    }


    // Code receive message
    public async void ClientReceiveMessage()
    {

        if (server == null)
        {
            return;
        }
        NetworkStream stream = server.GetStream();

        byte[] buffer = new byte[1024];
        int bytesRead;
        string textReceive = "";

        while (true)
        {
            bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            textReceive = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            print($"Message recu : {textReceive}");
            break;
        }
        PrintOnChat(textReceive);
    }



    // Code send message
    public async void ClientSendMessage(string textToSend)
    {
        if (server == null | textToSend.Length >= 50)
        {
            return;
        }
        NetworkStream stream = server.GetStream();

        while (true)
        {
            byte[] dataToSend = Encoding.ASCII.GetBytes(textToSend);
            await stream.WriteAsync(dataToSend, 0, dataToSend.Length);
            break;
        }
        PrintOnChat(textToSend);
    }


    public void OnApplicationQuit()
    {
        if (isServerStart == "false")
        {
            server.GetStream().Close();
            server.Close();
        }
    }


    public void OnSceneUnloaded(Scene scene)
    {
        if (scene == SceneManager.GetActiveScene() & isServerStart == "false")
        {
            ClientSendMessage("");
            server.GetStream().Close();
            server.Close();
        }
    }


    // CODE LOCAL

    // Code print message
    private void PrintOnChat(string text)
    {
        try
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
        catch (Exception e)
        {
            print($"Erreur de print (PrintOnChat) {e.Message}");
        }
    }


}

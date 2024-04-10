using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;
using System.Text;
using System;
using UnityEditor.PackageManager;
using System.Threading.Tasks;


public class ServerScript : MonoBehaviour
{
    private Dictionary<string, string> CLIENT;
    public GameObject errorTextParentIp;
    private TMP_Text errorTextIp;
    public GameObject errorTextParentServ;
    private TMP_Text errorTextServ;
    private IPAddress ipAddress;
    private TcpListener listener;
    private string ip = "10.33.72.75";
    private TcpClient client;
    private IPAddress clientAddress;

    private void Start()
    {
        errorTextIp = errorTextParentIp.GetComponentInChildren<TMP_Text>();
        errorTextIp.gameObject.SetActive(false);
        errorTextServ = errorTextParentServ.GetComponentInChildren<TMP_Text>();
        errorTextServ.gameObject.SetActive(false);
    }
    public async void StartServer()
    {
        try
        {
            ipAddress = IPAddress.Parse(ip);
            int port = 8080;
            listener = new TcpListener(ipAddress, port);
            listener.Start();
            print($"Serveur démarré sur {ipAddress}:{port}");
            while (true)
            {
                client = await listener.AcceptTcpClientAsync();
                clientAddress = ((IPEndPoint)client.Client.RemoteEndPoint).Address;
                print($"Client {clientAddress} connecté !");
                Task.Run(async () => await Chat());
            }
        } catch (SocketException ex)
        {
            if (ex.SocketErrorCode == SocketError.AddressAlreadyInUse)
            {
                errorTextServ.gameObject.SetActive(true);
                StartCoroutine(PrintErrorTimeServ());
                return;
            }
        }
    }
    
    public async Task Chat()
    {
        try
        {
            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
            {
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                print($"Message reçu du client ({((IPEndPoint)client.Client.RemoteEndPoint).Address}): {message}");

                byte[] dataToSend = Encoding.ASCII.GetBytes($"Message reçu : {message}");
                await stream.WriteAsync(dataToSend, 0, dataToSend.Length);
            }
        }
        catch (Exception ex)
        {
            print($"Client déconnecté : {ex.Message}");
        }
    }

    public void ServerIP(string ip)
    {
        if (ip == null)
        {
            print("marche pas");
            return;
        }
        string patternIp = @"^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}:\d{1,5}$";
        Regex regex = new Regex(patternIp);
        if (!regex.IsMatch(ip))
        {
            errorTextIp.gameObject.SetActive(true);
            StartCoroutine(PrintErrorTimeIp());
        }
        else
        {
            PlayerPrefs.SetString("Ip server", ip);
            PlayerPrefs.Save();
        }

    }

    IEnumerator PrintErrorTimeIp()
    {
        yield return new WaitForSeconds(3);
        errorTextIp.gameObject.SetActive(false);
    }
    IEnumerator PrintErrorTimeServ()
    {
        yield return new WaitForSeconds(3);
        errorTextServ.gameObject.SetActive(false);
    }
}

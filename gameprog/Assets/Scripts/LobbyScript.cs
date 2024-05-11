using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net;
using System.Net.Sockets;
using System;

public class LobbyScript : MonoBehaviour
{
    private TMP_Text errorBeginPlayerName;
    public GameObject errorBeginPlayerNameParent;
    private TMP_Text errorBeginIp;
    public GameObject errorBeginIpParent;
    private TMP_Text errorBeginPort;
    public GameObject errorBeginPortParent;

    public void Start()
    {
        errorBeginPlayerName = errorBeginPlayerNameParent.GetComponentInChildren<TMP_Text>();
        errorBeginPlayerName.gameObject.SetActive(false);
        errorBeginIp = errorBeginIpParent.GetComponentInChildren<TMP_Text>();
        errorBeginIp.gameObject.SetActive(false);
        errorBeginPort = errorBeginPortParent.GetComponentInChildren<TMP_Text>();
        errorBeginPort.gameObject.SetActive(false);
        if (!PlayerPrefs.HasKey("Inventory"))
        {
            PlayerPrefs.SetString("Inventory", "");
        }
    }
    public void Play()
    {
        string playerName = PlayerPrefs.GetString("PlayerName");
        if (string.IsNullOrWhiteSpace(playerName))    
        {
            errorBeginPlayerName.text = "You need to put your player name";
            errorBeginPlayerName.color = Color.red;
            errorBeginPlayerName.gameObject.SetActive(true);
            StartCoroutine(ErrorBeginPlayerName());
            return;
        }
        SceneManager.LoadScene("Project");
    }

    IEnumerator ErrorBeginPlayerName()
    {
        yield return new WaitForSeconds(3);
        errorBeginPlayerName.gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }


    public void SetPlayerName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
        PlayerPrefs.Save();
    }

    public void SetIp(string ip)
    {
        string regex = @"\b(?:\d{1,3}\.){3}\d{1,3}\b";
        if (!Regex.IsMatch(ip, regex))
        {
            errorBeginIp.text = "You need to put valid ip address";
            errorBeginIp.color = Color.red;
            errorBeginIp.gameObject.SetActive(true);
            StartCoroutine(ErrorBeginIp());
            return;
        }

        PlayerPrefs.SetString("Ip", ip);
        PlayerPrefs.Save();
    }

    IEnumerator ErrorBeginIp()
    {
        yield return new WaitForSeconds(3);
        errorBeginIp.gameObject.SetActive(false);
    }

    public void SetPort(string port)
    {
        string regex = @"\b\d{1,5}\b";
        if (!Regex.IsMatch(port, regex))
        {
            errorBeginPort.text = "You need to put valid port";
            errorBeginPort.color = Color.red;
            errorBeginPort.gameObject.SetActive(true);
            StartCoroutine(ErrorBeginPort());
            return;
        }

        PlayerPrefs.SetString("Port", port);
        PlayerPrefs.Save();
    }

    IEnumerator ErrorBeginPort()
    {
        yield return new WaitForSeconds(3);
        errorBeginPort.gameObject.SetActive(false);
    }

}

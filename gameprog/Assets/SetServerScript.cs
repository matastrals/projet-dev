using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;
using System.Text;
using System;


public class SetServerScript : MonoBehaviour
{
    public GameObject errorTextParentIp;
    private TMP_Text errorTextIp;
    public GameObject MessTextServStateParent;
    private TMP_Text MessTextServState;

    private void Start()
    {
        errorTextIp = errorTextParentIp.GetComponentInChildren<TMP_Text>();
        errorTextIp.gameObject.SetActive(false);
        MessTextServState = MessTextServStateParent.GetComponentInChildren<TMP_Text>();
        MessTextServState.gameObject.SetActive(false);
        PlayerPrefs.SetString("IsServerStart", "false");
    }

    
    public void IsServerStart()
    {
        if (PlayerPrefs.GetString("IsServerStart") == "false")
        {
            PlayerPrefs.SetString("IsServerStart", "true");
            PlayerPrefs.Save();
            MessTextServState.text = "Server is set to up !";
            MessTextServState.color = Color.green;
            MessTextServState.gameObject.SetActive(true);
            StartCoroutine(PrintMessageTimeServ());
        } else
        {
            PlayerPrefs.SetString("IsServerStart", "false");
            MessTextServState.text = "Server is down !";
            MessTextServState.color = Color.red;
            MessTextServState.gameObject.SetActive(true);
            StartCoroutine(PrintMessageTimeServ());
        }
    }

    public void ServerIP(string ip)
    {
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
    IEnumerator PrintMessageTimeServ()
    {
        yield return new WaitForSeconds(3);
        MessTextServState.gameObject.SetActive(false);
    }
}

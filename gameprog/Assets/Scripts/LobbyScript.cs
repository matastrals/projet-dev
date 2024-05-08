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
    private TMP_Text errorBegin;
    public GameObject errorBeginParent;

    public void Start()
    {
        errorBegin = errorBeginParent.GetComponentInChildren<TMP_Text>();
        errorBegin.gameObject.SetActive(false);
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
            errorBegin.text = "You need to put your player name";
            errorBegin.color = Color.red;
            errorBegin.gameObject.SetActive(true);
            StartCoroutine(ErrorBegin());
            return;
        }
        SceneManager.LoadScene("Project");
    }

    IEnumerator ErrorBegin()
    {
        yield return new WaitForSeconds(3);
        errorBegin.gameObject.SetActive(false);
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

}

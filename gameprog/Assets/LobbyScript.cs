using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Net;
using System.Net.Sockets;
using System;

public class LobbyScript : MonoBehaviour
{


    void Start()
    {

    }

    public void Play()
    {
        SceneManager.LoadScene("Project");
    }

    public void Quit()
    {
        Application.Quit();
    }

}

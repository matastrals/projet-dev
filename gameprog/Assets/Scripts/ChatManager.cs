using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    private List<string> allMessage;
    private TestServer testServer;
    private TestClient testClient;
    public TMP_InputField inputField;
    public TMP_Text chatText;
    private bool isServer;

    private void Start()
    {
        testServer = FindAnyObjectByType<TestServer>();
        testClient = FindAnyObjectByType<TestClient>();
        isServer = testServer.GetIsServer();
    }

    private void Update()
    {
        if (isServer)
        {
            allMessage = testServer.GetAllMessage();
        } else
        {
            allMessage = testClient.GetAllMessage();
        }
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!inputField.isFocused && inputField.text == "")
            {
                inputField.ActivateInputField();
            }
            else
            {
                string text = inputField.text;
                inputField.text = "";
                if (isServer)
                {
                    testServer.Send(text);
                } else
                {
                    testClient.Send(text);
                }
                
            }
        } 
        SetChat();
    }

    private void SetChat()
    {
        string message = "";
        foreach (string text in allMessage)
        {
            message += $"{text}\n";
        }
        chatText.text = message;
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class ChatManager : MonoBehaviour
{
    private List<string> allMessage;
    private Server server;
    private Client client;
    public TMP_InputField inputField;
    public TMP_Text chatText;
    private bool isServer;

    private PlayerMovement pm;
    private InventoryScript inventorySc;
    private MenuScript menuSc;
    private ScoreManager scoreManager;

    private void Start()
    {
        pm = FindAnyObjectByType<PlayerMovement>();
        inventorySc = FindAnyObjectByType<InventoryScript>();
        menuSc = FindAnyObjectByType<MenuScript>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
        inputField.gameObject.SetActive(false);
        try
        {
            server = FindAnyObjectByType<Server>();
            client = FindAnyObjectByType<Client>();
            isServer = server.GetIsServer();
        }
        catch
        {
            gameObject.SetActive(false);
        }
        
    }

    private void Update()
    {
        if (isServer)
        {
            allMessage = server.GetAllMessage();
        }
        else
        {
            allMessage = client.GetAllMessage();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!inputField.IsActive() && inputField.text == "")
            {
                DisableOtherUI();
                inputField.ActivateInputField();
            }
            else if (inputField.text == "")
            {
                EnableOtherUI();
            }
            else
            {
                EnableOtherUI();
                string text = inputField.text;
                inputField.text = "";
                if (isServer)
                {
                    server.Send(text);
                }
                else
                {
                    client.Send(text);
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

    private void EnableOtherUI()
    {
        pm.enabled = true;
        inventorySc.enabled = true;
        menuSc.enabled = true;
        scoreManager.enabled = true;
        inputField.gameObject.SetActive(false);
    }

    private void DisableOtherUI()
    {
        pm.enabled = false;
        inventorySc.enabled = false;
        menuSc.enabled = false;
        scoreManager.enabled = false;
        inputField.gameObject.SetActive(true);
    }
}

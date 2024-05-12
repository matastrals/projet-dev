using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private InventoryScript inventoryScript;
    private ChatManager chatManager;
    private ScoreManager scoreManagerScript;
    private Canvas menu;

    private void Start()
    {
        chatManager = FindAnyObjectByType<ChatManager>();
        scoreManagerScript = FindAnyObjectByType<ScoreManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        menu = GameObject.Find("MenuUI").GetComponent<Canvas>();
        inventoryScript = FindObjectOfType<InventoryScript>();
        menu.enabled = false;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.enabled) 
            {
                scoreManagerScript.enabled = true;
                inventoryScript.enabled = true;
                chatManager.enabled = true;
                playerMovement.isInMenu = false;
                menu.enabled = false;
            }
            else
            {
                scoreManagerScript.enabled = false;
                inventoryScript.enabled = false;
                chatManager.enabled = false;
                playerMovement.isInMenu = true;
                menu.enabled = true;
            }
        }
    }

    public void Resume()
    {
        playerMovement.isInMenu = false;
        menu.enabled = false;
        inventoryScript.enabled = true;
    }

    public void BackToLobby()
    {
        playerMovement.isInMenu = true;
        menu.enabled = true;
        SceneManager.LoadScene("Lobby");
    }
}

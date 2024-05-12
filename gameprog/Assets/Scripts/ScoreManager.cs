using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public PlayerMovement playerMovement;
    private string playerName;
    private MenuScript menuScript;
    private InventoryScript inventoryScript;
    private ChatManager chatManager;
    private Canvas scoreManager;

    private void Start()
    {
        chatManager = FindAnyObjectByType<ChatManager>();
        menuScript = FindObjectOfType<MenuScript>();
        inventoryScript = FindObjectOfType<InventoryScript>();
        playerName = PlayerPrefs.GetString("PlayerName");
        scoreManager = GameObject.Find("ScoreUI").GetComponent<Canvas>();
        scoreManager.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) { 
            if (scoreManager.enabled)
            {
                scoreManager.enabled = false;
                playerMovement.isInMenu = false;
                chatManager.enabled = true;
                menuScript.enabled = true;
                inventoryScript.enabled = true;
            }
            else
            {
                scoreManager.enabled = true;
                playerMovement.isInMenu = true;
                chatManager.enabled = false;
                menuScript.enabled = false;
                inventoryScript.enabled = false;
            }
        }       
 

        if (playerMovement != null) 
        {
            scoreText.text = "Player: " + playerName + "\nScore: " + playerMovement.GetNumberOfMoves().ToString();
        }
    }

}
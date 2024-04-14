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
    private ChatScript chatScript;
    private MenuScript menuScript;
    private InventoryScript inventoryScript;
    private Canvas scoreManager;

    private void Start()
    {
        menuScript = FindObjectOfType<MenuScript>();
        inventoryScript = FindObjectOfType<InventoryScript>();
        chatScript = FindObjectOfType<ChatScript>();
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
                menuScript.enabled = true;
                inventoryScript.enabled = true;
                chatScript.enabled = true;
            }
            else
            {
                scoreManager.enabled = true;
                playerMovement.isInMenu = true;
                menuScript.enabled = false;
                inventoryScript.enabled = false;
                chatScript.enabled = false;
            }
        }       
 

        if (playerMovement != null) 
        {
            scoreText.text = "Joueur: " + playerName + "\nScore: " + playerMovement.GetNumberOfMoves().ToString();
        }
    }

}
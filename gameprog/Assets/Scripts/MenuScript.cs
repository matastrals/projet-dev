using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private Canvas menu;

    private InventoryScript inventoryScript;


    private ScoreManager scoreManagerScript;

    private void Start()
    {
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
                playerMovement.isInMenu = false;
                menu.enabled = false;
            }
            else
            {
                scoreManagerScript.enabled = false;
                inventoryScript.enabled = false;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private Canvas menu;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        menu = GameObject.Find("Menu").GetComponent<Canvas>();
        menu.enabled = false;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menu.enabled) 
            {
                playerMovement.isInMenu = false;
                menu.enabled = false;
            }
            else
            {
                playerMovement.isInMenu = true;
                menu.enabled = true;
            }
        }

    }

    public void Resume()
    {
        playerMovement.isInMenu = false;
        menu.enabled = false;
    }

    public void Quit()
    {
        playerMovement.isInMenu = true;
        menu.enabled = true;
    }

}

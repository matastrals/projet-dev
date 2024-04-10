using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{

    private PlayerMovement playerMovement;

    private Canvas inventory;

    private MenuScript menu;
    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        inventory = GameObject.Find("Inventory").GetComponent<Canvas>();
        menu = FindObjectOfType<MenuScript>();
        inventory.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.enabled)
            {
                menu.enabled = true;
                playerMovement.isInMenu = false;
                inventory.enabled = false;
            }
            else
            {
                menu.enabled = false;
                playerMovement.isInMenu = true;
                inventory.enabled = true;
            }
        }
    }
}

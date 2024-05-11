using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryScript : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private Canvas inventory;

    private MenuScript menu;

    public GameObject panel;

    private GameObject[] panelChild;

    private ScoreManager scoreManagerScript;
    void Start()
    {
        scoreManagerScript = FindAnyObjectByType<ScoreManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        inventory = GameObject.Find("Inventory").GetComponent<Canvas>();
        menu = FindObjectOfType<MenuScript>();
        inventory.enabled = false;

        int childCount = panel.transform.childCount;
        panelChild = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            panelChild[i] = panel.transform.GetChild(i).gameObject;
        }
        GetItem();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.enabled)
            {
                scoreManagerScript.enabled = true;
                menu.enabled = true;
                playerMovement.isInMenu = false;
                inventory.enabled = false;
            }
            else
            {
                scoreManagerScript.enabled = false;
                menu.enabled = false;
                playerMovement.isInMenu = true;
                inventory.enabled = true;
            }
        }
    }

    
    private void GetItem()
    {
        string inventory = PlayerPrefs.GetString("Inventory");
        if (inventory == "") {
            return;
        }
        string[] inventoryParse = inventory.Split(",");
        try
        {
            for (var i = 0; i < inventoryParse.Length; i++)
            {
                Texture2D image = new Texture2D(2, 2);
                byte[] fileData = System.IO.File.ReadAllBytes("Assets/Ressources/" + inventoryParse[i] + ".png");
                image.LoadImage(fileData);
                Sprite sprite = Sprite.Create(image, new Rect(0, 0, image.width, image.height), Vector2.zero);
                panelChild[i].gameObject.transform.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
                panelChild[i].gameObject.transform.GetComponent<UnityEngine.UI.Image>().color = Color.white;
            }
        }
        catch (Exception e)
        {
            print(e.ToString());
        }
    }
}

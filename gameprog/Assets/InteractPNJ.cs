using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractPNJ : MonoBehaviour
{
    private BoxCollider2D collid;
    private bool isPlayerColliding = false;
    void Start()
    {
        collid = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (isPlayerColliding & Input.GetKey(KeyCode.E))
        {
            print("PNJ : Congratulation, you did it !");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerColliding = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerColliding = false;
        }
    }
}

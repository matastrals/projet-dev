using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractPNJ : MonoBehaviour
{
    private BoxCollider2D collid;
    private bool isPlayerColliding = false;
    public TMP_Text pnjText;
    void Start()
    {
        collid = GetComponent<BoxCollider2D>();
        pnjText.enabled= false;
        pnjText.text = "PNJ : Congratulation, you did it !";
    }

    void Update()
    {
        if (isPlayerColliding & Input.GetKey(KeyCode.E))
        {
            pnjText.enabled = true;
            StartCoroutine(PrintText());
        }
    }

    IEnumerator PrintText() 
    {
        yield return new WaitForSeconds(3);
        pnjText.enabled = false;
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

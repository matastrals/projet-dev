using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private BoxCollider2D boxCollider;

    private Rigidbody2D rb;

    private Vector3 moveDelta;

    private SpriteRenderer spriteRenderer;

    public Sprite topSprite;

    public Sprite bottomSprite;

    public Sprite leftSprite;

    private Vector2 dir;

    private float speed;

    public bool isInMenu;

    private int numberOfMoves = 0;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        speed = 5f;
    }

    void FixedUpdate()
    {
        Vector3 position = transform.position;

        PlayerPrefs.SetFloat("Position X", position.x);
        PlayerPrefs.SetFloat("Position Y", position.y);

        PlayerPrefs.Save();
        if (!isInMenu)
        {
            dir.x = Input.GetAxisRaw("Horizontal");
            dir.y = Input.GetAxisRaw("Vertical");
            rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                if (dir.magnitude > 0)
                {
                    numberOfMoves++;
                }
                if (dir.x > 0)
                {
                    spriteRenderer.sprite = leftSprite;
                    spriteRenderer.flipX = true;
                }
                else if (dir.x < 0)
                {
                    spriteRenderer.sprite = leftSprite;
                    spriteRenderer.flipX = false;
                }
            }
            else
            {
                if (dir.y > 0)
                {
                    spriteRenderer.sprite = topSprite;
                }
                else if (dir.y < 0)
                {
                    spriteRenderer.sprite = bottomSprite;
                }
            }
        }
    }

    public int GetNumberOfMoves()
    {
        return numberOfMoves;
    }
}

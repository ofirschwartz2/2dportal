using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public LayerMask groundLayers;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;
    private BoxCollider2D legsCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        legsCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LookController();
        MovementController();
    }

    private void LookController()
    {
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            sr.flipX = true;
        if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            sr.flipX = false;
    }

    private void MovementController()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(horizontalMovement, rb2d.velocity.y);
        rb2d.velocity = movement;

        //jump:
        if (Input.GetKeyDown(KeyCode.Z) && rb2d.velocity.y == 0)
            rb2d.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }
}

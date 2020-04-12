using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    private Rigidbody2D rb2d;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(moveHorizontal, 0);
        sr.flipX = movement.x < 0;
        rb2d.AddForce(movement * speed);

        Vector2 jumpVec = new Vector2(0, 10);
        if (Input.GetKeyDown(KeyCode.Z))
            rb2d.AddForce(jumpVec * jumpHeight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

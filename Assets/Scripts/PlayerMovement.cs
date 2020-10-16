using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Private
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private bool isFacingRight = true;
    private bool isGrounded = false;

    //Things to move the player.
    [SerializeField] public float speed;
    [SerializeField] Transform groundCheckPos;
    [SerializeField] private LayerMask whatIsGround;
    private float groundCheckRadius = 0.1f;

    void Start()
    {
        //Grabbing components on the player to move and such.
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        //Move the char left and right.
        float horizMove = Input.GetAxis("Horizontal");
        isGrounded = GroundCheck();
        rb.velocity = new Vector2(horizMove * speed, rb.velocity.y);

        if (isFacingRight && rb.velocity.x < 0 || (!isFacingRight && rb.velocity.x > 0))
        {
            Flip();
        }
    }
    private void Flip()
    {        
        //Flip their sprite around depending on which way they're going.
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;

        isFacingRight = !isFacingRight;
    }
    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheckPos.position, groundCheckRadius, whatIsGround);
    }
}

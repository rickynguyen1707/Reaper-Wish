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
    private int extraJumpValue = 1;

    //Things to move the player.
    [SerializeField] public float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private int extraJump;
    [SerializeField] private float groundCheckRadius = 0.1f;
    [SerializeField] Transform groundCheckPos;
    [SerializeField] private LayerMask whatIsGround;

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
        if (isGrounded)
        {
            extraJumpValue = extraJump;
        }
        rb.velocity = new Vector2(horizMove * speed, rb.velocity.y);
        if (isFacingRight && rb.velocity.x < 0 || (!isFacingRight && rb.velocity.x > 0))
        {
            Flip();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumpValue > 0)
        {
            extraJumpValue--;
            isGrounded = false;
            rb.velocity = Vector2.up * jumpForce;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            extraJumpValue--;
            isGrounded = false;
            rb.velocity = Vector2.up * jumpForce;
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

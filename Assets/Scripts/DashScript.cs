using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript : MonoBehaviour
{
    //"Public" variables
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float checkRadius;
    [SerializeField] private int dashCount;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    //Actual private variables
    private int direction;
    private int dashNumber;
    private float startDashTime;
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        dashNumber = dashCount;
    }
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isGrounded)
        {
            dashNumber = dashCount;
        }

        if (direction == 0)
        {
            if (Input.GetKey(KeyCode.LeftArrow) && dashNumber > 0 && Input.GetKey(KeyCode.Space))
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    direction = 5;
                    dashNumber--;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    direction = 8;
                    dashNumber--;
                }
                else
                {
                    direction = 1;
                    dashNumber--;
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow) && dashNumber > 0 && Input.GetKey(KeyCode.Space))
            {
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    direction = 6;
                    dashNumber--;
                }
                else if (Input.GetKey(KeyCode.UpArrow))
                {
                    direction = 7;
                    dashNumber--;
                }
                else
                {
                    direction = 2;
                    dashNumber--;
                }
            }
            else if (Input.GetKey(KeyCode.UpArrow) && dashNumber > 0 && Input.GetKey(KeyCode.Space))
            {
                direction = 3;
                dashNumber--;
            }
            else if (Input.GetKey(KeyCode.DownArrow) && dashNumber > 0 && Input.GetKey(KeyCode.Space))
            {
                direction = 4;
                dashNumber--;
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;
                switch (direction)
                {
                    case 1:
                        rb.velocity = Vector2.left * dashSpeed;
                        break;
                    case 2:
                        rb.velocity = Vector2.right * dashSpeed;
                        break;
                    case 3:
                        rb.velocity = Vector2.up * dashSpeed;
                        break;
                    case 4:
                        rb.velocity = Vector2.down * dashSpeed;
                        break;
                    case 5:
                        rb.velocity = (Vector2.left * dashSpeed) + (Vector2.up * dashSpeed);
                        break;
                    case 6:
                        rb.velocity = (Vector2.right * dashSpeed) + (Vector2.down * dashSpeed);
                        break;
                    case 7:
                        rb.velocity = (Vector2.up * dashSpeed) + (Vector2.right * dashSpeed);
                        break;
                    case 8:
                        rb.velocity = (Vector2.down * dashSpeed) + (Vector2.left * dashSpeed);
                        break;
                }
            }
        }
    }
}
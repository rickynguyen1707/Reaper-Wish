using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashController : MonoBehaviour
{
    //Components on the player and in the game.
    [Header("Char Components")]
    public Transform groundCheck;
    public LayerMask whatIsGround;

    //Bools to check if player can dash.
    private bool isGrounded = true;
    public bool UnlockDashMove = false;

    //Public things for the dash.
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float startDashTime;
    [SerializeField] private float checkRadius;
    [SerializeField] private int dashCount = 1;
    [SerializeField] private Text DashCounter;

    //Private things for the dash.
    private int direction;
    private int dashNumber;
    private Rigidbody2D rb;
    private bool isDashing;
    private GameObject DashCount;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        dashNumber = dashCount;
    }
    private void Update()
    {
        UpdateDashCounter();
    }
    public void UpdateDashCounter()
    {
        //Depending on how many dashes the player has, Update the UI.
        if (dashNumber == 1)
        {
            DashCounter.text = "You can dash 1 more time.";
        }
        else
        {
            DashCounter.text = $"You can dash {dashNumber} more times.";
        }
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
            if (Input.GetKey(KeyCode.LeftArrow) && UnlockDashMove == true && dashNumber > 0 && Input.GetKey(KeyCode.Space))
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
            else if (Input.GetKey(KeyCode.RightArrow) && UnlockDashMove == true && dashNumber > 0 && Input.GetKey(KeyCode.Space))
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
            else if (Input.GetKey(KeyCode.UpArrow) && UnlockDashMove == true && dashNumber > 0 && Input.GetKey(KeyCode.Space))
            {
                direction = 3;
                dashNumber--;
            }
            else if (Input.GetKey(KeyCode.DownArrow) && UnlockDashMove == true && dashNumber > 0 && Input.GetKey(KeyCode.Space))
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
                rb.velocity = Vector2.zero;
                dashTime = startDashTime;
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
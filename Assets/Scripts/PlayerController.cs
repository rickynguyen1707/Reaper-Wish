using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Some floats to make our player able to move.
    private float horizontalMove;
    private float verticalMove;
    public float speed;
    public float jumpForce;
    public float checkRadius;

    //Some components on the player or around the player to make it easy to move and such.
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    public GameObject gameController;
    public Transform groundCheck;
    public LayerMask whatIsGround;

    //You need to reference the game controller script and jump counter text later on.
    private GameController gameControllerScript;
    public Text JumpCounter;
    
    //Bools to check certain things.
    private bool isGrounded;

    //This allows him to double/triple jump.
    private int extraJumps = 1;
    public int extraJumpsValue;

    private GameObject gameControllerObject;
    void Start()
    {
        //Checking if there's a Game Controller and a jump text in the level already.
        gameControllerObject = GameObject.FindGameObjectWithTag("GameController");

        //If a Game Controller is not null (is found), then grab that component.
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject;
        }
        //Setting certain components and preparing things.
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");

        if (Input.GetAxisRaw("Horizontal") > 0.01f || Input.GetAxisRaw("Horizontal") < -0.01f)
        {
            transform.Translate(new Vector2(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0f));
            Flip();
        }
        verticalMove = rb.velocity.y;
    }
    void Update()
    {
        //Used for the double/triple jump.
        isGrounded = GroundCheck();

        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }
        //I have 2 different jump criterias.
        //1) if you're on the ground you can jump without using your extra jump.
        //2) if you're in the air and still have extra jumps, you can jump.
        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
        {
            if (isGrounded)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
            else
            {
                extraJumps--;
                rb.velocity = Vector2.up * jumpForce;
            }
        }
        UpdateJumpCounter();
    }
    public void Flip()
    {
        if (Input.GetAxisRaw("Horizontal") < 0.01f)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
    public void UpdateJumpCounter()
    {
        //Depending on how many extra jumps the player has, Update the UI.
        if (isGrounded)
        {
            JumpCounter.text = ($"You can jump {extraJumps + 1} more times");
        }
        else
        {
            JumpCounter.text = ($"You can jump {extraJumps} more times");
        }
        if (extraJumps == 0)
        {
            JumpCounter.text = ($"You can jump 0 more times");
        }
    }
    private bool GroundCheck()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }
}
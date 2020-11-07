using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempBoxRemover : MonoBehaviour
{
    [SerializeField]
    private GameObject box1;
    [SerializeField]
    private GameObject box2;

    public int scoreValue = 1;
    public GameController gameControllerScript;

    private void Start()
    {
        //Checking if there's a Game Controller in the level already.
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        //If a Game Controller is not null (is found), then grab that component.
        if (gameControllerObject != null)
        {
            gameControllerScript = gameControllerObject.GetComponent<GameController>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gameControllerScript.AddScore(scoreValue);
            box2.SetActive(false);
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player")
        {
            box1.SetActive(false);
            Destroy(gameObject);
        }
    }
}

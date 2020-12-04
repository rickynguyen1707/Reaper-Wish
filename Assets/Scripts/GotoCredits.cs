using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoCredits : MonoBehaviour
{
    //Reference to the game controller.
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
    void OnCollisionEnter2D(Collision2D other)
    {
        //If the gem collides with player, increase score and destroy it.
        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(4);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject followTarget;
    private Vector3 targetPos;
    public float moveSpeed;

    private GameObject Player;

    void Start()
    {
        //Checking if there's a player in the level already.
        Player = GameObject.FindGameObjectWithTag("Player");

        //If a player is not null (is found), then grab that component.
        if (Player != null)
        {
            followTarget = Player;
        }
    }
    void Update()
    {
        targetPos = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
}

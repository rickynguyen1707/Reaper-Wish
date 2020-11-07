using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public GameObject deathUI;
    public GameObject player;
    [SerializeField] private GameObject MainAudio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            deathUI.SetActive(true);
            MainAudio.GetComponent<AudioSource>().Stop();
            player.SetActive(false);
        }
    }
}

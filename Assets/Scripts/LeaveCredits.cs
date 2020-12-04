using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LeaveCredits : MonoBehaviour
{
    public VideoPlayer video;

    void Awake()
    {
        video.url = System.IO.Path.Combine(Application.streamingAssetsPath, "credist.mp4");
        video.Play();
        video.loopPointReached += CheckOver;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(0);
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        SceneManager.LoadScene(0);
    }
}

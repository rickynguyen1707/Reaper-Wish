using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Determine how to end the game. The candy count.
    private int score;
    [SerializeField] private int count;
    private float currentTime;
    [SerializeField] private bool nextLevel;
    [SerializeField] private bool isRunning;
    [SerializeField] private Text CandyScoreText;
    [SerializeField] private Text DeathText;
    [SerializeField] private Text TimerText;
    [SerializeField] private GameObject AudioWin;
    [SerializeField] private GameObject nextLevelUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject MainAudio;
    [SerializeField] private GameObject AudioLose;

    //Int on index to keep track of the current level.
    private int nextSceneToLoad;
    void Start()
    {
        //In order for a level to run, this needs to run.
        TimerStart();
        //Setting bools to false for the game to run right.
        nextLevel = false;
        //Int of the next level.
        nextSceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }
    void Update()
    {
        //The game timer.
        if (isRunning)
        {
            currentTime += Time.deltaTime;
            isRunning = true;
            TimerText.text = ($"Timer: {currentTime.ToString("00:00.00")}");
        }
        if (nextLevel && Input.GetKeyDown(KeyCode.N))
        {
            NextLevel();
        }
        if (nextLevel && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
        //If player stays stuck on the same level for 15 minutes, end the game (lost).
        if (currentTime >= 900)
        {
            EndGame();
        }
    }
    public void TimerStart()
    {
        //Makes the timer run and time itself to flow at regular speed.
        Time.timeScale = 1f;
        isRunning = true;
    }
    public void NextLevel()
    {
        //Proceed to the next level.
        SceneManager.LoadScene(nextSceneToLoad);
        Time.timeScale = 1f;
    }
    public void Restart()
    {
        //Restart the level.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    public void AddScore(int newScoreValue)
    {
        //Adds score in game.
        score += newScoreValue;
        UpdateScore();
    }
    public void UpdateScore()
    {
        //Updates the game UI that's keeping track of the score. If the score is equal to count, player passed.
        CandyScoreText.text = ($"Candy Score: {+score}/{count}");
        if (score == count)
        {
            GameWon();
        }
    }
    public void GameWon()
    {
        //If player beat the level, make it possible to restart the level and next level becomes active.
        MainAudio.GetComponent<AudioSource>().Stop();
        AudioWin.GetComponent<AudioSource>().Play();
        isRunning = false;
        nextLevel = true;
        nextLevelUI.SetActive(true);
    }
    public void EndGame()
    {
        //If player died after getting the gem, make this obsolete.
        if (nextLevel == false)
        {
            gameOverUI.SetActive(true);
            MainAudio.GetComponent<AudioSource>().Stop();
            AudioLose.GetComponent<AudioSource>().Play();
            if (currentTime >= 900)
            {
                StartCoroutine(ExecuteAfterTime(1.01f));
                DeathText.text = "You ran out of time";
            }
            else
            {
                StartCoroutine(ExecuteAfterTime(1.01f));
                DeathText.text = "You died";
            }
        }
    }
    //Enumerator used to stop time after player lost, exactly 1.01 seconds after time expired or death or gem destroyed.
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 0f;
    }
}
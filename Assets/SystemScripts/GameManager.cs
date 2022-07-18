using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//game manager keeps track of game state, score and UI in MainScene
public class GameManager : MonoBehaviour
{
    //game manager
    public bool isGameActive;
    private int score;

    //text
    public Button restartButton, menuButton;
    public Text scoreText, gameOverText, finalScoreText;
    public GameObject healthBar;

    public void Awake()
    {
        isGameActive = true;
        score = 0;
    }

    public void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        isGameActive = false;
        //display Game over
        gameOverText.gameObject.SetActive(true);
        finalScoreText.text = "Your final score: " + score;
        finalScoreText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(false);
        healthBar.SetActive(false);
        //display buttons
        restartButton.gameObject.SetActive(true);
        menuButton.gameObject.SetActive(true);
        //save scores
        MainManager.Instance.UpdatePlayerList(score);
        MainManager.Instance.SaveLastPlayer();
        MainManager.Instance.SavePlayerList();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameObject endScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI endScreenHeader;
    public TextMeshProUGUI endScreenScoreText; 
    
    public GameObject pauseScreen;

    //instance
    public static GameUI instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateScoreText();
    }

    public void UpdateScoreText()
    {
        scoreText.text = "Score: " + GameManager.instance.score;
    }

    public void SetEndScreen(bool hasWon)
    {
        endScreen.SetActive(true);

        endScreenScoreText.text = "<b>Score</b>\n" + GameManager.instance.score;

        if(hasWon)
        {
            endScreenHeader.text = "You Win!";
            endScreenHeader.color = Color.green;
        }
        else
        {
            endScreenHeader.text = "You Lost!";
            endScreenHeader.color = Color.red;   
        }
    }

    public void OnRestartButton()
    {
        if(GameManager.instance.paused)
            GameManager.instance.TogglePauseGame();

        SceneManager.LoadScene(1); 
    }

    public void OnMenuButton()
    {
        if(GameManager.instance.paused)
            GameManager.instance.TogglePauseGame();

        SceneManager.LoadScene(0);
    }

    public void OnResumeButton()
    {
        GameManager.instance.TogglePauseGame();
    }

    public void TogglePauseScreen (bool paused)
    {
        pauseScreen.SetActive(paused);
    }
}

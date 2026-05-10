using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject gameOverUi;
    [SerializeField] private GameObject gameWinUi;
    private bool isGameOver = false;
    private bool isGameWin = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    [Header("Characters in Scene")]
    public GameObject player;   // object Player trong scene
    public GameObject player1;  // object Player1 trong scene

    void Start()
    {
        string selected = PlayerPrefs.GetString("SelectedCharacter", "Ngoc");

        if (selected == "Ngoc")
        {
            player.SetActive(true);
            player1.SetActive(false);
        }
        else if (selected == "Tien")
        {
            player1.SetActive(true);
            player.SetActive(false);
        }

        UpdateScore();
        gameOverUi.SetActive(false);
        gameWinUi.SetActive(false);
    }

   
    public void AddScore(int points)
    {
        if (!isGameOver && !isGameWin)
        {
            score += points;
            UpdateScore();
        }
        
    }
    public void UpdateScore() => scoreText.text = score.ToString();
    public void GameOver()
    {
        isGameOver = true;
        score = 0;
        Time.timeScale = 0;
        gameOverUi.SetActive(true);
    }
    public void GameWin()
    {
        isGameWin = true;
        Time.timeScale = 0;
        gameWinUi.SetActive(true);
    }
    public void RestartGame()
    {
        isGameOver = false;
        score = 0;
        UpdateScore();
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void GotoMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }
    public bool IsGameOver()
    {
        return isGameOver;
    }
    public bool IsGameWin()
    {
            return isGameWin;
    }
}

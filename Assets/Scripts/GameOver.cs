using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public Text gameOverText;

    public void Start()
    {
        if (PlayerPrefs.GetString("Loser").Equals("Player 1"))
        {
            gameOverText.text = "Game Over! You Lost!";
        }
        else
        {
            gameOverText.text = "Game Over! You Win!";
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

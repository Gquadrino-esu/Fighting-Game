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
        if (PlayerPrefs.GetString("GameType").Equals("AI"))
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
        else if (PlayerPrefs.GetString("GameType").Equals("TwoPlayer"))
        {
            if (PlayerPrefs.GetString("Loser").Equals("Player 1"))
            {
                gameOverText.text = "Game Over! Player 2 wins!";
            }
            else
            {
                gameOverText.text = "Game Over! Player 1 wins!";
            }
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

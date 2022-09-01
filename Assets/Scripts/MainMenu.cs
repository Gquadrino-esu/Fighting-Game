using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void AI()
    {
        PlayerPrefs.SetString("GameType", "AI");
        StartGame();
    }

    public void TwoPlayer()
    {
        PlayerPrefs.SetString("GameType", "TwoPlayer");
        StartGame();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject AI, Player2;

    public void Start()
    {
        if (PlayerPrefs.GetString("GameType").Equals("TwoPlayer"))
        {
            Player2.transform.position = new Vector2(6.0f, -1.0f);
            Player2.SetActive(true);
        }
        else
        {
            AI.transform.position = new Vector2(6.0f, -1.0f);
            AI.SetActive(true);
        }
    }

    public void EndGame(string loser)
    {
        if (loser.Equals("Player 1"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            // Maybe add another scene where you lose? Has to be a simplier way of telling the next scene who won or lost
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

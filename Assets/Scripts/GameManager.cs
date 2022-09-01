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

    public void EndGame()
    {
        SceneManager.LoadScene("GameOver");
    }
}

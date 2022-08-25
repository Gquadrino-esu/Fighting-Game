using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void EndGame(string loser)
    {
        //PlayerPrefs.SetString(loser, winner);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int lives;
    public Text livesText;

    // Start is called before the first frame update
    void Start()
    {
        lives = 3;
        livesText.text = "3";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LostLife()
    {
        lives--;
        livesText.text = lives.ToString();
        if (lives == 0)
        {
            Died();
        }
        else
        {
            gameObject.transform.position = new Vector2(0f, 8.0f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            GetComponent<Attack>().Killed();
            //FindObjectOfType<Attack>().attackForce = 50000f;
            //FindObjectOfType<Attack>().enemyDamage = 0;
            //FindObjectOfType<Attack>().enemyDamageText.text = "0%";
        }
    }

    public void Died()
    {
        PlayerPrefs.SetString("Loser", gameObject.tag);
        FindObjectOfType<GameManager>().EndGame(gameObject.tag);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bounds : MonoBehaviour
{
    public int playerLives, enemyLives, playerDamage, enemyDamage;
    public Text playerLivesText, enemyLivesText, playerDamageText, enemyDamageText;

    // Start is called before the first frame update
    void Start()
    {
        //playerLivesText.text = "3";
        //enemyLivesText.text = "3";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player 1" || collision.gameObject.tag == "AI")
        {
            // Player class takes care of lives
            collision.gameObject.GetComponent<Player>().LostLife();

            /*
            if (playerLives == 0 || enemyLives == 0)
            {
                FindObjectOfType<GameManager>().EndGame();
            }
            else
            {
                ResetDamage();
                collision.gameObject.transform.position = new Vector2(0f, 5.0f);
                collision.rigidbody.velocity = new Vector2(0f, 0f);
            }
            */
        }

    }

    public void ResetDamage()
    {
        playerDamage = 0;
        playerDamageText.text = "0%";
        enemyDamage = 0;
        enemyDamageText.text = "0%";
        FindObjectOfType<Attack>().enemyDamage = 0;
        FindObjectOfType<Attack>().enemyDamageText.text = "0%";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public float sidewaysForce = 500f;
    public float upwardsForce = 20000f;

    bool canJump = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (tag == "Player 1")
        {
            if (Input.GetKey("d"))
            {
                rb.AddForce(new Vector2(sidewaysForce * Time.deltaTime, 0));
            }

            if (Input.GetKey("a"))
            {
                rb.AddForce(new Vector2(-sidewaysForce * Time.deltaTime, 0));
            }

            if (Input.GetKey("w") && canJump)
            {
                canJump = false;
                rb.AddForce(new Vector2(0, upwardsForce * Time.deltaTime));
            }

            
        }
        else if (tag == "Player 2")
        {
            if (Input.GetKey("right"))
            {
                rb.AddForce(new Vector2(sidewaysForce * Time.deltaTime, 0));
            }

            if (Input.GetKey("left"))
            {
                rb.AddForce(new Vector2(-sidewaysForce * Time.deltaTime, 0));
            }

            if (Input.GetKey("up") && canJump)
            {
                canJump = false;
                rb.AddForce(new Vector2(0, upwardsForce * Time.deltaTime));
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Platform" || collision.collider.tag == "AI")
        {
            canJump = true;
        }
        /*
        if (collision.collider.tag == "Player 2")
        {
            if (Input.GetKey("e"))
            {
                float playerX = rb.velocity.x * 10000;
                Debug.Log("playerX is " + playerX);
                collision.rigidbody.AddForce(new Vector2(playerX * Time.deltaTime, 0));
            }
        }
        */
    }
}

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
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(new Vector2(sidewaysForce * Time.deltaTime, 0));
            }

            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(new Vector2(-sidewaysForce * Time.deltaTime, 0));
            }

            if (Input.GetKey(KeyCode.W) && canJump)
            {
                canJump = false;
                rb.AddForce(new Vector2(0, upwardsForce * Time.deltaTime));
            }

            
        }
        else if (tag == "Player 2")
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.AddForce(new Vector2(sidewaysForce * Time.deltaTime, 0));
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.AddForce(new Vector2(-sidewaysForce * Time.deltaTime, 0));
            }

            if (Input.GetKey(KeyCode.UpArrow) && canJump)
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
    }
}

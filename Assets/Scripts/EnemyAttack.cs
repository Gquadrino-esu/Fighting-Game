using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAttack : MonoBehaviour
{
    // Note: Eventually this whole class should be merged with Attack

    public Transform attackPoint;
    public float attackRange = 1f, attackForce = 3000f, randNum;
    public LayerMask playerLayers;
    public SpriteRenderer playerSprite;
    public GameObject player;
    bool canHit = true;
    public int playerDamage;
    public Text playerDamageText;

    // Update is called once per frame
    void Update()
    {   
        Fight();
        
    }

    void Fight()
    {
        // Finds collider of enemy caught in the overlap circle, where they can be attacked
        Collider2D playerColl = Physics2D.OverlapCircle(attackPoint.position, attackRange, playerLayers);

        if (playerColl != null)
        {
            randNum = Random.Range(0f, 2f);
            if (randNum - 0f < 0.4f && canHit)
            {
                //Debug.Log("Enemy hits.");

                Rigidbody2D playerRB = playerSprite.GetComponent<Rigidbody2D>();

                if (attackPoint.position.x > playerRB.position.x)
                {
                    playerRB.AddForce(new Vector2(-attackForce * Time.deltaTime, attackForce * Time.deltaTime));
                }
                else
                {
                    playerRB.AddForce(new Vector2(attackForce * Time.deltaTime, attackForce * Time.deltaTime));
                }

                // The more the player is hit, the farther they are pushed back
                attackForce += 500f;

                
                StartCoroutine(GotHit());
                StartCoroutine(PlayerDamaged());
            }
            else
            {
                //Debug.Log("Enemy misses.");
            }

            StartCoroutine(Cooldown());
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Prevents null instance error
        if (attackPoint == null)
        {
            return;
        }

        // Makes the attack circle visible
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    IEnumerator GotHit()
    {
        playerSprite.color = Color.red;
        if (player.GetComponent<PlayerMovement>() != null)
        {
            player.GetComponent<PlayerMovement>().enabled = false;
        }
        yield return new WaitForSeconds(0.5f);
        playerSprite.color = new Color32(0, 207, 243, 255);
        if (player.GetComponent<PlayerMovement>() != null)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
        }
    }

    IEnumerator Cooldown()
    {
        canHit = false;
        yield return new WaitForSeconds(1f);
        canHit = true;
    }

    IEnumerator PlayerDamaged()
    {
        playerDamage += 50;
        playerDamageText.text = playerDamage.ToString() + "%";
        yield return new WaitForSeconds(1f);
    }
}

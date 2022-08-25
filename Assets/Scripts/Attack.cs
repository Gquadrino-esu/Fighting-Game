using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange, attackForce, randNum;      // randNum determines whether the enemy hits or misses the player
    public LayerMask layers, enemyLayers;
    public SpriteRenderer sprite, enemySprite;
    public GameObject enemy;
    public int damage, enemyDamage, i;
    public Text damageText, enemyDamageText;
    public float hitDelay, hitNext;       // hitDelay is the space between when the player has hit and can next hit; hitNext is hitDelay + Time.deltaTime
    public bool canHit;
    Color32 playerColor, AIColor;

    // NOTE: Start distinguishing between "enemy" and "AI"
    // Enemy is the enemy of whoever the method is attached to
    // AI is the AI that the player fights
    // Another word for "enemy" could be "other"
    // "Player" can refer to whoever the method is attached to, not literally the player
    // Could just not have a word for it, i.e. layers and enemyLayers instead of playerLayers and enemyLayers

    private void Start()
    {
        attackRange = 0.75f;
        attackForce = 20000f;
        hitDelay = 1.0f;
        canHit = true;
        playerColor = new Color32(0, 207, 243, 255);
        AIColor = new Color32(241, 0, 234, 255);

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "Player 1")
        {
            if (Input.GetKey(KeyCode.Space) && canHit)
            {
                Fight();
            }
        }
        else if (gameObject.tag == "AI")
        {
            if (canHit)
            {
                AIFight();
            }
        }
    }

    void Fight()
    {
        // Finds collider of enemy caught in the overlap circle, where they can be attacked
        Collider2D enemyColl = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);

        if (enemyColl != null)
        {
            Rigidbody2D enemyRB = enemy.GetComponent<Rigidbody2D>();

            StartCoroutine(GotHit());

            enemyRB.velocity = new Vector2(0f, 0f);
            if (attackPoint.position.x > enemyRB.position.x)
            {
                // AI pushed left
                enemyRB.AddForce(new Vector2(-attackForce * Time.deltaTime, attackForce * Time.deltaTime));
            }
            else
            {
                // AI pushed right
                enemyRB.AddForce(new Vector2(attackForce * Time.deltaTime, attackForce * Time.deltaTime));
            }

            // The more the enemy is hit, the farther they are pushed back
            attackForce += 1000f;
            enemyDamage += 50;
            enemyDamageText.text = enemyDamage.ToString() + "%";

            StartCoroutine(Cooldown());
        }
    }

    void AIFight()
    {
        // Finds collider of enemy caught in the overlap circle, where they can be attacked
        Collider2D enemyColl = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayers);

        if (enemyColl != null)
        {
            randNum = Random.Range(0f, 2f);
            if (randNum < 1.0f)
            {
                // AI hits
                Rigidbody2D enemyRB = enemySprite.GetComponent<Rigidbody2D>();

                StartCoroutine(GotHit());

                enemyRB.velocity = new Vector2(0f, 0f);
                if (attackPoint.position.x > enemyRB.position.x)
                {
                    enemyRB.AddForce(new Vector2(-attackForce * Time.deltaTime, attackForce * Time.deltaTime));
                }
                else
                {
                    enemyRB.AddForce(new Vector2(attackForce * Time.deltaTime, attackForce * Time.deltaTime));
                }

                // The more the player is hit, the farther they are pushed back
                attackForce += 1000f;
                enemyDamage += 50;
                enemyDamageText.text = enemyDamage.ToString() + "%";
            }
            else
            {
                // AI misses
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

    // Enemy got hit by whoever the script is attached to
    IEnumerator GotHit()
    {
        enemySprite.color = Color.red;
        
        //Debug.Log(gameObject.tag);
        if (enemy.tag == "Player 1")
        {
            //Debug.Log("The player is the enemy");
            if (enemy.GetComponent<PlayerMovement>() != null)
            {
                enemy.GetComponent<PlayerMovement>().enabled = false;
            }

            yield return new WaitForSeconds(0.5f);

            enemySprite.color = playerColor;

            if (enemy.GetComponent<PlayerMovement>() != null)
            {
                enemy.GetComponent<PlayerMovement>().enabled = true;
            }
        }
        else if (enemy.tag == "AI")
        {
            //Debug.Log("The AI is the enemy");
            if (enemy.GetComponent<EnemyMovement>() != null)
            {
                enemy.GetComponent<EnemyMovement>().enabled = false;
            }

            yield return new WaitForSeconds(0.5f);

            enemySprite.color = AIColor;

            if (enemy.GetComponent<EnemyMovement>() != null)
            {
                enemy.GetComponent<EnemyMovement>().enabled = true;
            }
        }
    }

    // Cooldown for attack
    IEnumerator Cooldown()
    {
        canHit = false;
        yield return new WaitForSeconds(hitDelay);
        canHit = true;
    }
}

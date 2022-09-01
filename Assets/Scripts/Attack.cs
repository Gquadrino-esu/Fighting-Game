using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    public Transform attackPoint;
    private float attackRange, attackForce, randNum;      // randNum determines whether the enemy hits or misses the player
    public LayerMask layers, enemyLayers;
    public SpriteRenderer sprite, enemySprite;
    private GameObject enemy;
    private int damage, enemyDamage;
    public Text damageText, enemyDamageText;
    private float hitDelay;       // hitDelay is the space between when the player has hit and can next hit
    private bool canHit;
    Color32 playerColor, AIColor;

    // NOTE: Start distinguishing between "enemy" and "AI"
    // Enemy is the enemy of whoever the method is attached to
    // AI is the AI that the player fights
    // Another word for "enemy" could be "other"
    // "Player" can refer to whoever the method is attached to, not literally the player
    // Could just not have a word for it, i.e. layers and enemyLayers instead of playerLayers and enemyLayers

    // NOTE 2: Can't GetComponent from a game object that starts out disabled
    // Use GetComponentInChildren, and set () to true
    // i.e. enemySprite = enemy.GetComponent<SpriteRenderer>(); won't work
    // enemySprite = enemy.GetComponentInChildren<SpriteRenderer>(true); will work

    private void Start()
    {
        attackRange = 0.75f;
        attackForce = 50000f;   // Apparently 50,000 is too small, 500,000 is too big
        hitDelay = 1.0f;
        canHit = true;
        playerColor = new Color32(0, 207, 243, 255);
        AIColor = new Color32(241, 0, 234, 255);

        //layers = gameObject.layer;
        //sprite = gameObject.GetComponent<SpriteRenderer>();

        if (gameObject.tag.Equals("Player 1"))
        {
            layers = LayerMask.GetMask("Player 1");

            // Sets Player 1's enemy to Player 2
            if (PlayerPrefs.GetString("GameType").Equals("TwoPlayer"))
            {
                enemy = GameObject.FindGameObjectWithTag("Player 2");
                enemyLayers = LayerMask.GetMask("Player 2");

                // Activates Player 2
                enemy.GetComponent<Player>().enabled = true;
                enemy.GetComponent<BoxCollider2D>().enabled = true;
            }
            else // Sets Player 1's enemy to the AI
            {
                enemy = GameObject.FindGameObjectWithTag("AI");
                enemyLayers = LayerMask.GetMask("AI");

                // Activates the AI
                enemy.GetComponent<EnemyMovement>().enabled = true;
                enemy.GetComponent<Player>().enabled = true;
                enemy.GetComponent<BoxCollider2D>().enabled = true;
            }

            enemySprite = enemy.GetComponentInChildren<SpriteRenderer>(true);
        }
        else
        {
            enemy = GameObject.FindGameObjectWithTag("Player 1");
            enemyLayers = LayerMask.GetMask("Player 1");
            if (gameObject.tag.Equals("Player 2"))
            {
                layers = LayerMask.GetMask("Player 2");
            }
            else
            {
                layers = LayerMask.GetMask("AI");
            }
            enemySprite = enemy.GetComponent<SpriteRenderer>();
        }
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
        else if (gameObject.tag == "Player 2")
        {
            if (Input.GetKey(KeyCode.RightShift) && canHit)
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
                // Enemy pushed left
                enemyRB.AddForce(new Vector2(-attackForce * Time.deltaTime, attackForce * Time.deltaTime));
            }
            else
            {
                // Enemy pushed right
                enemyRB.AddForce(new Vector2(attackForce * Time.deltaTime, attackForce * Time.deltaTime));
            }

            // The more the enemy is hit, the farther they are pushed back
            attackForce = enemy.GetComponent<Attack>().damage * 10000 + 50000f;
            // Directly change the enemy's script
            enemy.GetComponent<Attack>().damage += 50;
            // Reset enemyDamage
            enemyDamage = enemy.GetComponent<Attack>().damage;
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
                attackForce = enemyDamage * 10000 + 50000f;
                // Directly change the enemy's script
                enemy.GetComponent<Attack>().damage += 50;
                // Reset enemyDamage
                enemyDamage = enemy.GetComponent<Attack>().damage;
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
        else
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

    public void Killed()
    {
        Debug.Log("Killed() has been called on " + tag);
        Debug.Log("Damage was " + damage);
        damage = 0;
        Debug.Log("Damage is now " + damage);
        damageText.text = "0%";
    }
}

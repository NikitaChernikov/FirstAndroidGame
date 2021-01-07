using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour {

    


    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private bool m_combatIdle = false;
    SpriteRenderer sr;

    /// /////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public int health;

    public Transform attackPose;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public int damage;

    private HeroKnight player;

    public EnemyHealthBar enemyHealthBar;
    /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    // Use this for initialization
    void Start() {
        player = FindObjectOfType<HeroKnight>();
        sr = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();

        enemyHealthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update() {
        

        

        // -- Handle Animations --
        //Death
        if (health <= 0) {
            m_animator.SetTrigger("Death");
            enemyHealthBar.RemoveHealthBar();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //Destroy(gameObject, 0.5f);
        }

        if (m_combatIdle)
        {
            m_animator.SetInteger("AnimState", 1);
        } 


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Swap direction of sprite depending on walk direction
            if (GameObject.Find("HeroKnight").transform.position.x < gameObject.transform.position.x && health > 0)
                sr.flipX = false;
            else if (GameObject.Find("HeroKnight").transform.position.x > gameObject.transform.position.x && health > 0)
                sr.flipX = true;
            m_combatIdle = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_combatIdle = false;
            m_animator.SetInteger("AnimState", 2);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_combatIdle = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_combatIdle = true;
        }
    }

    /// //////////////////////////////////////////////////////////////////////////////////////////////////
    public void TakeDamage(int damage)
    {
        m_animator.SetTrigger("Hurt");
        health -= damage;
        FindObjectOfType<AudioManager>().Play("Damage");
        enemyHealthBar.SetHealth(health);
    }

    public void OnPlayerAttack()
    {
        player.TakeDamage(damage);
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////
}

using UnityEngine;
using System.Collections;

public class Bandit : MonoBehaviour {

    [SerializeField] float m_speed = 4.0f;
    //[SerializeField] float      m_jumpForce = 7.5f;


    private Animator m_animator;
    private Rigidbody2D m_body2d;
    private Sensor_Bandit m_groundSensor;
    private bool m_grounded = false;
    private bool m_combatIdle = false;
    //private bool                m_isDead = false;
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
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Bandit>();

        enemyHealthBar.SetMaxHealth(health);
    }

    // Update is called once per frame
    void Update() {
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State()) {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State()) {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Set AirSpeed in animator
        m_animator.SetFloat("AirSpeed", m_body2d.velocity.y);

        // -- Handle Animations --
        //Death
        if (health <= 0) {
            m_animator.SetTrigger("Death");
            Destroy(gameObject, 0.5f);
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
            if (GameObject.Find("HeroKnight").transform.position.x < gameObject.transform.position.x)
                sr.flipX = false;
            else if (GameObject.Find("HeroKnight").transform.position.x > gameObject.transform.position.x)
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

        enemyHealthBar.SetHealth(health);
    }

    public void OnPlayerAttack()
    {
        player.TakeDamage(damage);
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] float m_speed = 4.0f;
    private Animator anim;
    SpriteRenderer sr;
    private HeroKnight player;
    bool idle = true;
    bool run = false;
    bool hurt = false;
    bool defeat = false;

    public int health;

    public int damage;

    public EnemyHealthBar enemyHealthBar;


    private void Start()
    {
        player = GetComponent<HeroKnight>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<HeroKnight>();

        enemyHealthBar.SetMaxHealth(health);
    }

    private void FixedUpdate()
    {
        //animations
        if (idle)
            anim.SetInteger("Boss", 0);
        else if (run)
            anim.SetInteger("Boss", 1);
        else if (defeat)
            anim.SetInteger("Boss", 4);

        //death
        if (health <= 0)
        {
            run = false;
            idle = false;
            defeat = true;
        }

        //run
        if (run)
        {
            
            transform.position = Vector2.Lerp(transform.position, GameObject.Find("HeroKnight").transform.position, m_speed * Time.deltaTime);
        }
        else
        {
            FindObjectOfType<AudioManager>().Stop("BossSteps");
        }
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameObject.Find("HeroKnight").transform.position.x > gameObject.transform.position.x)
                sr.flipX = false;
            else if (GameObject.Find("HeroKnight").transform.position.x < gameObject.transform.position.x)
                sr.flipX = true;
            FindObjectOfType<AudioManager>().Play("BossSteps");
            idle = false;
            run = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            run = false;
            idle = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            run = false;
            anim.SetInteger("Boss", 2);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            run = true;
        }
    }





    public void onPlayerAttack()
    {
        FindObjectOfType<AudioManager>().Play("BossHit");
        player.TakeDamage(damage);
    }


    public void TakeDamage(int damage)
    {
        FindObjectOfType<AudioManager>().Play("Damage");
        anim.SetTrigger("Hurt");
        health -= damage;
        enemyHealthBar.SetHealth(health);
    }

    
}

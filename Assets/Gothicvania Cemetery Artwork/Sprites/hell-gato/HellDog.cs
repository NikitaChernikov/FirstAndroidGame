using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellDog : MonoBehaviour
{
    ////////////////////////////////////////////////////////////////////// 
    public int health;

    public int damage;

    private HeroKnight player;
    ///////////////////////////////////////////////////////////////// 


    SpriteRenderer sr;
    Animator anim;
    Rigidbody2D rb;
    public int distance;
    float maxDistance;
    float minDistance;
    int speed = 3;

    private void Start()
    {
        player = FindObjectOfType<HeroKnight>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        maxDistance = transform.position.x + distance;
        minDistance = transform.position.x - distance;
    }

    private void FixedUpdate()
    {
        transform.Translate(transform.right * speed * Time.deltaTime);
        if (transform.position.x > maxDistance)
        {
            sr.flipX = false;
            speed = -speed;
        }
        else if (transform.position.x < minDistance)
        {
            sr.flipX = true;
            speed = -speed;
        }

        if (health <= 0)
        {
            anim.SetInteger("Dog", 1);
            Destroy(gameObject, 0.5f);
        }
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.TakeDamage(damage);
            if (sr.flipX == true)
            {
                sr.flipX = false;
                speed = -speed;
            }
            else if (sr.flipX == false)
            {
                sr.flipX = true;
                speed = -speed;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellsDog : MonoBehaviour
{
    SpriteRenderer sr;
    Animator anim;
    Rigidbody2D rb;
    public int distance;
    float maxDistance;
    float minDistance;
    int speed = 3;

    private void Start()
    {
        
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
    }

}

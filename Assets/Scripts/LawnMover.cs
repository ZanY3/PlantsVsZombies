using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LawnMover : MonoBehaviour
{
    public float destroyTime;
    public float speed;

    private Rigidbody2D rb;
    private bool isMoving = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(isMoving)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            Destroy(gameObject, destroyTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            isMoving = true;
            collision.gameObject.GetComponent<ZombieController>().health = 0;
        }
    }
}
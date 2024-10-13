using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float speed;
    public int health;
    public GameObject head;

    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private async void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);

        if(health <= 0)
        {
            //sounds, effects
            head.SetActive(false);
            anim.SetTrigger("Death");
            await new WaitForSeconds(1.5f);
            Destroy(gameObject);
        }
    }
}
